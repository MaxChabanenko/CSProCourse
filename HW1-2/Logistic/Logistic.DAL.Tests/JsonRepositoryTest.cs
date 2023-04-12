using Logistic.Core;
using Logistic.Models;

namespace Logistic.DAL.Tests
{
    public class JsonRepositoryTest
    {
        private readonly JsonRepository<Vehicle, int> _jsonVehicleRepository;

        public JsonRepositoryTest()
        {
            _jsonVehicleRepository = new JsonRepository<Vehicle, int>();
        }
        [Fact]
        public void ReadVehicle_ValidJson_DeserializeSuccessfuly()
        {
            //Act
            var list = _jsonVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.json"));
            
            //Assert
            Assert.Equal(105,list[0].Id);
            Assert.Equal(121, list[1].Id);
            Assert.Equal(216, list[2].Id);
            Assert.Equal("ad8b9ce0-df73-4a0a-82f1-29a0c0eb2f45", list[0].Cargos[0].Id.ToString());
        }
        [Fact]
        public void ReadJson_FileNotFound_ThrowException()
        {
            //Act
            Action act = () => _jsonVehicleRepository.Read(Path.Combine("Resources", "123.json"));

            //Assert
            var exception = Assert.Throws<FileNotFoundException>(act);
        }
        [Fact]
        public void ReadJson_XmlFile_ThrowException()
        {            
            //Act
            Action act = () => _jsonVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.xml"));

            //Assert
            var exception = Assert.Throws<System.Text.Json.JsonException>(act);
        }
    }
}