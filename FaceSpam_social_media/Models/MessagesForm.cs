﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media.Models
{
    public class MessagesForm
    {
        public User user = new User();
        public List<Chat> chats = new List<Chat>();
        public List<User> members = new List<User>();
        public List<Message> chatMessages = new List<Message>();
        public Chat selectedChat = new Chat();

        public string message { get; set; }
        public int currentChat { get; set; }
        public Chat GetChatMessages(mydbContext context, int chatId)
        {
            if (chatId != 0)
            {
                currentChat = chatId;
                chatMessages = context.Messages.Where(x => x.ChatChatId == currentChat)
                    .Include(x => x.UserUser).ToList();
                selectedChat = chats.Where(x => x.ChatId == chatId).First();
                members = context.ChatMembers.Where(x => x.ChatChatId == currentChat).Select(y => y.UserUser).ToList();
                return chats.Where(x => x.ChatId == currentChat).First();
            }
            return new Chat();
        }

        public int AddMember(mydbContext context, int memberId)
        {
            ChatMember newMember = new ChatMember
            {
                ChatChatId = selectedChat.ChatId,
                UserUserId = memberId
            };
            context.Add(newMember);
            context.SaveChanges();
            members.Add(context.Users.Where(x => x.UserId == memberId).First());
            return members.Count;
        }

        public int RemoveChatMember(mydbContext context, int memberId)
        {
            User member = members.Where(x => x.UserId == memberId).First();
            ChatMember remove = context.ChatMembers.Where(x => x.UserUserId == memberId 
            && x.ChatChatId == selectedChat.ChatId).First();
            context.ChatMembers.Remove(remove);
            context.SaveChanges();
            members.Remove(member);
            return members.Count;
        }

        public int RemoveMessage(mydbContext context, int messageId)
        {
            int entries = 1;
            Message remove = context.Messages.Where(x => x.MessageId == messageId).First();
            context.Messages.Remove(remove);
            entries = context.SaveChanges();
            chatMessages.Remove(chatMessages.Where(x => x.MessageId == messageId).First());
            return entries;
        }

        public int SendMessage(mydbContext context, string inputMessage)
        {
            Message send = new Message();
            send.Text = inputMessage;
            send.ChatChatId = currentChat;
            send.DateSending = DateTime.Now;
            send.UserUserId = user.UserId;
            context.Messages.Add(send);
            context.SaveChanges();
            chatMessages.Add(send);
            return send.MessageId;
        }

        public void GetChats(mydbContext context)
        {
            chats = context.ChatMembers.Where(x => x.UserUserId == user.UserId)
                .Select(x => x.ChatChat).ToList();
        }

        public void QuitGroup(mydbContext context)
        {
            ChatMember chatMember = context.ChatMembers
                .Where(x => x.ChatChatId == selectedChat.ChatId && x.UserUserId == user.UserId).First();
            context.Remove(chatMember);
            context.SaveChanges();
        }

        public void DeleteGroup(mydbContext context, int Id)
        {
            Chat remove = context.Chats.Where(x => x.ChatId == Id).FirstOrDefault();
            context.Chats.Remove(remove);
            context.SaveChanges();
            chats.Remove(remove);
        }

        public void SelectChat(mydbContext context, int chatId)
        {
            selectedChat = chats.Where(x => x.ChatId == chatId).First();
            GetChatMessages(context, chatId);
        }

        public List<User> SelectAllUsers(mydbContext context)
        {
            List<User> users = context.Users.Where(x => x.UserId != user.UserId).ToList();
            return users;
        }

        public Chat CreateGroup(mydbContext context, string name, string description, List<int> members, string reference)
        {
            Chat created = new Chat()
            {
                ChatName = name,
                Description = description,
                DateCreating = DateTime.Now,
                Admin = user.UserId,
            };
            if (reference != null)
            {
                created.ImageReference = reference;
            }
            else
            {
                created.ImageReference = "../Images/default_group.jpg";
            }
            context.Chats.Add(created);
            context.SaveChanges();
            chats.Add(created);
            foreach (int member in members)
            {
                ChatMember newMember = new ChatMember() 
                { 
                    ChatChatId = created.ChatId, 
                    UserUserId = member 
                };
                context.Add(newMember);
            }
            context.SaveChanges();
            return created;
        }
    }
}
