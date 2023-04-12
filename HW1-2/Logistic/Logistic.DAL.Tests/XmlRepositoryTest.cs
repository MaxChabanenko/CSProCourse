using Logistic.Models;

namespace Logistic.DAL.Tests
{
    public class XmlRepositoryTest
    {
        private readonly XmlRepository<Vehicle, int> _xmlVehicleRepository;

        public XmlRepositoryTest()
        {
            _xmlVehicleRepository = new XmlRepository<Vehicle, int>();
        }
        [Fact]
        public void ReadVehicle_ValidXml_DeserializeSuccessfuly()
        {
            //Act
            var list = _xmlVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.xml"));

            //Assert
            Assert.Equal(105, list[0].Id);
            Assert.Equal(121, list[1].Id);
            Assert.Equal(216, list[2].Id);
            Assert.Equal("ad8b9ce0-df73-4a0a-82f1-29a0c0eb2f45", list[0].Cargos[0].Id.ToString());
        }
        [Fact]
        public void ReadXml_FileNotFound_ThrowException()
        {
            //Act
            Action act = () => _xmlVehicleRepository.Read(Path.Combine("Resources", "123.xml"));

            //Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
        }
        [Fact]
        public void ReadXml_XmlFile_ThrowException()
        {
            //Act
            Action act = () => _xmlVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.js"));

            //Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
        }
    }
}