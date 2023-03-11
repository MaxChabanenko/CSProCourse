using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Classes
{
    public enum VehicleType { Car, Ship, Plane, Train }
    public enum WeightUnit { Kilograms, Pounds }

    public class Vehicle : EntityBase
    {
        private const double PND_COEFFICIENT = 2.20462262;

        public Vehicle(VehicleType VehicleType, int MaxCargoWeightKg, double MaxCargoVolume)
        {
            Id = IdNext++;
            Number = new Random().Next(1, 101).ToString();
            Type = VehicleType;
            this.MaxCargoWeightKg = MaxCargoWeightKg;
            this.MaxCargoVolume = MaxCargoVolume;
            MaxCargoWeightPnd = PND_COEFFICIENT * MaxCargoWeightKg;
            Cargos = new List<Cargo>();
        }

        public Vehicle() { Id = IdNext++; }

        public new int Id { get; set; }
        static private int IdNext = 1;
        public VehicleType Type { get; set; }
        public string Number { get; set; }
        public int MaxCargoWeightKg { get; set; }
        public double MaxCargoWeightPnd { get; set; }
        public double MaxCargoVolume { get; set; }
        public List<Cargo> Cargos { get; set; }
        public void Add(Cargo cargo)
        {
            Cargos.Add(cargo);
        }
        public void Delete(Cargo cargo)
        {
            Cargos.Remove(cargo);
        }
        public Cargo DeleteByGuid(Guid id)
        {
            var item = Cargos.Find(x => x.Id == id);
            Delete(item);
            return item;
        }
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
