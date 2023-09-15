using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoUploaderWebApp.Models;
using VideoUploaderWebApp.Models.ViewModels;
using VideoUploaderWebApp.Repository;

namespace VideoUploaderWebApp.Controllers
{
    [Authorize]
    public class VideoUploaderController : Controller
    {
        private readonly UploaderRepo uploaderRepo;
        private List<SelectListItem> languages = new List<SelectListItem>
        {
            new SelectListItem() { Text = "English", Value = "1" },
            new SelectListItem() { Text = "Urdu", Value = "2" },
            new SelectListItem() { Text = "German", Value = "3" },
            new SelectListItem() { Text = "Arabic", Value = "4" },
            new SelectListItem() { Text = "Persian", Value = "5" }
        };

        public VideoUploaderController()
        {
            uploaderRepo = new UploaderRepo();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Upload()
        {
            List<string> videoCategories = new List<string>
            {
                "Lifestyle",
                "Fashion",
                "Services",
                "Gaming",
                "Technology",
                "Cooking",
                "Traveling",
                "Education",
                "Recipes",
                "Comedy",
                "Films",
                "Romance",
                "Action",
                "Biology",
                "Sea",
                "Love",
                "Earth",
                "Space",
                "Disaster",
                "Animals",
                "Music",
                "Sports",
                "Health",
                "Food",
                "Science",
                "Nature",
                "History",
                "Travel",
                "Art",
                "Fitness"
            };

            ViewBag.Languages = languages;
            ViewBag.VideoCategories = videoCategories;
            ValidateVideoInformation information = new ValidateVideoInformation();
            return View(information);
        }

        private string GetUniqueFileName(string fileName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string randomString = Path.GetRandomFileName();
            string uniqueFileName = $"{timestamp}_{randomString}{Path.GetExtension(fileName)}";
            return uniqueFileName;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(ValidateVideoInformation videoInformation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "video/mp4" && file.ContentLength <= 250 * 1024 * 1024)
                        {
                            var uploadDirectory = Server.MapPath("~/UploadedVideos");

                            if (!Directory.Exists(uploadDirectory))
                            {
                                Directory.CreateDirectory(uploadDirectory);
                            }

                            var fileName = GetUniqueFileName(file.FileName);
                            var filePath = Path.Combine(uploadDirectory, fileName);

                            file.SaveAs(filePath);

                            videoInformation.Language = languages.Where(x => x.Value == videoInformation.Language.ToString()).FirstOrDefault().Text;
                            videoInformation.UserID = int.Parse(Session["UserID"].ToString());
                            videoInformation.VideoLocation = filePath;
                            var reas = uploaderRepo.uploadVideo(videoInformation);
                            string baseUrl = $"{HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)}";
                            ModelState.Clear();
                            if (reas)
                                return Json(new { success = true, videourl = baseUrl + "/UploadedVideos/" + fileName });
                            else
                                return Json(new { success = false, message = "Error on uploading video please try again later!" });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Invalid file format or size exceeded." });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "No file was selected. Please select a video file" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Incorrect video information." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}