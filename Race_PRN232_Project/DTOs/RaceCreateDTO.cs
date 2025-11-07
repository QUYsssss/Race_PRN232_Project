namespace Race_PRN232_Project.DTOs
{
    public class RaceCreateDTO
    {
        public string RaceName { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LocationId { get; set; }
        public string? Description { get; set; }

        // Optional details
        public List<RaceDistanceDTO>? Distances { get; set; }
        public List<ImageDTO>? Images { get; set; }
    }

    public class RaceDistanceDTO
    {
        public decimal DistanceKm { get; set; }
        public decimal CutoffTimeHours { get; set; }
    }

    public class ImageDTO
    {
        public string Url { get; set; } = "";
        public string? Caption { get; set; }
    }
}
