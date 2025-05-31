using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public ActionResult CreateShift([FromBody] Shift shift)
        {
            _shiftService.AddShift(shift);
            return Ok("Added succesfully!");
        }

        [HttpPut]
        public ActionResult EditShift([FromBody] Shift shift)
        {
            _shiftService.UpdateShift(shift);
            return Ok("Edited Succesfully!");
        }

        [HttpDelete]
        public ActionResult DeleteShift(int id)
        {
            _shiftService.DeleteShift(id);
            return Ok("Removed Succesfully");
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var shifts = await _shiftService.GetShifts();
            if (shifts.IsNullOrEmpty())
            {
                return NotFound("No shifts added yet!");
            }
            return Ok(shifts);
        }
    }
}