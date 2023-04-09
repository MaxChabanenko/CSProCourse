using Logistic.Core;
using Logistic.Models;

namespace Logistic.DAL.Tests
{
    public class JsonRepositoryTest
    {
        private readonly JsonRepository<Vehicle, int> _jsonVehicleRepository;
        private readonly JsonRepository<Warehouse, int> _jsonWarehouseRepository;

        public JsonRepositoryTest()
        {
            _jsonVehicleRepository = new JsonRepository<Vehicle, int>();
            _jsonWarehouseRepository = new JsonRepository<Warehouse, int>();

        }
        [Fact]
        public void ReadVehicle_ValidJson_DeserializeSuccessfuly()
        {
            var list = _jsonVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.json"));
            
            Assert.Equal(105,list[0].Id);
            Assert.Equal(121, list[1].Id);
            Assert.Equal(216, list[2].Id);
            Assert.Equal("ad8b9ce0-df73-4a0a-82f1-29a0c0eb2f45", list[0].Cargos[0].Id.ToString());
        }
        [Fact]
        public void ReadWarehouse_ValidJson_DeserializeSuccessfuly()
        {
            var list = _jsonVehicleRepository.Read(Path.Combine("Resources", "Warehouse_08_04_2023_19_39.json"));

            Assert.Equal(197, list[0].Id);
            Assert.Equal(53, list[1].Id);
            Assert.Equal(185, list[2].Id);
            Assert.Equal("c4a7311d-0656-416e-bd36-b61802edeb15", list[0].Cargos[0].Id.ToString());
        }
        [Fact]
        public void ReadJson_FileNotFound_ThrowException()
        {
            Action act = () => _jsonVehicleRepository.Read(Path.Combine("Resources", "123.json"));

            FileNotFoundException exception = Assert.Throws<FileNotFoundException>(act);
        }
        [Fact]
        public void ReadJson_XmlFile_ThrowException()
        {
            Action act = () => _jsonVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.xml"));

            System.Text.Json.JsonException exception = Assert.Throws<System.Text.Json.JsonException>(act);
        }
    }
}