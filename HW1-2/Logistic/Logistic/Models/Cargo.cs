using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Classes
{
    public class Cargo : EntityBase//BaseEntityGeneric<Guid>
    {
        public Cargo() { }
        public Cargo(double Volume, int Weight)
        {
            Id = Guid.NewGuid();
            this.Volume = Volume;
            this.Weight = Weight;
        }
        public Cargo(double Volume, int Weight, string Code)
        {
            Id = Guid.NewGuid();
            this.Code = Code;
            this.Volume = Volume;
            this.Weight = Weight;
        }
        public Cargo(Guid id, double Volume, int Weight, string Code)
        {
            Id = id;
            this.Code = Code;
            this.Volume = Volume;
            this.Weight = Weight;
        }

        public new Guid Id { get; set; }
        public Invoice Invoice { get; set; }
        public double Volume { get; set; }
        public int Weight { get; set; }//in kg
        public string Code { get; set; }

        public override string ToString()
        {
            return "Cargo №" + Id + "\nVolume = " + Volume.ToString() + "\nWeight = " + Weight.ToString();
        }

    }
}