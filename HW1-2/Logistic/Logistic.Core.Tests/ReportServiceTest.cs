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
        private readonly ReportService<Warehouse, int> _warehouseReportService;
        private readonly IReportingRepository<Warehouse, int> _warehouseReportingRepository;

        public ReportServiceTest()
        {
             _vehicleReportingRepository = Substitute.For<IReportingRepository<Vehicle,int>>();

            _vehicleReportService = new ReportService<Vehicle,int>(_vehicleReportingRepository, _vehicleReportingRepository);

             _warehouseReportingRepository = Substitute.For<IReportingRepository<Warehouse, int>>();

            _warehouseReportService = new ReportService<Warehouse, int>(_warehouseReportingRepository, _warehouseReportingRepository);
        }

        //[Theory]
        //[InlineData(typeof(Vehicle), ReportType.xml)]
        //[InlineData(typeof(Vehicle), ReportType.json)]
        //[InlineData(typeof(Warehouse), ReportType.xml)]
        //[InlineData(typeof(Warehouse), ReportType.json)]
        //public void GenericReport_Default_CallExpectedMethods(Type modelType, ReportType reportType)
        //{
        //    //Arrange
        //    var fixture = new Fixture();

        //    IList list = ListFromType(modelType);
        //    ;
        //    var list1 = fixture.Create < List < modelType.GetType() >> ();
        //    var list2 = fixture.Create<List<modelType>>();

        //    ////Act
        //    _vehicleReportService.CreateReport(list, reportType);

        //    //Assert
        //    _vehicleReportingRepository.Received(1).Create(Arg.Any<List<Vehicle>>());
        //}
        [Theory]
        [InlineData(ReportType.xml)]
        [InlineData(ReportType.json)]
        public void CreateVehicleReport_Default_CallExpectedMethods(ReportType reportType)
        {
            //Arrange
            var fixture = new Fixture();
            var list = fixture.Create<List<Vehicle>>();

            //Act
            _vehicleReportService.CreateReport(list, reportType);

            //Assert
            _vehicleReportingRepository.Received(1).Create(Arg.Any<List<Vehicle>>());
        }

        //з приводу  Warehouse, хоча в домашці і не було функціональних вимог стоврювати репорти Warehouse,
        //    але в програмі все ж таки дженерік, тому тести написано
        [Theory]
        [InlineData(ReportType.xml)]
        [InlineData(ReportType.json)]
        public void CreateWarehouseReport_Default_CallExpectedMethods(ReportType reportType)
        {
            //Arrange
            var fixture = new Fixture();
            var list = fixture.Create<List<Warehouse>>();

            //Act
            _warehouseReportService.CreateReport(list, reportType);

            //Assert
            _warehouseReportingRepository.Received(1).Create(Arg.Any<List<Warehouse>>());
        }
        //я так розумію. в цих тестах перевіряємо лише виклик, а ось читання і збіг сутностей будемо в конкретних
        //реалізація, джсон та хмл
        [Theory]
        [InlineData(".xml")]
        [InlineData(".json")]
        public void LoadVehicleReport_Default_CallExpectedMethods(string extension)
        {
            //Act
            _vehicleReportService.LoadReport(Path.Combine("Resources", "Vehicle_08_04_2023_19_39"+extension));
            //Assert
            _vehicleReportingRepository.Received(1).Read(Arg.Any<string>());
            
        }


        [Theory]
        [InlineData(".xml")]
        [InlineData(".json")]
        public void LoadWarehouseReport_Default_CallExpectedMethods(string extension)
        {
            //Act
            _vehicleReportService.LoadReport(Path.Combine("Resources", "Warehouse_08_04_2023_19_39" + extension));
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