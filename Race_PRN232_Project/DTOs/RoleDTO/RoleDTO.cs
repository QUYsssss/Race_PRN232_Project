namespace Race_PRN232_Project.DTOs.RoleDTO
{
    public class RoleDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CreateRoleDTO
    {
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class UpdateRoleDTO
    {
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
