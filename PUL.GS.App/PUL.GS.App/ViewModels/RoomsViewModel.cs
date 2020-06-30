//using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.Core.Services;
using PUL.GS.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PUL.GS.App.ViewModels
{
    public class RoomsViewModel: FreshBasePageModel
    {
        IChatService ChatService;
        //IUserDialogs dialogs;
        bool IsBusy = false;
        string UserName;

        public List<Room> Rooms { get; set; }
        public Room CurrentRoom { get; set; }
        public ICommand EnterRoomCommand { get; set; }

        public RoomsViewModel(
            //IUserDialogs _dialogs,
            IChatService _chatService)
        {
            ChatService = _chatService;
            //dialogs = _dialogs;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            UserName = initData as string;

            EnterRoomCommand = new Command(async () =>
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    if (CurrentRoom != null)
                    {
                        Tuple<string, string> data =
                        new Tuple<string, string>(UserName, CurrentRoom.Name);
                        await CoreMethods.PushPageModel<ChatViewModel>(data);
                        CurrentRoom = null;
                    }

                    IsBusy = false;
                }
            });
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            
            //dialogs.ShowLoading("Cargando");

            await ChatService.InitAsync(UserName);

            Rooms = await ChatService.GetRooms();

            //dialogs.HideLoading();
        }
    }
}
