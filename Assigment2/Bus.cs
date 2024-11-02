namespace FordonApp
{
    //Buss-klass
    public class Bus : Vehicle
    {
        public Bus(string registreringsNummer)
            : base(registreringsNummer, VehicleType.Bus, 16, DateTime.Now) { } //kev

        public override void Starta()
        {
            Console.WriteLine($"Bus med registreringsnummer {RegistrationNumber} startar.");
        }
    }
}