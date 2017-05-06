using AppSoccer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.ViewModels
{
    public class EditPredictionViewModel   : Match
    {
        #region Attributes
        private Match match;
        #endregion

        #region Constructor
        public EditPredictionViewModel(Match match)
        {
            this.match = match;

            DateId = match.DateId;
            DateTime = match.DateTime;
            Local = match.Local;
            LocalGoals = match.LocalGoals;
            LocalId = match.LocalId;
            MatchId = match.MatchId;
            StatusId = match.StatusId;
            TournamentGroupId = match.TournamentGroupId;
            Visitor = match.Visitor;
            VisitorGoals = match.VisitorGoals;
            VisitorId = match.VisitorId;
            WasPredicted = match.WasPredicted;
        } 
        #endregion
    }
}
