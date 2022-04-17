﻿using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.DbModels
{
    public partial class User
    {
        public User()
        {
            ChatMembers = new HashSet<ChatMember>();
            FriendFriendNavigations = new HashSet<Friend>();
            FriendUserUsers = new HashSet<Friend>();
            Likes = new HashSet<Like>();
            Messages = new HashSet<Message>();
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public sbyte? IsAdmin { get; set; }
        public string Description { get; set; }
        public string ImageReference { get; set; }

        public virtual ICollection<ChatMember> ChatMembers { get; set; }
        public virtual ICollection<Friend> FriendFriendNavigations { get; set; }
        public virtual ICollection<Friend> FriendUserUsers { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
