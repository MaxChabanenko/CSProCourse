﻿
namespace Logistic.Models
{
    public class Cargo : IEntity<Guid>
    {
        public Cargo() { }
        public Cargo(double Volume, int Weight)
        {
            if (Volume<=0)
                throw new ArgumentException("Invalid Cargo parameter: ", nameof(Volume));
            if (Weight<=0)
                throw new ArgumentException("Invalid Cargo parameter: ", nameof(Weight));

            Id = Guid.NewGuid();
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
            return "Cargo №" + Id + "\nVolume = " + Volume.ToString() + "\nWeight = " + Weight.ToString()+"\n";
        }

    }
}