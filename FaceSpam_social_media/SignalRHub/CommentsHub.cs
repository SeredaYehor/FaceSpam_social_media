using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FaceSpam_social_media.SignalRHub
{
    public class CommentsHub : Hub
    {
        public async Task Send(int messageId, string userName, string image, string text)
        {
            await this.Clients.All.SendAsync("Send", messageId, userName, image, text);
        }

        public async Task Remove(int id)
        {
            await this.Clients.All.SendAsync("Remove", id);
        }
    }
}
