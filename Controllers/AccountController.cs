using System.Web;
using System;
using System.Web.Mvc;
using System.Web.Security;
using VideoUploaderWebApp.Models;
using VideoUploaderWebApp.Models.ViewModels;
using VideoUploaderWebApp.Repository;

namespace VideoUploaderWebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserRepo _userRepo;

        public AccountController()
        {
            _userRepo = new UserRepo();
        }
 
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Upload", "VideoUploader");
            }
            else
            {
                ValidateUserProfiles user = new ValidateUserProfiles();
                return View(user);
            }
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(ValidateUserProfiles user)
        {
            if (ModelState.IsValid)
            {
                string Username = "", UserID = "";
                if (_userRepo.ValidateUser(user, out Username, out UserID))
                {
                    Session["Username"] = Username;
                    Session["UserID"] = UserID;
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Upload", "VideoUploader");
                }
                else
                {
                    TempData["ErrorMsg"] = "Invalid Username or Password";
                }   
            }
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction("Login", "Account");
        }
    }
}