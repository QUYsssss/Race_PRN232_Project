using System;
using System.Collections.Generic;

namespace Race_PRN232_Project.Models
{
    public partial class User
    {
        public User()
        {
            RaceParticipants = new HashSet<RaceParticipant>();
            SupportTeamMembers = new HashSet<SupportTeamMember>();
            SupportTeams = new HashSet<SupportTeam>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool? EmailConfirmed { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? Phone { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public string? Avatar { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<RaceParticipant> RaceParticipants { get; set; }
        public virtual ICollection<SupportTeamMember> SupportTeamMembers { get; set; }
        public virtual ICollection<SupportTeam> SupportTeams { get; set; }
    }
}
