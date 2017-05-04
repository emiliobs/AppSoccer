﻿using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Models
{
    public class Team
    {
        [PrimaryKey]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Initials { get; set; }
        public int LeagueId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<User> Fans { get; set; }
        public string FullLogo
        {
            get
            {
                if (string.IsNullOrEmpty(Logo))
                {
                    return "avatar_shield.png";
                }

                return $"http://soccerbackend.azurewebsites.net{Logo.Substring(1)}";
            }
        }

        public override int GetHashCode()
        {
            return TeamId;
        }

    }
}
