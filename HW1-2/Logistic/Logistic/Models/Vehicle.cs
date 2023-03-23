using Logistic.ConsoleClient.DataAccess;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.Classes
{
    public enum VehicleType { Car, Ship, Plane, Train }
    public enum WeightUnit { Kilograms, Pounds }

    public class Vehicle : IEntity<int>
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

        public Vehicle() {  }

        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string Number { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; }
        
        public override string ToString()
        {
            string cargos = "";
            foreach (var item in Cargos)
            { 
                cargos += item.ToString(); 
            }

            return "Vehicle №" + Id + "\nType = " + Type.ToString() + "\nMaxVolume = " + MaxCargoVolume.ToString() + "\nMaxWeight = " + MaxCargoWeightKg.ToString()
                + "\n     Cargos\n" + cargos;
        }
    }

}
