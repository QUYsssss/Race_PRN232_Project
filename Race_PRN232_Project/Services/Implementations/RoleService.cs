using Race_PRN232_Project.DTOs.RoleDTO;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RacePRN232Context _context;

        public RoleService(RacePRN232Context context)
        {
            _context = context;
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            return _context.Roles.Select(r => new RoleDTO
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                Description = r.Description
            }).ToList();
        }

        public RoleDTO? GetById(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return null;

            return new RoleDTO
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description
            };
        }

        public RoleDTO Create(CreateRoleDTO dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName,
                Description = dto.Description
            };
            _context.Roles.Add(role);
            _context.SaveChanges();

            return new RoleDTO
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description
            };
        }

        public bool Update(int id, UpdateRoleDTO dto)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return false;

            role.RoleName = dto.RoleName;
            role.Description = dto.Description;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
                return false;

            // 🔒 Kiểm tra role có đang được sử dụng không
            bool isUsed = _context.Users.Any(u => u.RoleId == id);
            if (isUsed)
                throw new InvalidOperationException("Không thể xóa vai trò này vì đang được sử dụng bởi người dùng khác.");

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }

    }
}
