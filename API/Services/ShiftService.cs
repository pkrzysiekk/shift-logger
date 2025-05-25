using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace API.Services;

public class ShiftService : IShiftService
{
    public readonly IShiftRepository _repository;

    public ShiftService(IShiftRepository repository)
    {
        _repository = repository;
    }

    public void AddShift(Shift shift)
    {
        _repository.Add(shift);
    }

    public async Task DeleteShift(Shift shift)
    {
        await _repository.Delete(shift);
    }

    public async Task<IEnumerable<Shift>> GetShifts()
    {
        return await _repository.GetAll().ToListAsync();
    }

    public async Task UpdateShift(Shift shift)
    {
        await _repository.Update(shift);
    }
}