using Race_PRN232_Project.DTOs;

namespace Race_PRN232_Project.Services.Interfaces
{
    public interface IRaceService
    {
        IEnumerable<RaceDTO> GetAll();
        RaceDTO? GetById(int id);
        bool CreateRace(RaceCreateDTO dto);
        bool Create(RaceDTO dto);
        bool Update(RaceDTO dto);
        bool Delete(int id);
    }
}
