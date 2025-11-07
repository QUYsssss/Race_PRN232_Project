using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class SupportTeamMember
    {
        public int SupportTeamMemberId { get; set; }
        public int SupportTeamId { get; set; }
        public int UserId { get; set; }
        public string RoleInTeam { get; set; } = null!;
        public DateTime? JoinDate { get; set; }
        public bool? IsLeader { get; set; }

        public virtual SupportTeam SupportTeam { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
