namespace Race_PRN232_Project.DTOs.SupportTeamDTO
{
    public class SupportTeamDTO
    {
        public int SupportTeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public int RaceId { get; set; }
        public int? LeaderId { get; set; }
        public string? LeaderName { get; set; }
        public string? ContactPhone { get; set; }
    }

    public class CreateSupportTeamDTO
    {
        public string TeamName { get; set; } = null!;
        public int RaceId { get; set; }
        public int? LeaderId { get; set; }
        public string? ContactPhone { get; set; }
    }

    public class UpdateSupportTeamDTO
    {
        public string TeamName { get; set; } = null!;
        public int? LeaderId { get; set; }
        public string? ContactPhone { get; set; }
    }
}
