using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : ControllerBase
    {
        public readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpPost]
        public ActionResult CreateShift(Shift shift)
        {
            _shiftService.AddShift(shift);
            return Ok("Added succesfully!");
        }
    }
}