using System;
using VideoUploaderWebApp.Models.ViewModels;
using VideoUploaderWebApp.Models;

namespace VideoUploaderWebApp.Repository
{
    public class UploaderRepo
    {
        private readonly videouploader_dbEntities _context;

        public UploaderRepo()
        {
            _context = new videouploader_dbEntities();
        }

        public bool uploadVideo(ValidateVideoInformation videoInformation)
        {
            try
            {
                var video = new VideoInformation()
                {
                    VideoTitle = videoInformation.VideoTitle,
                    About = videoInformation.About,
                    Tags = videoInformation.Tags,
                    Language = videoInformation.Language,
                    Category = string.Join(",", videoInformation.Category),
                    VideoLocation = videoInformation.VideoLocation,
                    UserID = videoInformation.UserID,
                    UploadedOn = DateTime.Now.ToString(),
                };

                _context.VideoInformations.Add(video);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}