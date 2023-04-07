using Logistic.Models;
using Logistic.Core;
using Logistic.DAL;

Warehouse InputWarehouse()
{
    return new Warehouse();
}
Vehicle InputVehicle()
{
    Console.WriteLine("Input vehicle type (Car, Ship, Plane, Train) = ");
    string type = Console.ReadLine();

    var isParsed = Enum.TryParse(type, out VehicleType vehicleType) && Enum.IsDefined(typeof(VehicleType), vehicleType);
    if (!isParsed)
        throw new Exception("No such vehicle type");

    Console.WriteLine("Input MaxCargoWeightKg = ");
    int weight = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Input MaxCargoVolume = ");
    double volume = Convert.ToDouble(Console.ReadLine());

    var res = new Vehicle(vehicleType, weight, volume);

    return res;
}

Cargo InputCargo()
{
    Console.WriteLine("Input CargoWeight = ");
    int weight = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Input CargoVolume = ");
    int volume = Convert.ToInt32(Console.ReadLine());

    var res = new Cargo(weight, volume);
    Console.WriteLine("Id = " + res.Id.ToString());
    return res;
}

var vehicleRepository = new InMemoryRepository<Vehicle>();
var vehicleService = new VehicleService(vehicleRepository);

InMemoryRepository<Warehouse> warehouseRepository = new InMemoryRepository<Warehouse>();
var warehouseService = new WarehouseService(warehouseRepository);

ReportService<Vehicle,int> reportService = new ReportService<Vehicle, int>(new JsonRepository<Vehicle, int>(),new XmlRepository<Vehicle, int>());

Console.WriteLine(@"add vehicle
    далі консоль пропонує ввести данні Vehicle (окрім айді)
add warehouse
get-all vehicle
get-all warehouse
load-cargo vehicle 12 - завантажити Cargo у vehicle
далі консоль пропонує ввести данні Cargo (окрім айді)
load-cargo warehouse 15
unload-cargo vehicle *guid* 11 //додав ще один індекс (UnloadCargo(CargoId, VehicleId) - вивантажи Cargo по айді з Vehicle по Vehicle айді)
unload-cargo warehouse *guid* 11
create-report vehicle xml - створює репорт для всіх сутностей що були створені за час роботи додатку (дані накопичені у InMemoryRepository<T>)
create-report vehicle json
load-report filename - відображає список vehicle у консолі з репорт файлу (приводячи до формату який зручно читати, тобто НЕ у json, xml формтах)");

while (true)
{
    try
    {
        Console.WriteLine(">>> Input command");
        string command = Console.ReadLine();
        var stringArr = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        switch (stringArr[0])
        {
            case Commands.ADD_COMMAND:
                ExecuteAdd(stringArr);
                break;
            case Commands.GET_COMMAND:
                ExecuteGet(stringArr);
                break;
            case Commands.LOAD_COMMAND:
                ExecuteLoad(stringArr);
                break;
            case Commands.UNLOAD_COMMAND:
                ExecuteUnload(stringArr);
                break;
            case Commands.CREATE_REPORT_COMMAND:
                Enum.TryParse(stringArr[2], out ReportType reportType);
                var entities = vehicleService.GetAll();
                Console.WriteLine("File Name = " + reportService.CreateReport(entities,reportType));
                break;
            case Commands.LOAD_REPORT_COMMAND:
                var list = reportService.LoadReport(stringArr[^1]);
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                break;
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

void ExecuteAdd(string[] strings)
{
    switch (strings[1])
    {
        case "vehicle":
            Console.WriteLine("Id = " + vehicleService.Create(InputVehicle()));
            break;
        case "warehouse":
            Console.WriteLine("Id = " + warehouseService.Create(InputWarehouse()));
            break;
        default:
            Console.WriteLine("Unknown argument");
            break;
    }
}
void ExecuteGet(string[] strings)
{
    switch (strings[1])
    {
        case "vehicle":
            Console.WriteLine(vehicleService.GetAll());
            break;
        case "warehouse":
            Console.WriteLine(warehouseService.GetAll());
            break;
        default:
            Console.WriteLine("Unknown argument");
            break;
    }
}
void ExecuteLoad(string[] strings)
{
    switch (strings[1])
    {
        case "vehicle":
            vehicleService.LoadCargo(InputCargo(), Convert.ToInt32(strings[2]));
            break;
        case "warehouse":
            warehouseService.LoadCargo(InputCargo(), Convert.ToInt32(strings[2]));
            break;
        default:
            Console.WriteLine("Unknown argument");
            break;
    }
}
void ExecuteUnload(string[] strings)
{
    switch (strings[1])
    {
        case "vehicle":
            vehicleService.UnloadCargo(new Guid(strings[2]), Convert.ToInt32(strings[3]));
            break;
        case "warehouse":
            warehouseService.UnloadCargo(new Guid(strings[2]), Convert.ToInt32(strings[3]));
            break;
        default:
            Console.WriteLine("Unknown argument");
            break;
    }
}

public static class Commands
{
    public const string ADD_COMMAND = "add";
    public const string GET_COMMAND = "get-all";
    public const string LOAD_COMMAND = "load-cargo";
    public const string UNLOAD_COMMAND = "unload-cargo";
    public const string CREATE_REPORT_COMMAND = "create-report";
    public const string LOAD_REPORT_COMMAND = "load-report";

}



