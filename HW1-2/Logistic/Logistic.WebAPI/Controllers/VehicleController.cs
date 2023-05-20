using Logistic.Core;
using Logistic.DAL;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService vehicleService;

        public VehicleController(VehicleService vs)
        {
            vehicleService = vs;
        }
        [HttpPost("Create")]
        public int Read(Vehicle v)
        {
            return vehicleService.Create(v);
        }
        [HttpGet("ReadAll")]
        public IEnumerable<Vehicle> GetAll()
        {
            return vehicleService.GetAll();
        }
        [HttpPut("Update")]
        public int Read(Vehicle newVehicle, int id)
        {
            return vehicleService.Update(newVehicle, id);
        }
        [HttpDelete("Delete")]
        public IActionResult Read(int id)
        {
            vehicleService.Delete(id);
            return Ok();
        }
        [HttpPut("LoadCargo")]
        public IActionResult LoadCargo(Cargo cargo, int vehicleId)
        {
            vehicleService.LoadCargo(cargo, vehicleId);
            return Ok();
        }
        [HttpPut("UnloadCargo")]
        public IActionResult UnloadCargo(Guid cargoId, int vehicleId)
        {
            vehicleService.UnloadCargo(cargoId, vehicleId);
            return Ok();
        }
    }
}