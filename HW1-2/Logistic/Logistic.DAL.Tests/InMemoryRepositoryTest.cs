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
        public void CreateAndReadById_Default_ReturnsClone()
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
        public void CreateAndDelete_Default_CallsExpectedMethods()
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
        public void Update_Default_CallsExpectedMethods()
        {
            //Arrange
            var fix = new Fixture();
            var vehicle = fix.Create<Vehicle>();
            var vehicle2 = fix.Create<Vehicle>();
            int id = _inMemoryRepository.Create(vehicle);

            //Act
            _inMemoryRepository.Update(id,vehicle2);

            //Assert
            _inMemoryRepository.Received(1).Delete(Arg.Any<int>());
            _inMemoryRepository.Received(1).Create(vehicle2);

        }

    }
}