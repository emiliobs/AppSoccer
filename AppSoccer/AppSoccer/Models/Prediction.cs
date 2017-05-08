using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Models
{
    public class Prediction
    {
        public int PredictionId { get; set; }
        public int UserId { get; set; }
        public int MachId { get; set; }
        public int LocalGoals { get; set; }
        public int VisitorGoals { get; set; }
        public int Points { get; set; }

        public override int GetHashCode()
        {
            return PredictionId;
        }
    }
}
