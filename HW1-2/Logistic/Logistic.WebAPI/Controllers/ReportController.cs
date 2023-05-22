using Logistic.Core;
using Logistic.DAL;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService<Vehicle, int> reportService;
        private readonly VehicleService vehicleService;

        public ReportController(ReportService<Vehicle, int> reportService, VehicleService vehicleService)
        {
            this.reportService = reportService;
            this.vehicleService = vehicleService;
        }
        [HttpPost("CreateReport")]
        public IActionResult Create(string type)
        {
            try
            {
                Enum.TryParse(type, out ReportType reportType);
                var entities = vehicleService.GetAll();

                if (entities.Count > 0)
                {
                    return Ok(reportService.CreateReport(entities, reportType));
                }
                return BadRequest("No vehicles");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ReadReport")]
        public IActionResult Read(string filename)
        {
            try
            {
                List<Vehicle> list = reportService.LoadReport(filename);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}