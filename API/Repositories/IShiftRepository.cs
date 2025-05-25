using API.Models;

namespace API.Services;

public interface IShiftRepository
{
    IQueryable<Shift> GetAll();

    Task Add(Shift shift);

    Task Update(Shift shift);

    Task Delete(Shift shift);

    Task SaveChanges();
}