//using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Configuration;
using PUL.GS.Models;

namespace PUL.GS.App.ViewModels
{
    public class ProfileViewModel: FreshBasePageModel
    {
        public UserFacebook CurrentUserFacebook { get; set; } = new UserFacebook();
        public User CurrentUser { get; set; } = new User();

        public ProfileViewModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            CurrentUser = initData as User;

            //CurrentUser = new User()
            //{
            //    Id = user.Id,
            //    Name = user.Name,
            //    Picture = user.Picture,
            //    //Birthday = FacebookAuth.CurrentUser.User.Birthday
            //};

        }
    }
}
