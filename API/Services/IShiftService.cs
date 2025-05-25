using API.Models;

namespace API.Services;

public interface IShiftService
{
    Task<IEnumerable<Shift>> GetShifts();

    void AddShift(Shift shift);

    Task DeleteShift(Shift shift);

    Task UpdateShift(Shift shift);
}