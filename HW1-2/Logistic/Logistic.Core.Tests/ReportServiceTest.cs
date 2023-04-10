using AutoFixture;
using AutoFixture.Xunit2;
using Logistic.Models;
using NSubstitute;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Logistic.Core.Tests
{
    public class ReportServiceTest
    {
        private readonly ReportService<Vehicle,int> _vehicleReportService;
        private readonly IReportingRepository<Vehicle, int> _vehicleReportingRepository;

        public ReportServiceTest()
        {
             _vehicleReportingRepository = Substitute.For<IReportingRepository<Vehicle,int>>();

            _vehicleReportService = new ReportService<Vehicle,int>(_vehicleReportingRepository, _vehicleReportingRepository);
        }

        [Theory]
        [InlineAutoData(ReportType.xml)]
        [InlineAutoData(ReportType.json)]
        public void CreateVehicleReport_WhenDefaultExecution_ShouldCallExpectedMethods(ReportType reportType, List<Vehicle> entities)
        {
            //Act
            _vehicleReportService.CreateReport(entities, reportType);

            //Assert
            _vehicleReportingRepository.Received(1).Create(Arg.Any<List<Vehicle>>());
        }

        [Theory]
        [InlineData(".xml")]
        [InlineData(".json")]
        public void LoadVehicleReport_WhenDefaultExecution_ShouldCallExpectedMethods(string extension)
        {
            //Act
            _vehicleReportService.LoadReport(Path.Combine("Resources", "Vehicle_08_04_2023_19_39"+extension));
            //Assert
            _vehicleReportingRepository.Received(1).Read(Arg.Any<string>());  
        }

        [Fact]
        public void LoadReport_InvalidExtension_ThrowException()
        {
            //Act
            Action act = () => _vehicleReportService.LoadReport(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.doc"));

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Contains("Error occured while trying to read a file", exception.Message);
           
        }
    }
}