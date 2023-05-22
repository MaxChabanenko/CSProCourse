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
        public int CreateVehicle(Vehicle v)
        {
            return vehicleService.Create(v);
        }
        [HttpGet("ReadAll")]
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return vehicleService.GetAll();
        }
        [HttpPut("Update")]
        public int UpdateVehicle(Vehicle newVehicle, int id)
        {
            return vehicleService.Update(newVehicle, id);
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteVehicle(int id)
        {
            vehicleService.Delete(id);
            return Ok();
        }
        [HttpPut("LoadCargo")]
        public IActionResult LoadCargo(Cargo cargo, int vehicleId)
        {
            vehicleService.LoadCargo(cargo, vehicleId);
            return Accepted();
        }
        [HttpPut("UnloadCargo")]
        public IActionResult UnloadCargo(Guid cargoId, int vehicleId)
        {
            vehicleService.UnloadCargo(cargoId, vehicleId);
            return Ok();
        }
    }
}