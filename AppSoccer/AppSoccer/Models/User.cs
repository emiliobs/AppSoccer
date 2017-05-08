﻿using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Models
{
    public class User
    {
        [PrimaryKey]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserTypeId { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public int FavoriteTeamId { get; set; }
        public int Points { get; set; }

        [ManyToOne]
        public UserType UserType { get; set; }

        [ManyToOne]
        public Team FavoriteTeam { get; set; }
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public DateTime TokenExpires { get; set; }

        public string Password { get; set; }

        public bool IsRemembered { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }      
        public string FullPicture
        {
            get
            {
                if (string.IsNullOrEmpty(Picture))
                {
                    return "avatar_user.png";
                }

                return $"http://soccerapi.azurewebsites.net/{Picture.Substring(1)}";
            }
        }

        public byte[] ImageArray { get; internal set; }

        public override int GetHashCode()
        {
            return UserId;
        }

    }
}
