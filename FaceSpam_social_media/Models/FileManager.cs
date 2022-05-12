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
        private static async void AddImageToPost(IFormFile file)
        {
            string path = "./wwwroot/Images/" + file.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                fileStream.Close();
            }
        }

        public static string UploadImage(IFormFile file)
        {
            string image_ref = null;
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension != ".jpg" && extension != ".png")
                {
                    return image_ref;
                }
                image_ref = "../Images/" + file.FileName;
                AddImageToPost(file);
            }
            return image_ref;
        }
    }
}
