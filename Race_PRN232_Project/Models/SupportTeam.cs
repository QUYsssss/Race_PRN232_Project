using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class SupportTeam
    {
        public SupportTeam()
        {
            SupportTeamMembers = new HashSet<SupportTeamMember>();
        }

        public int SupportTeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public int RaceId { get; set; }
        public int? LeaderId { get; set; }
        public string? ContactPhone { get; set; }

        public virtual User? Leader { get; set; }
        public virtual Race Race { get; set; } = null!;
        public virtual ICollection<SupportTeamMember> SupportTeamMembers { get; set; }
    }
}
