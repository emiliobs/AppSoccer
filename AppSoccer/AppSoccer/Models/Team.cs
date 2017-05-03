using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Initials { get; set; }
        public int LeagueId { get; set; }
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


    }
}
