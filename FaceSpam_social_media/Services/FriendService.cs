using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Services
{
    public class FriendService : IFriendService
    {
        private readonly IRepository _repository;

        public FriendService(IRepository repository)
        {
            _repository = repository;
        }
        /*public void DeleteFriend(int id)
        {
            Friend friend = new Friend();
            friend = context.Friends
                .Where(x => x.FriendId == id && x.UserUserId == user.Id).FirstOrDefault();

            context.Friends.Remove(friend);
            context.SaveChanges();
            friends.Remove(friends.Where(x => x.Id == id).FirstOrDefault());
        }*/
        /*public async Task<int> AddFriend()
        {
            
        }
        public async Task UpdateFriend()
        {
           
        }*/
    }
}