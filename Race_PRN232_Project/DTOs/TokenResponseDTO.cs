namespace Race_PRN232_Project.DTOs
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public DateTime ExpiredAt { get; set; }
    }
}
