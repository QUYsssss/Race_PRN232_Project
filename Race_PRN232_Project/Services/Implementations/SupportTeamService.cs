using AutoMapper;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class SupportTeamService : ISupportTeamService
    {
        private readonly RacePRN232Context _context;
        private readonly IMapper _mapper;

        public SupportTeamService(RacePRN232Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<SupportTeamDTO> GetAll()
        {
            var data = from t in _context.SupportTeams
                       join r in _context.Races on t.RaceId equals r.RaceId
                       select new SupportTeamDTO
                       {
                           SupportTeamId = t.SupportTeamId,
                           TeamName = t.TeamName,
                           RaceName = r.RaceName,
                           ContactPhone = t.ContactPhone
                       };
            return data.ToList();
        }

        public bool AddMember(int teamId, int userId, string role)
        {
            if (_context.SupportTeamMembers.Any(x => x.SupportTeamId == teamId && x.UserId == userId))
                return false;

            _context.SupportTeamMembers.Add(new SupportTeamMember
            {
                SupportTeamId = teamId,
                UserId = userId,
                RoleInTeam = role
            });
            _context.SaveChanges();
            return true;
        }

        public bool RemoveMember(int teamId, int userId)
        {
            var member = _context.SupportTeamMembers
                .FirstOrDefault(x => x.SupportTeamId == teamId && x.UserId == userId);
            if (member == null) return false;
            _context.SupportTeamMembers.Remove(member);
            _context.SaveChanges();
            return true;
        }
    }
}
