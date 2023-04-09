using Logistic.Models;

namespace Logistic.DAL.Tests
{
    public class XmlRepositoryTest
    {
        private readonly XmlRepository<Vehicle, int> _xmlVehicleRepository;
        private readonly XmlRepository<Warehouse, int> _xmlWarehouseRepository;

        public XmlRepositoryTest()
        {
            _xmlVehicleRepository = new XmlRepository<Vehicle, int>();
            _xmlWarehouseRepository = new XmlRepository<Warehouse, int>();

        }
        [Fact]
        public void ReadVehicle_ValidXml_DeserializeSuccessfuly()
        {
            var list = _xmlVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.xml"));

            Assert.Equal(105, list[0].Id);
            Assert.Equal(121, list[1].Id);
            Assert.Equal(216, list[2].Id);
            Assert.Equal("ad8b9ce0-df73-4a0a-82f1-29a0c0eb2f45", list[0].Cargos[0].Id.ToString());
        }
        [Fact]
        public void ReadWarehouse_ValidXml_DeserializeSuccessfuly()
        {
            var list = _xmlWarehouseRepository.Read(Path.Combine("Resources", "Warehouse_08_04_2023_19_39.xml"));

            Assert.Equal(197, list[0].Id);
            Assert.Equal(53, list[1].Id);
            Assert.Equal(185, list[2].Id);
            Assert.Equal("c4a7311d-0656-416e-bd36-b61802edeb15", list[0].Cargos[0].Id.ToString());
        }
        [Fact]
        public void ReadXml_FileNotFound_ThrowException()
        {
            Action act = () => _xmlVehicleRepository.Read(Path.Combine("Resources", "123.xml"));

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
        }
        [Fact]
        public void ReadXml_XmlFile_ThrowException()
        {
            Action act = () => _xmlVehicleRepository.Read(Path.Combine("Resources", "Vehicle_08_04_2023_19_39.js"));

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
        }
    }
}