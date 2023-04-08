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
        public void LoadCargo_Default_CallExpectedMethods()
        {
            //Arrange
            var warehouse = new Warehouse();

            _warehouseRespository.ReadById(Arg.Any<int>()).Returns(warehouse);

            var cargo = new Cargo(10, 10);


            //Act
            _warehouseService.LoadCargo(cargo, 1);

            //Assert
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
            _warehouseRespository.Received(1).Update(Arg.Any<int>(), Arg.Any<Warehouse>());
            Assert.Equal(warehouse.Cargos.FirstOrDefault(), cargo);
        }

        [Fact]
        public void LoadCargo_WarehouseIdNotFound_ThrowExeption()
        {
            //Arrange
            var cargo = new Cargo(10, 10);

            //Act
            Action act = () => _warehouseService.LoadCargo(cargo, 1);

            //Assert
            NullReferenceException exception = Assert.Throws<NullReferenceException>(act);
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
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
            var warehouse = new Warehouse()
            {
                Cargos = new List<Cargo> { cargo }
            };
            _warehouseRespository.ReadById(Arg.Any<int>()).Returns(warehouse);

            //Act
            var res = _warehouseService.UnloadCargo(cargo.Id, 1);

            //Assert
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
            _warehouseRespository.Received(1).Update(Arg.Any<int>(), Arg.Any<Warehouse>());
            Assert.Equal(cargo.Id, res.Id);
            Assert.Equal(default(Cargo), warehouse.Cargos.FirstOrDefault());
        }

        [Fact]
        public void UnloadCargo_NotFoundCargo_ThrowExeption()
        {
            //Arrange
            var warehouse = new Warehouse();
            _warehouseRespository.ReadById(Arg.Any<int>()).Returns(warehouse);

            //Act
            Action act = () => _warehouseService.UnloadCargo(Guid.NewGuid(), 1);

            //Assert
            Exception exception = Assert.Throws<Exception>(act);
            _warehouseRespository.Received(1).ReadById(Arg.Any<int>());
            Assert.Contains("Cargo not found", exception.Message);
        }
    }
}