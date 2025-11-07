namespace Race_PRN232_Project.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Avatar { get; set; }
        public string? RoleName { get; set; }
    }
}
