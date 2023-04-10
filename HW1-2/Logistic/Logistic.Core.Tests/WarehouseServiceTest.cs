using Logistic.Models;
using NSubstitute;

namespace Logistic.Core.Tests
{
    public class WarehouseServiceTest
    {
        private readonly WarehouseService _warehouseService;
        private readonly IRepository<Warehouse> _warehouseRespository;

        public WarehouseServiceTest()
        {
            _warehouseRespository = Substitute.For<IRepository<Warehouse>>();
            _warehouseService = new WarehouseService(_warehouseRespository);
        }
        [Fact]
        public void LoadCargo_WhenDefaultExecution_ShouldCallExpectedMethods()
        {
            //Arrange
            int volumeAndWeight = 10;
            int warehouseId = 1;

            var warehouse = new Warehouse();
            _warehouseRespository.ReadById(warehouseId).Returns(warehouse);

            var cargo = new Cargo(volumeAndWeight, volumeAndWeight);

            //Act
            _warehouseService.LoadCargo(cargo, warehouseId);

            //Assert
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
            _warehouseRespository.Received(1).Update(Arg.Any<int>(), Arg.Any<Warehouse>());
            Assert.Equal(warehouse.Cargos.FirstOrDefault(), cargo);
        }

        [Fact]
        public void LoadCargo_WhenWarehouseIdNotFound_ThrowExeption()
        {
            //Arrange
            int volumeAndWeight = 10;
            int warehouseId = 1;

            var cargo = new Cargo(volumeAndWeight, volumeAndWeight);

            //Act
            Action act = () => _warehouseService.LoadCargo(cargo, warehouseId);

            //Assert
            NullReferenceException exception = Assert.Throws<NullReferenceException>(act);
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
        }

        [Fact]
        public void UnloadCargo_WhenDefaultExecution_ShouldCallExpectedMethods()
        {
            //Arrange
            int volumeAndWeight = 10;
            int warehouseId = 1;

            var cargo = new Cargo(volumeAndWeight, volumeAndWeight);

            var warehouse = new Warehouse()
            {
                Cargos = new List<Cargo> { cargo }
            };
            _warehouseRespository.ReadById(warehouseId).Returns(warehouse);

            //Act
            var res = _warehouseService.UnloadCargo(cargo.Id, warehouseId);

            //Assert
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
            _warehouseRespository.Received(1).Update(Arg.Any<int>(), Arg.Any<Warehouse>());
            Assert.Equal(cargo.Id, res.Id);
            Assert.Equal(default(Cargo), warehouse.Cargos.FirstOrDefault());
        }

        [Fact]
        public void UnloadCargo_WhenCargoNotFound_ThrowExeption()
        {
            //Arrange
            int warehouseId = 1;

            var warehouse = new Warehouse();

            _warehouseRespository.ReadById(warehouseId).Returns(warehouse);

            //Act
            Action act = () => _warehouseService.UnloadCargo(Guid.NewGuid(), warehouseId);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
            Assert.Contains("Cargo not found", exception.Message);
        }
    }
}