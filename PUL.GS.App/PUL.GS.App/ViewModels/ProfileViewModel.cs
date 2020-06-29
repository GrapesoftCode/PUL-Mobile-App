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
            CurrentUser = new UserFacebook()
            {
                Id = CurrentUser.Id,
                Name = CurrentUser.Name,
                Picture = CurrentUser.Picture,
                //Birthday = FacebookAuth.User.Birthday
            };
        }
    }
}
