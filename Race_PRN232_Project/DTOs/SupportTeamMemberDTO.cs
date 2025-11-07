namespace Race_PRN232_Project.DTOs
{
    public class SupportTeamMemberDTO
    {
        public int SupportTeamMemberId { get; set; }
        public int SupportTeamId { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; } = "";
        public string RoleInTeam { get; set; } = "";
        public DateTime? JoinDate { get; set; }
        public bool? IsLeader { get; set; }
    }

    public class CreateSupportTeamMemberDTO
    {
        public int SupportTeamId { get; set; }
        public int UserId { get; set; }
        public string RoleInTeam { get; set; } = "";
        public bool? IsLeader { get; set; }
    }

    public class UpdateSupportTeamMemberDTO
    {
        public string RoleInTeam { get; set; } = "";
        public bool? IsLeader { get; set; }
    }
}
