using AutoMapper;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Models;
using Race_PRN232_Project.Services.Interfaces;

namespace Race_PRN232_Project.Services.Implementations
{
    public class RaceService : IRaceService
    {
        private readonly RacePRN232Context _context;
        private readonly IMapper _mapper;

        public RaceService(RacePRN232Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<RaceDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<RaceDTO>>(_context.Races.ToList());
        }

        public RaceDTO? GetById(int id)
        {
            var race = _context.Races.Find(id);
            return race != null ? _mapper.Map<RaceDTO>(race) : null;
        }

        public bool Create(RaceDTO dto)
        {
            var race = _mapper.Map<Race>(dto);
            _context.Races.Add(race);
            _context.SaveChanges();
            return true;
        }

        public bool Update(RaceDTO dto)
        {
            var entity = _context.Races.Find(dto.RaceId);
            if (entity == null) return false;
            entity.RaceName = dto.RaceName;
            entity.Description = dto.Description;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = _context.Races.Find(id);
            if (entity == null) return false;
            _context.Races.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
