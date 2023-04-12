using AutoFixture;
using Logistic.Models;
using NSubstitute;

namespace Logistic.Core.Tests
{
    public class VehicleServiceTest
    {
        private readonly VehicleService _vehicleService;
        private readonly IRepository<Vehicle> _vehicleRespository;

        public VehicleServiceTest()
        {
            _vehicleRespository = Substitute.For<IRepository<Vehicle>>();
            _vehicleService = new VehicleService(_vehicleRespository);
        }
        [Fact]
        public void LoadCargo_WhenCargoFits_ShouldCallExpectedMethods()
        {
            //Arrange
            int volumeAndWeight = 10;
            int vehicleId = 1;

            var fix = new Fixture();
            var vehicle = fix.Create<Vehicle>();
            vehicle.MaxCargoVolume = volumeAndWeight;
            vehicle.MaxCargoWeightKg = volumeAndWeight;
            vehicle.Cargos = new List<Cargo>();
            vehicle.Id = vehicleId;

            _vehicleRespository.ReadById(vehicleId).Returns(vehicle);

            var cargo = new Cargo(volumeAndWeight, volumeAndWeight);

            //Act
            _vehicleService.LoadCargo(cargo, vehicleId);

            //Assert
            _vehicleRespository.Received(1).ReadById(vehicleId);
            _vehicleRespository.Received(1).Update(vehicleId, vehicle);
            Assert.Equal(vehicle.Cargos.FirstOrDefault(), cargo);
        }

        [Fact]
        public void LoadCargo_WhenCargoDoesntFit_ThrowExeption()
        {
            //Arrange
            int volumeAndWeight = 10;
            int cargoExtraVolumeAndWeight = 100;
            int vehicleId = 1;

            var fix = new Fixture();
            var vehicle = fix.Create<Vehicle>();
            vehicle.MaxCargoVolume = volumeAndWeight;
            vehicle.MaxCargoWeightKg = volumeAndWeight;
            vehicle.Cargos = new List<Cargo>();
            vehicle.Id = vehicleId;


            _vehicleRespository.ReadById(vehicleId).Returns(vehicle);

            var cargo = new Cargo(cargoExtraVolumeAndWeight, cargoExtraVolumeAndWeight);

            //Act
            Action act = () => _vehicleService.LoadCargo(cargo, vehicleId);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            _vehicleRespository.Received(1).ReadById(vehicleId);
            Assert.Contains("Not enough", exception.Message);
        }

        [Fact]
        public void LoadCargo_VehicleIdNotFound_ThrowExeption()
        {
            //Arrange
            int volumeAndWeight = 10;
            int vehicleId = 1;

            var cargo = new Cargo(volumeAndWeight, volumeAndWeight);

            //Act
            Action act = () => _vehicleService.LoadCargo(cargo, vehicleId);

            //Assert
            NullReferenceException exception = Assert.Throws<NullReferenceException>(act);
            _vehicleRespository.Received(1).ReadById(Arg.Any<int>());
        }


        [Fact]
        public void UnloadCargo_WhenDefaultExecution_ShouldCallExpectedMethods()
        {
            //Arrange
            int volumeAndWeight = 10;
            int vehicleId = 1;

            var cargo = new Cargo(volumeAndWeight, volumeAndWeight);

            var fix = new Fixture();
            var vehicle = fix.Create<Vehicle>();
            vehicle.MaxCargoVolume = volumeAndWeight;
            vehicle.MaxCargoWeightKg = volumeAndWeight;
            vehicle.Cargos = new List<Cargo> { cargo };
            vehicle.Id = vehicleId;


            _vehicleRespository.ReadById(vehicleId).Returns(vehicle);

            //Act
            var res = _vehicleService.UnloadCargo(cargo.Id, vehicleId);

            //Assert
            _vehicleRespository.Received(1).ReadById(vehicleId);
            _vehicleRespository.Received(1).Update(vehicleId, vehicle);
            Assert.Equal(cargo.Id, res.Id);
            Assert.Equal(default(Cargo), vehicle.Cargos.FirstOrDefault());
        }

        [Fact]
        public void UnloadCargo_WhenCargoNotFound_ThrowExeption()
        {
            //Arrange
            int volumeAndWeight = 10;
            int vehicleId = 1;

            var fix = new Fixture();
            var vehicle = fix.Create<Vehicle>();
            vehicle.MaxCargoVolume = volumeAndWeight;
            vehicle.MaxCargoWeightKg = volumeAndWeight;
            vehicle.Cargos = new List<Cargo>();
            vehicle.Id = vehicleId;

            _vehicleRespository.ReadById(vehicleId).Returns(vehicle);

            //Act
            Action act = () => _vehicleService.UnloadCargo(Guid.NewGuid(), vehicleId);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            _vehicleRespository.Received(1).ReadById(vehicleId);
            Assert.Contains("Cargo not found", exception.Message);
        }

    }
}