using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class UserChats
    {
        public List<Chat> chats = new List<Chat>();
        public string message { get; set; }
        public int chatId = 0;

        public int GetChatId(int id)
        {
            int pos = -1;
            for(int counter = 0; counter < chats.Count; counter++)
            {
                if(chats[counter].Id == id)
                {
                    pos = counter;
                    break;
                }
            }
            return pos;
        }
    }
}
