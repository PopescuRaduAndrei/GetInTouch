using GetInTouch.Logic.ViewModels.User;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.Infrastructure
{
    public interface IUserLogic
    {
        UserModel GetUserFromIdentity(string userId);
        void Register(string id, string firstName, string lastName, string email, int year, int month, int day);
        void UpdateProfile(UserDetailsViewModel vm);
        UserModel Get(Guid userId);
        IEnumerable<string> GetAllUserImages(Guid userId);
        void SetProfilePicture(Guid userId, string selectedImage);
        void AddToCloseFriends(Guid userId, Guid id);
        void RemoveFromCloseFriends(Guid userId, Guid id);
        bool CheckCloseFriends(Guid id, Guid userId);
        bool CheckFollowing(Guid userId, Guid id);
        void Follow(Guid userId, Guid id);
        void Unfollow(Guid userId, Guid id);
    }
}
