using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.Services;

Vehicle InputVehicle()
{
    Console.WriteLine("Input vehicle type (Car, Ship, Plane, Train) = ");
    string type = Console.ReadLine();
    Enum.TryParse(type, out VehicleType vehicleType);

    Console.WriteLine("Input MaxCargoWeightKg = ");
    int weight = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Input MaxCargoVolume = ");
    double volume = Convert.ToDouble(Console.ReadLine());

    var res = new Vehicle(vehicleType, weight, volume);
    Console.WriteLine("Id = " + res.Id.ToString());
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

VehicleService vehicleService = new VehicleService();
WarehouseService warehouseService = new WarehouseService();
ReportService<Vehicle> reportService = new ReportService<Vehicle>();

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
        switch (stringArr[0] + " " + stringArr[1])
        {
            case "add vehicle":
                vehicleService.Create(InputVehicle());
                break;
            case "add warehouse":
                warehouseService.Create(new Warehouse());
                break;
            case "get-all vehicle":
                Console.WriteLine(vehicleService.GetAll());
                break;
            case "get-all warehouse":
                Console.WriteLine(warehouseService.GetAll());
                break;
            case "load-cargo vehicle":
                vehicleService.LoadCargo(InputCargo(), Convert.ToInt32(stringArr[^1]));
                break;
            case "load-cargo warehouse":
                warehouseService.LoadCargo(InputCargo(), Convert.ToInt32(stringArr[^1]));
                break;
            case "unload-cargo vehicle":
                vehicleService.UnloadCargo(new Guid(stringArr[^2]), Convert.ToInt32(stringArr[^1]));
                break;
            case "unload-cargo warehouse":
                warehouseService.UnloadCargo(new Guid(stringArr[^2]), Convert.ToInt32(stringArr[^1]));
                break;
            case "create-report vehicle":
                Enum.TryParse(stringArr[^1], out ReportType reportType);
                Console.WriteLine("File Name = " + reportService.CreateReport(reportType));
                break;

        }
        //окремо, тому що команда з одного слова(
        if (stringArr[0] == "load-report")
        {
            var list = reportService.LoadReport(stringArr[^1]);
            foreach (var item in list)
                Console.WriteLine(item);
        }
    }

    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}





