using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Classes
{
    public class Cargo : EntityBase//BaseEntityGeneric<Guid>
    {
        public new Guid Id { get; set; }
        public Invoice Invoice { get; set; }
        public double Volume { get; set; }
        public int Weight { get; set; }//in kg
        public string Code { get; set; }
        public Cargo() { }
        public Cargo(double Volume, int Weight, string Code)
        {
            Id = Guid.NewGuid();
            this.Code = Code;
            this.Volume = Volume;
            this.Weight = Weight;
        }
        public string GetInformation()//public override string ToString()
        {
            return "Cargo №" + Code + "\nVolume = " + Volume.ToString() + "\nWeight = " + Weight.ToString();
        }

    }
}