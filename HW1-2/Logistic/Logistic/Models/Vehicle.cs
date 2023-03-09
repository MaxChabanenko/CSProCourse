namespace Logistic.ConsoleClient.Classes
{
    public enum VehicleType { Car, Ship, Plane, Train }
    public enum WeightUnit { Kilograms, Pounds }

    internal class Vehicle
    {
        private const double PND_COEFFICIENT = 2.20462262;

        public Vehicle(VehicleType VehicleType, int MaxCargoWeightKg, double MaxCargoVolume)
        {
            Number = new Random().Next(1, 101).ToString();
            Type = VehicleType;
            this.MaxCargoWeightKg = MaxCargoWeightKg;
            this.MaxCargoVolume = MaxCargoVolume;
            MaxCargoWeightPnd = PND_COEFFICIENT * MaxCargoWeightKg;
            Cargos = new List<Cargo>();
        }
        public Vehicle() { }
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string Number { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; }
        private int currentWeight { get; set; }
        private double currentVolume { get; set; }

        public string GetCargoVolumeLeft()
        {
            return "Залишилось об'єму у транспортному засобі = " + (MaxCargoVolume - currentVolume).ToString();
        }

        public string GetCargoWeightLeft(WeightUnit weightUnit)
        {

            if (weightUnit == WeightUnit.Kilograms)
            {
                return "Залишилось вантажопідйомності у транспортному засобі (kg)= " + (MaxCargoWeightKg - currentWeight).ToString();
            }
            else
            {
                return "Залишилось вантажопідйомності у транспортному засобі (lbs)= " + ((MaxCargoWeightKg - currentWeight) * PND_COEFFICIENT).ToString();
            }
        }

        public string GetInformation()
        {

            return "VehicleType = " + Type.ToString()
            + "\nNumber = " + Number.ToString()
            + "\nMaxCargoWeightKg = " + MaxCargoWeightKg.ToString()
            + "\nMaxCargoWeightPnd = " + MaxCargoWeightPnd.ToString()
            + "\nMaxCargoVolume = " + MaxCargoVolume.ToString()
            + "\nCargo amount = " + Cargos.Count.ToString()
            + "\nTotalWeight (kg)= " + currentWeight.ToString()
            + "\nTotalVolume = " + currentVolume.ToString();
        }

        public void LoadCargo(Cargo cargo)
        {

            if (currentWeight + cargo.Weight <= MaxCargoWeightKg)
            {
                if (currentVolume + cargo.Volume <= MaxCargoVolume)
                {
                    this.currentWeight += cargo.Weight;
                    this.currentVolume += cargo.Volume;
                    Cargos.Add(cargo);
                }
                else
                {
                    throw new Exception("Not enough space to fit in the " + cargo.GetInformation());
                }
            }
            else
            {
                if (currentVolume + cargo.Volume > MaxCargoVolume)
                {
                    throw new Exception("Not enough weight & space capacity to fit in the " + cargo.GetInformation());
                }
                else
                {
                    throw new Exception("Not enough weight capacity to fit in the " + cargo.GetInformation());
                }
            }
        }
    }

}
