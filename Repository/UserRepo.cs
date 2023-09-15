using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoUploaderWebApp.Models;
using VideoUploaderWebApp.Models.ViewModels;

namespace VideoUploaderWebApp.Repository
{
    public class UserRepo
    {
        private readonly videouploader_dbEntities _context;

        public UserRepo()
        {
            _context = new videouploader_dbEntities();           
        }

        public bool ValidateUser(ValidateUserProfiles user, out string Username, out string UserID)
        {
            Username = "";
            UserID = "";
            var checkUser = _context.Users.SingleOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (checkUser != null)
            {
                Username = checkUser.Username;
                UserID = checkUser.UserID.ToString();
                return true;
            }                
            return false;
        }
    }
}