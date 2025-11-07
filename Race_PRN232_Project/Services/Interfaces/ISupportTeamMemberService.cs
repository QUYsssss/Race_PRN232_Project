using Race_PRN232_Project.DTOs;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface ISupportTeamMemberService
    {
        IEnumerable<SupportTeamMemberDTO> GetAll();
        IEnumerable<SupportTeamMemberDTO> GetByTeamId(int teamId);
        SupportTeamMemberDTO? GetById(int id);
        SupportTeamMemberDTO Create(CreateSupportTeamMemberDTO dto);
        bool Update(int id, UpdateSupportTeamMemberDTO dto);
        bool Delete(int id);
    }
}
