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
            var races = _context.Races
                  .Where(r => !r.IsDeleted)
                  .ToList();

            return _mapper.Map<IEnumerable<RaceDTO>>(races);
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
            var race = _context.Races.Find(id);
            if (race == null) return false;

            race.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public bool CreateRace(RaceCreateDTO dto)
        {
            // 1️⃣ Tạo đối tượng Race
            var race = new Race
            {
                RaceName = dto.RaceName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                LocationId = dto.LocationId,
                Description = dto.Description
            };
            _context.Races.Add(race);
            _context.SaveChanges();

            // 2️⃣ Thêm các RaceDistance (nếu có)
            if (dto.Distances != null)
            {
                foreach (var d in dto.Distances)
                {
                    _context.RaceDistances.Add(new RaceDistance
                    {
                        RaceId = race.RaceId,
                        DistanceKm = d.DistanceKm,
                        CutoffTimeHours = d.CutoffTimeHours
                    });
                }
            }

            // 3️⃣ Thêm hình ảnh (nếu có)
            if (dto.Images != null)
            {
                foreach (var img in dto.Images)
                {
                    _context.Images.Add(new Image
                    {
                        RaceId = race.RaceId,
                        Url = img.Url,
                        Caption = img.Caption
                    });
                }
            }

            _context.SaveChanges();
            return true;
        }

    }
}
