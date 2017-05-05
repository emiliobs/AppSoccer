using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public List<Group> Groups { get; set; }
        public List<Date> Dates { get; set; }
        public string FullLogo
        {
            get
            {
                           
                    if (string.IsNullOrEmpty(Logo))
                    {
                        return "avatar_tournament.png";
                    }

                    return $"http://soccerbackend.azurewebsites.net{Logo.Substring(1)}";
            }             
        }

    }
}
