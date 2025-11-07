namespace Race_PRN232_Project.DTOs
{
    public class RaceDTO
    {
        public int RaceId { get; set; }
        public string RaceName { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public string? LocationName { get; set; }
    }
}
