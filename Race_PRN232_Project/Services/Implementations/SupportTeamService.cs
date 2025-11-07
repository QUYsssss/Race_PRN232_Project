using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.DTOs.SupportTeamDTO;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class SupportTeamService : ISupportTeamService
    {
        private readonly RacePRN232Context _context;

        public SupportTeamService(RacePRN232Context context)
        {
            _context = context;
        }

        public IEnumerable<SupportTeamDTO> GetAll()
        {
            return _context.SupportTeams
                .Include(t => t.Leader)
                .Select(t => new SupportTeamDTO
                {
                    SupportTeamId = t.SupportTeamId,
                    TeamName = t.TeamName,
                    RaceId = t.RaceId,
                    LeaderId = t.LeaderId,
                    LeaderName = t.Leader != null ? $"{t.Leader.FirstName} {t.Leader.LastName}" : null,
                    ContactPhone = t.ContactPhone
                })
                .ToList();
        }

        public SupportTeamDTO? GetById(int id)
        {
            var team = _context.SupportTeams
                .Include(t => t.Leader)
                .FirstOrDefault(t => t.SupportTeamId == id);

            if (team == null) return null;

            return new SupportTeamDTO
            {
                SupportTeamId = team.SupportTeamId,
                TeamName = team.TeamName,
                RaceId = team.RaceId,
                LeaderId = team.LeaderId,
                LeaderName = team.Leader != null ? $"{team.Leader.FirstName} {team.Leader.LastName}" : null,
                ContactPhone = team.ContactPhone
            };
        }

        public SupportTeamDTO Create(CreateSupportTeamDTO dto)
        {
            var team = new SupportTeam
            {
                TeamName = dto.TeamName,
                RaceId = dto.RaceId,
                LeaderId = dto.LeaderId,
                ContactPhone = dto.ContactPhone
            };
            _context.SupportTeams.Add(team);
            _context.SaveChanges();

            return GetById(team.SupportTeamId)!;
        }

        public bool Update(int id, UpdateSupportTeamDTO dto)
        {
            var team = _context.SupportTeams.Find(id);
            if (team == null) return false;

            team.TeamName = dto.TeamName;
            team.LeaderId = dto.LeaderId;
            team.ContactPhone = dto.ContactPhone;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var team = _context.SupportTeams
                .Include(t => t.SupportTeamMembers)
                .FirstOrDefault(t => t.SupportTeamId == id);

            if (team == null) return false;

            if (team.SupportTeamMembers.Any())
                throw new InvalidOperationException("Không thể xóa vì đội hỗ trợ này vẫn còn thành viên.");

            _context.SupportTeams.Remove(team);
            _context.SaveChanges();
            return true;
        }
    }
}
