using AutoFixture;
using Logistic.Models;
using NSubstitute;

namespace Logistic.DAL.Tests
{
    public class InMemoryRepositoryTest
    {
        private readonly InMemoryRepository<Vehicle> _inMemoryRepository;
        public InMemoryRepositoryTest()
        {
            _inMemoryRepository = Substitute.For<InMemoryRepository<Vehicle>>();
        }
        [Fact]
        public void ReadById_WhenDefaultExecution_ReturnsClone()
        {
            //Arrange
            var fix = new Fixture();
            var vehicle = fix.Create<Vehicle>();
            int id = _inMemoryRepository.Create(vehicle);
            vehicle.Id = id;

            //Act
            var res =  _inMemoryRepository.ReadById(id);

            //Clone(different reference)
            Assert.NotEqual(vehicle,res);
        }
        [Fact]
        public void Delete_WhenDefaultExecution_ShouldCallExpectedMethods()
        {
            //Arrange
            var fix = new Fixture();
            var vehicle = fix.Create<Vehicle>();
            int id = _inMemoryRepository.Create(vehicle);

            //Act
            _inMemoryRepository.Delete(id);

            //Assert
            Assert.Equal(0, _inMemoryRepository.ReadAll().Count());
        }
        [Fact]
        public void Update_WhenDefaultExecution_ShouldCallExpectedMethods()
        {
            //Arrange
            var fix = new Fixture();
            var vehicleToUpdate = fix.Create<Vehicle>();
            var newVehicle = fix.Create<Vehicle>();
            int id = _inMemoryRepository.Create(vehicleToUpdate);

            //Act
            _inMemoryRepository.Update(id,newVehicle);

            //Assert
            _inMemoryRepository.Received(1).Delete(id);
            _inMemoryRepository.Received(1).Create(newVehicle);

        }

    }
}