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
        public delegate IReportingRepository<Vehicle, int> ReportResolver(ReportType serviceType);

        public ReportController(ReportService<Vehicle, int> rs, VehicleService vs)
        {
            reportService = rs;
            vehicleService = vs;
        }
        [HttpPost("CreateReport")]
        public string Create(string type/*ReportType reportType*/)
        {
            Enum.TryParse(type, out ReportType reportType);
            var entities = vehicleService.GetAll();

            if (entities.Count > 0)
            {
                return reportService.CreateReport(entities, reportType);
            }
            return "Error";
        }
        [HttpGet("ReadReport")]
        public string Read(string filename)
        {
            try
            {
                List<Vehicle> list = reportService.LoadReport(filename);
                string res="";
                foreach (var item in list)
                  res+=item.ToString();

                return res;
            }
            catch (Exception)
            {
                return "Error";
            }
        }
       
    }
}