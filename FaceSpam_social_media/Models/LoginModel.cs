﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Models
{
    public class LoginModel
    {
        public IRepository _repository;

        public LoginModel()
        {
        }
        [Required(ErrorMessage = "Enter login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Verify(string name, string password)
        {
            bool result = _repository.GetAll<User>().Any(x => x.Name == name && x.Password == password);
            Login = name;
            Password = password;
            return result;
        }

    }

}
