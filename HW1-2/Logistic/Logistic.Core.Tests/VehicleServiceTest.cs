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
        public void LoadCargo_WhenCargoFits_CallExpectedMethods()
        {
            //Arrange
            var vehicle = new Vehicle()
            {
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 10,
                Cargos = new List<Cargo>()
            };

            _vehicleRespository.ReadById(Arg.Any<int>()).Returns(vehicle);

            var cargo = new Cargo(10, 10);


            //Act
            _vehicleService.LoadCargo(cargo, 1);

            //Assert
            _vehicleRespository.Received(1).ReadById(Arg.Any<int>());
            _vehicleRespository.Received(1).Update(Arg.Any<int>(), Arg.Any<Vehicle>());
            Assert.Equal(vehicle.Cargos.FirstOrDefault(), cargo);
        }

        [Fact]
        public void LoadCargo_WhenCargoDoesntFit_ThrowExeption()
        {
            //Arrange
            var vehicle = new Vehicle()
            {
                MaxCargoVolume = 1,
                MaxCargoWeightKg = 1,
                Cargos = new List<Cargo>()
            };

            _vehicleRespository.ReadById(Arg.Any<int>()).Returns(vehicle);

            var cargo = new Cargo(10, 10);


            //Act
            Action act = () => _vehicleService.LoadCargo(cargo, 1);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            _vehicleRespository.Received(1).ReadById(Arg.Any<int>());
            Assert.Contains("Not enough", exception.Message);
        }

        [Fact]
        public void LoadCargo_VehicleIdNotFound_ThrowExeption()
        {
            //Arrange
            var cargo = new Cargo(10, 10);

            //Act
            Action act = () => _vehicleService.LoadCargo(cargo, 1);

            //Assert
            NullReferenceException exception = Assert.Throws<NullReferenceException>(act);
            _vehicleRespository.Received(1).ReadById(Arg.Any<int>());
        }

        [Fact]
        public void LoadCargo_InvalidCargo_ThrowExeption()
        {
            //Arrange
            Cargo cargo;

            //Act
            Action act = () => cargo = new Cargo(-10, 0);

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Contains("Invalid Cargo parameter: ", exception.Message);
        }

        [Fact]
        public void UnloadCargo_Successful_CallExpectedMethods()
        {
            //Arrange
            var cargo = new Cargo(10, 10);
            var vehicle = new Vehicle()
            {
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 10,
                Cargos = new List<Cargo> { cargo }
            };
            _vehicleRespository.ReadById(Arg.Any<int>()).Returns(vehicle);

            //Act
            var res = _vehicleService.UnloadCargo(cargo.Id, 1);

            //Assert
            _vehicleRespository.Received(1).ReadById(Arg.Any<int>());
            _vehicleRespository.Received(1).Update(Arg.Any<int>(), Arg.Any<Vehicle>());
            Assert.Equal(cargo.Id, res.Id);
            Assert.Equal(default(Cargo), vehicle.Cargos.FirstOrDefault());
        }

        [Fact]
        public void UnloadCargo_NotFoundCargo_ThrowExeption()
        {
            //Arrange
            
            var vehicle = new Vehicle()
            {
                MaxCargoVolume = 10,
                MaxCargoWeightKg = 10,
                Cargos = new List<Cargo>()
            };
            _vehicleRespository.ReadById(Arg.Any<int>()).Returns(vehicle);

            //Act
            Action act = () => _vehicleService.UnloadCargo(Guid.NewGuid(), 1);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            _vehicleRespository.Received(1).ReadById(Arg.Any<int>());
            Assert.Contains("Cargo not found", exception.Message);
        }

    }
}