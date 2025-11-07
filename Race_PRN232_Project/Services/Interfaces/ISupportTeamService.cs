using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.DTOs.SupportTeamDTO;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface ISupportTeamService
    {
        IEnumerable<SupportTeamDTO> GetAll();
        SupportTeamDTO? GetById(int id);
        SupportTeamDTO Create(CreateSupportTeamDTO dto);
        bool Update(int id, UpdateSupportTeamDTO dto);
        bool Delete(int id);
    }
}
