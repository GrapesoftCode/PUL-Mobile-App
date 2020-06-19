using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using PUL.GS.Core.EventHandlers;
using PUL.GS.Core.Models;
using PUL.GS.Models;
using PUL.GS.Models.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PUL.GS.Core.Services
{
    public class ChatService : IChatService
    {
        public bool IsConnected { get; set; }

        public string ConnectionToken { get; set; }
        private SemaphoreSlim semaphoreSlim =
            new SemaphoreSlim(1, 1);
        private HttpClient httpClient;
        HubConnection hub;
        public event EventHandler<MessageEventArgs> OnReceivedMessage;

        public async Task InitAsync(string userId)
        {
            await semaphoreSlim.WaitAsync();
            if (httpClient == null)
            {
                httpClient = new HttpClient();
            }

            var result = await httpClient
                .GetStringAsync($"{Config.NegotiateEndPoint}/{userId}");

            var info = JsonConvert.DeserializeObject<ConnectionInfo>(result);

            var connectionBuilder =
                new HubConnectionBuilder();

            connectionBuilder.WithUrl(info.Url, (obj) =>
            {
                obj.AccessTokenProvider = () => Task.Run(() => info.AccessToken);
            });

            hub = connectionBuilder.Build();

            await hub.StartAsync();

            ConnectionToken = hub.ConnectionId;

            IsConnected = true;

            hub.On<object>("ReceiveMessage", (message) =>
            {
                var json = message.ToString();
                var obj = JsonConvert.DeserializeObject<ChatMessage>(json);
                var msg = (ChatMessage)JsonConvert
                .DeserializeObject(json, obj.TypeInfo);
                OnReceivedMessage?.Invoke(this, new MessageEventArgs(msg));
            });

            semaphoreSlim.Release();
        }

        public async Task DisconnectAsync()
        {
            if (!IsConnected)
                return;

            try
            {
                await hub.DisposeAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsConnected = false;
        }

        public async Task SendMessageAsync(ChatMessage message)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Not connected");
            }

            var json =
                JsonConvert.SerializeObject(message);

            var content = new StringContent(json, Encoding.UTF8,
                "application/json");

            await httpClient.PostAsync(Config.MessagesEndPoint, content);
        }

        public async Task JoinChannelAsync(UserConnectedMessage message)
        {
            if (!IsConnected)
                return;
            message.Token = ConnectionToken;

            message.IsEntering = true;

            var json = JsonConvert.SerializeObject(message);

            var content = new StringContent(json, Encoding.UTF8,
                "application/json");

            await httpClient.PostAsync(Config.AddToGroupEndPoint, content);

            await SendMessageAsync(message);
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms =
                new List<Room>
                {
                new Room
                {
                    Name = "C#",
                    Image = "csharp.png"
                },
                new Room
                {
                    Name = "Xamarin",
                    Image = "xamarin.png"
                },
                new Room
                {
                    Name = ".NET",
                    Image = "dotnet.png"
                },
                new Room
                {
                    Name = "ASP.NET Core",
                    Image = "aspcore.png"
                },
                new Room
                {
                    Name = "Xamarin Forms",
                    Image = "xamforms.png"
                },
                new Room
                {
                    Name = "Visual Studio",
                    Image = "visualstudio.png"
                }
                };

            foreach (var room in rooms)
            {
                room.UserNumber = await GetRoomCount(room.Name);
            }

            return rooms;
        }

        public async Task LeaveChannelAsync(UserConnectedMessage message)
        {
            if (!IsConnected)
                return;
            message.Token = ConnectionToken;

            var json = JsonConvert.SerializeObject(message);

            var content = new StringContent(json, Encoding.UTF8,
                "application/json");

            await httpClient.PostAsync(Config.LeaveGroupEndPoint, content);

            await SendMessageAsync(message);
        }

        public async Task<List<User>> GetUsersGroup(string group)
        {
            var url =
                $"{Config.RoomEndPoint}/{group}";
            var result =
                await httpClient.GetStringAsync(url);

            var users =
                JsonConvert.DeserializeObject<List<User>>(result);
            return users;
        }

        private async Task<int> GetRoomCount(string group)
        {
            var users = await GetUsersGroup(group);
            return users.Count;
        }

        public async Task<User> GetUser(string userId)
        {
            var url =
                $"{Config.UserEndPoint}/{userId}";
            var result =
                await httpClient.GetStringAsync(url);
            var users =
               JsonConvert.DeserializeObject<List<User>>(result);
            return users.FirstOrDefault();
        }
    }
}
