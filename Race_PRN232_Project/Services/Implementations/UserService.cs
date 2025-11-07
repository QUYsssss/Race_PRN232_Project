using AutoMapper;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly RacePRN232Context _context;
        private readonly IMapper _mapper;

        public UserService(RacePRN232Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var data = from u in _context.Users
                       join r in _context.Roles on u.RoleId equals r.RoleId
                       select new UserDTO
                       {
                           UserId = u.UserId,
                           FullName = u.FirstName + " " + u.LastName,
                           Email = u.Email,
                           RoleName = r.RoleName,
                           Avatar = u.Avatar
                       };
            return data.ToList();
        }

        public UserDTO? GetById(int id)
        {
            var user = _context.Users.Find(id);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public bool Create(UserDTO dto)
        {
            var entity = new User
            {
                FirstName = dto.FullName.Split(' ')[0],
                LastName = dto.FullName.Split(' ').Last(),
                Email = dto.Email,
                PasswordHash = "User@123", // default
                RoleId = 2
            };
            _context.Users.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Update(UserDTO dto)
        {
            var entity = _context.Users.Find(dto.UserId);
            if (entity == null) return false;
            entity.Email = dto.Email;
            entity.Avatar = dto.Avatar;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
