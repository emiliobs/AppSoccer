using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Models
{

    public class Match
    {
        public int MatchId { get; set; }
        public int DateId { get; set; }
        public DateTime DateTime { get; set; }
        public int LocalId { get; set; }
        public int VisitorId { get; set; }
        public int? LocalGoals { get; set; }
        public int? VisitorGoals { get; set; }
        public int StatusId { get; set; }
        public int TournamentGroupId { get; set; }
        public bool WasPredicted { get; set; }
        public Team Local { get; set; }
        public Team Visitor { get; set; }

        public int? LocalGoals2
        {
            get
            {
                if (WasPredicted)
                {
                    return LocalGoals;
                }

                return null;
            }
        }
        public int? VisitorGoals2
        {
            get
            {
                if (WasPredicted)
                {
                    return VisitorGoals;
                }

                return null;
            }
        }
    }

    //public class Local
    //{
    //    public int TeamId { get; set; }
    //    public string Name { get; set; }
    //    public string Logo { get; set; }
    //    public string Initials { get; set; }
    //    public int LeagueId { get; set; }
    //}

    //public class Visitor
    //{
    //    public int TeamId { get; set; }
    //    public string Name { get; set; }
    //    public string Logo { get; set; }
    //    public string Initials { get; set; }
    //    public int LeagueId { get; set; }
    //}

}
