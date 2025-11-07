namespace Race_PRN232_Project.DTOs
{
    public class RaceRegistrationDto
    {
        public int RaceId { get; set; }
        public string RoleInRace { get; set; } = null!; // "Runner" hoặc "Support"
        public int? DistanceId { get; set; } // chỉ dùng nếu chọn Runner
        public int? SupportTeamId { get; set; } // chỉ dùng nếu chọn Support
    }
}
