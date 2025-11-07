using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class RaceParticipant
    {
        public int RaceId { get; set; }
        public int UserId { get; set; }
        public int? DistanceId { get; set; }
        public DateTime? RegisterDate { get; set; }

        public virtual RaceDistance? Distance { get; set; }
        public virtual Race Race { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
