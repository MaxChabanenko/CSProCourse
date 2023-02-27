using Logistic.ConsoleClient.Classes;

void SuccessScenario()
{
    Vehicle boat = new Vehicle(VehicleType.Ship,8000,10000);
    Console.WriteLine("________Empty(Success)________");
    Console.WriteLine(boat.GetInformation());

    Cargo[] cargos = new Cargo[4];
    cargos[0] = new Cargo(1000,800,"1a");
    cargos[1] = new Cargo(1500,500, "2b");
    cargos[2] = new Cargo(2000,3000, "3c");
    cargos[3] = new Cargo(2500,1000, "4d");

    foreach (var cargo in cargos)
        boat.LoadCargo(cargo);

    Console.WriteLine("________Loaded(Success)_______");
    Console.WriteLine(boat.GetInformation());
}

void ExceptionScenario()
{
    Vehicle car = new Vehicle(VehicleType.Car, 1000, 1000);
    Console.WriteLine("________Empty(Exception)________");
    Console.WriteLine(car.GetInformation());

    Cargo[] cargos = new Cargo[4];
    cargos[0] = new Cargo(800, 800, "1a");
    cargos[1] = new Cargo(1500, 1000, "2b");
    cargos[2] = new Cargo(2000, 3000, "3c");
    cargos[3] = new Cargo(2500, 1000, "4d");

    Console.WriteLine("________Loading(Exception)_______");
    foreach (var cargo in cargos)
        car.LoadCargo(cargo);

    Console.WriteLine("________Loaded(Exception)_______");
    Console.WriteLine(car.GetInformation());

}

try
{
    SuccessScenario();
    ExceptionScenario();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();


