using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FaceSpam_social_media.SignalRHub
{
    public class MessageHub : Hub
    {
        public async Task Send(int messageId, string message, string userName, string image, int selectedChat)
        {
            await this.Clients.All.SendAsync("Send", messageId, message, userName, image, selectedChat);
        }

        public async Task Remove(int messageId)
        {
            await this.Clients.All.SendAsync("Remove", messageId);
        }

    }
}