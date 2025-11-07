using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class RaceDistance
    {
        public RaceDistance()
        {
            RaceParticipants = new HashSet<RaceParticipant>();
        }

        public int RaceDistanceId { get; set; }
        public int RaceId { get; set; }
        public decimal DistanceKm { get; set; }
        public decimal? CutoffTimeHours { get; set; }

        public virtual Race Race { get; set; } = null!;
        public virtual ICollection<RaceParticipant> RaceParticipants { get; set; }
    }
}
