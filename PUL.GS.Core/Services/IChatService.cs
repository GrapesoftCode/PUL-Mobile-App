using PUL.GS.Core.EventHandlers;
using PUL.GS.Models;
using PUL.GS.Models.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUL.GS.Core.Services
{
    public interface IChatService
    {
        bool IsConnected { get; }
        string ConnectionToken { get; set; }
        Task InitAsync(string userId);
        Task DisconnectAsync();
        Task SendMessageAsync(ChatMessage message);
        Task JoinChannelAsync(UserConnectedMessage message);
        Task<List<Room>> GetRooms();
        Task LeaveChannelAsync(UserConnectedMessage message);
        Task<List<User>> GetUsersGroup(string group);
        Task<User> GetUser(string userId);

        event EventHandler<MessageEventArgs> OnReceivedMessage;
    }
}
