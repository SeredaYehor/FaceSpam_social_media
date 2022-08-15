using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace FaceSpam_social_media.Models
{
    public class FileManager
    {
        private static void AddImageToPost(IFormFile file)
        {
            string path = "./wwwroot/Images/" + file.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        }

        public static string UploadImage(IFormFile file)
        {
            string image_ref = null;
            if (file != null)
            {
                image_ref = "../Images/" + file.FileName;
                AddImageToPost(file);
            }
            return image_ref;
        }
    }
}
