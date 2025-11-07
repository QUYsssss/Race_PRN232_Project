using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class RaceRegistrationService : IRaceRegistrationService
    {
        private readonly RacePRN232Context _context;
        public RaceRegistrationService(RacePRN232Context context)
        {
            _context = context;
        }

        public void Register(int userId, RaceRegistrationDto dto)
        {
            if (dto.RoleInRace != "Runner" && dto.RoleInRace != "Support")
                throw new Exception("Vai trò phải là 'Runner' hoặc 'Support'");

            // Kiểm tra giải có tồn tại không
            var race = _context.Races.FirstOrDefault(r => r.RaceId == dto.RaceId);
            if (race == null)
                throw new Exception("Giải đấu không tồn tại.");

            // 🔹 Kiểm tra xem user đã đăng ký vai trò nào khác trong cùng giải chưa
            bool alreadyRunner = _context.RaceParticipants
                .Any(rp => rp.UserId == userId && rp.RaceId == dto.RaceId);
            bool alreadySupport = _context.SupportTeamMembers
                .Any(stm => stm.UserId == userId && stm.SupportTeam.RaceId == dto.RaceId);

            if (alreadyRunner || alreadySupport)
                throw new Exception("Bạn đã tham gia giải này với vai trò khác.");

            // 🔸 Nếu là Runner → thêm vào RaceParticipant
            if (dto.RoleInRace == "Runner")
            {
                var participant = new RaceParticipant
                {
                    RaceId = dto.RaceId,
                    UserId = userId,
                    DistanceId = dto.DistanceId,
                    RegisterDate = DateTime.UtcNow
                };

                _context.RaceParticipants.Add(participant);
            }
            // 🔸 Nếu là Support → thêm vào SupportTeamMember
            else if (dto.RoleInRace == "Support")
            {
                if (dto.SupportTeamId == null)
                    throw new Exception("Cần chọn SupportTeamId khi đăng ký làm Support.");

                var member = new SupportTeamMember
                {
                    SupportTeamId = dto.SupportTeamId.Value,
                    UserId = userId,
                    RoleInTeam = "Supporter",
                    JoinDate = DateTime.UtcNow,
                    IsLeader = false
                };

                _context.SupportTeamMembers.Add(member);
            }

            _context.SaveChanges();
        }

    }
}
