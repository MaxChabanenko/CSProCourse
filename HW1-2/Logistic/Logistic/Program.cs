using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.DataAccess;

//void SuccessScenario()
//{

//    List<Cargo> cargoList = new List<Cargo>();

//    cargoList.Add(new Cargo(1000, 800, "1a"));
//    cargoList.Add(new Cargo(1500, 500, "2b"));
//    cargoList.Add(new Cargo(2000, 3000, "3c"));
//    cargoList.Add(new Cargo(2500, 1000, "4d"));
//    Warehouse warehouse = new Warehouse();
//    warehouse.Cargos = cargoList;
//    warehouse.Id = 111;

//    Warehouse warehouse2 = new Warehouse();
//    warehouse2.Cargos = cargoList;
//    warehouse2.Id = 333;

//    XmlRepository<Cargo> jsonRepository = new XmlRepository<Cargo>();
//    //JsonRepository<Warehouse> jsonRepository = new JsonRepository<Warehouse>();

//    var listW = new List<Warehouse>();
//    listW.Add(warehouse);
//    listW.Add(warehouse2);

//    jsonRepository.Create(cargoList);
//    ;

//}
//void ExceptionScenario()
//{

//    List<Cargo> cargoList = new List<Cargo>();

//    cargoList.Add(new Cargo(1000, 800, "1a"));
//    cargoList.Add(new Cargo(1500, 500, "2b"));
//    cargoList.Add(new Cargo(2000, 3000, "3c"));
//    cargoList.Add(new Cargo(2500, 1000, "4d"));

//    XmlRepository<Cargo> jsonRepository = new XmlRepository<Cargo>();


//   var a= jsonRepository.Read("Cargo_09_03_2023_11_37.xml");
//    ;
//}

try
{
    //SuccessScenario();
    // ExceptionScenario();
    var a = new Cargo(1000, 800, "1a");
    InMemeoryRepository<Cargo, Guid>.Create(a);
    InMemeoryRepository<Cargo, Guid>.Create(new Cargo(1500, 500, "2b"));

    var b = InMemeoryRepository<Cargo, Guid>.ReadById(a.Id);

    var list = InMemeoryRepository<Cargo, Guid>.ReadAll();

    InMemeoryRepository<Cargo, Guid>.Update(a.Id, new Cargo(1500, 500, "2b"));

    InMemeoryRepository<Cargo, Guid>.Delete(a.Id);




}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();


