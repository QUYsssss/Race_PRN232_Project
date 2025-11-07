namespace Race_PRN232_Project.DTOs
{
    public class TokenResponseDTO
    {
        public string Token { get; set; } = "";
        public DateTime ExpireAt { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
    }
}
