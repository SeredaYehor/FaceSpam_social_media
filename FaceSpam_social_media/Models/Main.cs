using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class Main
    {
        public User user;
        public List<Post> posts = new List<Post>();
        public string message { get; set; }
    }
}
