using Microsoft.EntityFrameworkCore;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class SupportTeamMemberService : ISupportTeamMemberService
    {
        private readonly RacePRN232Context _context;

        public SupportTeamMemberService(RacePRN232Context context)
        {
            _context = context;
        }

        public IEnumerable<SupportTeamMemberDTO> GetAll()
        {
            return _context.SupportTeamMembers
                .Include(m => m.User)
                .Select(m => new SupportTeamMemberDTO
                {
                    SupportTeamMemberId = m.SupportTeamMemberId,
                    SupportTeamId = m.SupportTeamId,
                    UserId = m.UserId,
                    UserFullName = m.User.FullName,
                    RoleInTeam = m.RoleInTeam,
                    JoinDate = m.JoinDate,
                    IsLeader = m.IsLeader
                }).ToList();
        }

        public IEnumerable<SupportTeamMemberDTO> GetByTeamId(int teamId)
        {
            return _context.SupportTeamMembers
                .Include(m => m.User)
                .Where(m => m.SupportTeamId == teamId)
                .Select(m => new SupportTeamMemberDTO
                {
                    SupportTeamMemberId = m.SupportTeamMemberId,
                    SupportTeamId = m.SupportTeamId,
                    UserId = m.UserId,
                    UserFullName = m.User.FullName,
                    RoleInTeam = m.RoleInTeam,
                    JoinDate = m.JoinDate,
                    IsLeader = m.IsLeader
                }).ToList();
        }

        public SupportTeamMemberDTO? GetById(int id)
        {
            var member = _context.SupportTeamMembers.Include(m => m.User).FirstOrDefault(m => m.SupportTeamMemberId == id);
            if (member == null) return null;

            return new SupportTeamMemberDTO
            {
                SupportTeamMemberId = member.SupportTeamMemberId,
                SupportTeamId = member.SupportTeamId,
                UserId = member.UserId,
                UserFullName = member.User.FullName,
                RoleInTeam = member.RoleInTeam,
                JoinDate = member.JoinDate,
                IsLeader = member.IsLeader
            };
        }

        public SupportTeamMemberDTO Create(CreateSupportTeamMemberDTO dto)
        {
            var member = new SupportTeamMember
            {
                SupportTeamId = dto.SupportTeamId,
                UserId = dto.UserId,
                RoleInTeam = dto.RoleInTeam,
                IsLeader = dto.IsLeader,
                JoinDate = DateTime.Now
            };

            _context.SupportTeamMembers.Add(member);
            _context.SaveChanges();

            var user = _context.Users.Find(dto.UserId);

            return new SupportTeamMemberDTO
            {
                SupportTeamMemberId = member.SupportTeamMemberId,
                SupportTeamId = member.SupportTeamId,
                UserId = member.UserId,
                UserFullName = user?.FullName ?? "",
                RoleInTeam = member.RoleInTeam,
                JoinDate = member.JoinDate,
                IsLeader = member.IsLeader
            };
        }

        public bool Update(int id, UpdateSupportTeamMemberDTO dto)
        {
            var member = _context.SupportTeamMembers.Find(id);
            if (member == null) return false;

            member.RoleInTeam = dto.RoleInTeam;
            member.IsLeader = dto.IsLeader;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var member = _context.SupportTeamMembers.Find(id);
            if (member == null) return false;

            // Không cho xóa leader
            if (member.IsLeader == true)
                throw new InvalidOperationException("Không thể xóa Leader khỏi đội hỗ trợ.");

            _context.SupportTeamMembers.Remove(member);
            _context.SaveChanges();
            return true;
        }
    }
}
