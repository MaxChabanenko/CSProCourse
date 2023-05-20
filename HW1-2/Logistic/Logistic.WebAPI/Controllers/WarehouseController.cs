using Logistic.Core;
using Logistic.DAL;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService warehouseService;

        public WarehouseController(WarehouseService vs)
        {
            warehouseService = vs;
        }
        [HttpPost("Create")]
        public int Read(Warehouse v)
        {
            return warehouseService.Create(v);
        }
        [HttpGet("ReadAll")]
        public IEnumerable<Warehouse> GetAll()
        {
            return warehouseService.GetAll();
        }
        [HttpPut("Update")]
        public int Read(Warehouse newWarehouse, int id)
        {
            return warehouseService.Update(newWarehouse, id);
        }
        [HttpDelete("Delete")]
        public IActionResult Read(int id)
        {
            warehouseService.Delete(id);
            return Ok();
        }
    }
}