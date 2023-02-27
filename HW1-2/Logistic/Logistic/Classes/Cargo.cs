namespace Logistic.ConsoleClient.Classes
{
    public class Cargo
    {
        public double Volume { get; set; }
        public int Weight { get; set; }//in kg
        public string Code { get; set; }

        public Cargo(double Volume, int Weight, string Code)
        {
            this.Code = Code;
            this.Volume = Volume;
            this.Weight = Weight;
        }
        
        //public override string ToString()
        public string GetInformation()
        {
            return "Cargo №" + Code + "\nVolume = " + Volume.ToString() + "\nWeight = " + Weight.ToString();
        }

    }
}
