using Race_PRN232_Project.DTOs;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface ISupportTeamService
    {
        IEnumerable<SupportTeamDTO> GetAll();
        bool AddMember(int teamId, int userId, string role);
        bool RemoveMember(int teamId, int userId);
    }
}
