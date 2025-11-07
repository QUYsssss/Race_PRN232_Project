using Race_PRN232_Project.DTOs.RoleDTO;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetAll();
        RoleDTO? GetById(int id);
        RoleDTO Create(CreateRoleDTO dto);
        bool Update(int id, UpdateRoleDTO dto);
        bool Delete(int id);
    }
}
