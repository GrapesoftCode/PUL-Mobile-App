using Acr.UserDialogs;
using FreshMvvm;
using PUL.GS.App.Configuration;
using PUL.GS.Models;

namespace PUL.GS.App.ViewModels
{
    public class ProfileViewModel: FreshBasePageModel
    {
        public UserFacebook CurrentUser { get; set; } = new UserFacebook();

        public ProfileViewModel()
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var user = initData as UserFacebook;

            CurrentUser = new UserFacebook()
            {
                Id = user.Id,
                Name = user.Name,
                Picture = user.Picture,
                //Birthday = FacebookAuth.CurrentUser.User.Birthday
            };

        }
    }
}
