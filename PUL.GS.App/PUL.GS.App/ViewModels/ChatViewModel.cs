//using Acr.UserDialogs;
using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PUL.GS.Core.Helpers;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using PUL.GS.Models.Messages;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class ChatViewModel : FreshBasePageModel
    {
        readonly IChatService ChatService;
        IUserDialogs dialogs;

        public ObservableCollection<User> Users { get; set; } =
            new ObservableCollection<User>();

        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }
        public User SelectedUser { get; set; }

        public ObservableCollection<ChatMessage> Messages { get; set; } =
            new ObservableCollection<ChatMessage>();

        public ICommand SendCommand => new Command(async () => 
        {
            if (string.IsNullOrEmpty(Text))
            {
                return;
            }
            var message = new SimpleTextMessage(UserName)
            {
                Text = this.Text,
                GroupName = this.GroupName
            };

            Messages.Add(new LocalSimpleTextMessage(message));
            await ChatService.SendMessageAsync(message);
            Text = string.Empty;
        });

        public ICommand PhotoCommand => new Command(async () =>
        {
             await CrossMedia.Current.Initialize();
            var options = new PickMediaOptions
            {
                CompressionQuality = 50
            };

            var photo = await CrossMedia.Current.PickPhotoAsync(options);
            var stream = photo.GetStream();
            var bytes = ImageHelper.ReadFully(stream);

            var base64Photo = Convert.ToBase64String(bytes);

            var message = new PhotoMessage(UserName)
            {
                Base64Photo = base64Photo,
                FileEnding = photo.Path.Split('.').Last(),
                GroupName = GroupName
            };

            Messages.Add(message);

            dialogs.ShowLoading("Cargando...");
            await ChatService.SendMessageAsync(message);
            dialogs.HideLoading();

        });

        //public ICommand ItemSelectedCommand => new Command(async () =>
        //{

        //    if (SelectedUser != null)
        //    {
        //        var privateMessage =
        //        await _dialogs
        //        .PromptAsync($"Mensaje privado para: {SelectedUser.userId}");
        //        if (string.IsNullOrEmpty(privateMessage.Text))
        //        {
        //            return;
        //        }

        //        var message = new SimpleTextMessage(UserName)
        //        {
        //            Text = privateMessage.Text,
        //            Recipient = SelectedUser.userId
        //        };

        //        await ChatService.SendMessageAsync(message);
        //        SelectedUser = null;
        //    }
        //});

        public ICommand LeaveCommand => new Command(async () =>
        {
            var message = new UserConnectedMessage(UserName, GroupName);
            await ChatService.LeaveChannelAsync(message);
            await CoreMethods.PopPageModel();
        });

        public ChatViewModel(
            IUserDialogs _dialogs,
            IChatService _chatService)
        {
            ChatService = _chatService;
            dialogs = _dialogs;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            var data = initData as Tuple<Greeting, string>;

            UserName = data.Item1.Username;
            FileName = data.Item1.FileName;
            GroupName = data.Item2;

            var messageGreeting = new PhotoUrlMessage(UserName);
            if (!string.IsNullOrEmpty(FileName))
            {
                messageGreeting.Url = FileName;
                var message = new UserConnectedMessage(UserName, GroupName);
                await ChatService.JoinChannelAsync(message);
                Messages.Add(messageGreeting);
            }
            var user = await ChatService.GetUsersGroup(GroupName);
            Users = new ObservableCollection<User>(user);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            ChatService.OnReceivedMessage += ChatService_OnReceivedMessage;
        }

        private void ChatService_OnReceivedMessage(object sender, Core.EventHandlers.MessageEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Messages.All(x => x.Id != e.Message.Id))
                {
                    if (e.Message.TypeInfo.Name == nameof(UserConnectedMessage))
                    {
                        var users = await ChatService.GetUsersGroup(GroupName);
                        Users = new ObservableCollection<User>(users);
                    }
                    if (e.Message.TypeInfo.Name != nameof(UserConnectedMessage))
                    {
                        var user = Users.FirstOrDefault(x => x.id ==
                        e.Message.Sender);

                        e.Message.Color = user.Color;
                        e.Message.Avatar = user.Avatar;
                    }
                    Messages.Add(e.Message);
                    
                }
            });
        }

        protected override async void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            ChatService.OnReceivedMessage -= ChatService_OnReceivedMessage;
            var message = new UserConnectedMessage(UserName, GroupName);
            await ChatService.LeaveChannelAsync(message);
        }
    }
}
