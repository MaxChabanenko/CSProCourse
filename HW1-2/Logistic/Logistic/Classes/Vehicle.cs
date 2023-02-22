namespace Logistic.ConsoleClient.Classes
{
    public enum VehicleType { Car, Ship, Plane, Train }
    public enum WeightUnit { Kilograms, Pounds }

    internal class Vehicle
    {
        private const double KgToPnd = 2.20462262;

        public Vehicle(VehicleType VehicleType, int MaxCargoWeightKg, double MaxCargoVolume)
        {
            Number = new Random().Next(1, 101).ToString();
            Type = VehicleType;
            this.MaxCargoWeightKg = MaxCargoWeightKg;
            this.MaxCargoVolume = MaxCargoVolume;
            MaxCargoWeightPnd = KgToPnd * MaxCargoWeightKg;
            Cargos = new List<Cargo>();//new Cargo[100]; 
        }

        public VehicleType Type { get; set; }
        public string Number { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; }

        //який поветає стрічку з інформацією скільки залишилось об'єму у транспортному
        //засобі (вираховується на основі завантаженого Cargo).
        public string GetCargoVolumeLeft()
        {
            double totalVolume = 0;

            foreach (Cargo car in Cargos)
                totalVolume += car.Volume;

            return "Залишилось об'єму у транспортному засобі = " + (MaxCargoVolume - totalVolume).ToString();
        }
        //який поветає стрічку з інформацією скільки залишилось вантажопідйомності
        //(кілограмів) у транспортному засобі (вираховується на основі завантаженого Cargo)

        //непотрібна функція
        public string GetCargoWeightLeft(WeightUnit weightUnit)
        {
            int totalWeightKg = 0;

            foreach (Cargo car in Cargos)
                totalWeightKg += car.Weight;

            if (weightUnit == WeightUnit.Kilograms)
                return "Залишилось вантажопідйомності у транспортному засобі (kg)= " + (MaxCargoWeightKg - totalWeightKg).ToString();
            else
                return "Залишилось вантажопідйомності у транспортному засобі (lbs)= " + ((MaxCargoWeightKg - totalWeightKg) * KgToPnd).ToString();
        }
        //повертає стрічку з усією інформацією по транспортному засобу - всі поля
        //(виключаючи Cargo) + кількість завантаженого Cargo та його сумарну масу, об'єм.
        public string GetInformation()
        {
            int totalWeight = 0;
            double totalVolume = 0;

            foreach (Cargo car in Cargos)
            {
                totalWeight += car.Weight;
                totalVolume += car.Volume;
            }

            return "VehicleType = " + Type.ToString()
            + "\nNumber = " + Number.ToString()
            + "\nMaxCargoWeightKg = " + MaxCargoWeightKg.ToString()
            + "\nMaxCargoWeightPnd = " + MaxCargoWeightPnd.ToString()
            + "\nMaxCargoVolume = " + MaxCargoVolume.ToString()
            + "\nCargo amount = " + Cargos.Count.ToString()
            + "\nTotalWeight (kg)= " + totalWeight.ToString()
            + "\nTotalVolume = " + totalVolume.ToString();
        }
        // метод додає Cargo до массиву Cargos класу Vehicle, також метод має викинути
        // виключення у разі якщо у транспортного засобу перевищення по об'єму або за вагою.

        // Якщо вантаж не вліз - додайте у деталі помилки фізичні характеристики Cargo,
        // що не вліз (вага та об'єм) та чому саме не вліз (не вистачило місця або вантажопідйомності)
        public void LoadCargo(Cargo cargo)
        {
            int totalWeight = 0;
            double totalVolume = 0;

            foreach (Cargo car in Cargos)
            {
                totalWeight += car.Weight;
                totalVolume += car.Volume;
            }

            if (totalWeight + cargo.Weight <= MaxCargoWeightKg)
                if (totalVolume + cargo.Volume <= MaxCargoVolume)
                    Cargos.Add(cargo);
                else
                    throw new Exception("Not enough space to fit in the " + cargo.GetInformation());
            else
            {
                if (totalVolume + cargo.Volume > MaxCargoVolume)
                    throw new Exception("Not enough weight & space capacity to fit in the " + cargo.GetInformation());
                else
                    throw new Exception("Not enough weight capacity to fit in the " + cargo.GetInformation());
            }
        }
    }

}
