using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class Race
    {
        public Race()
        {
            Images = new HashSet<Image>();
            RaceDistances = new HashSet<RaceDistance>();
            RaceParticipants = new HashSet<RaceParticipant>();
            SupportTeams = new HashSet<SupportTeam>();
        }

        public int RaceId { get; set; }
        public string RaceName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LocationId { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual Location? Location { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<RaceDistance> RaceDistances { get; set; }
        public virtual ICollection<RaceParticipant> RaceParticipants { get; set; }
        public virtual ICollection<SupportTeam> SupportTeams { get; set; }
    }
}
