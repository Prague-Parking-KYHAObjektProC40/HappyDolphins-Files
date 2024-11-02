namespace FordonApp
{
    // MC-klass
    public class MC : Vehicle
    {
        public MC(string registreringsNummer)
            : base(registreringsNummer, VehicleType.MC, 2, DateTime.Now) { } //kev

        public override void Starta()
        {
            Console.WriteLine($"MC:n med registreringsnummer {RegistrationNumber} startar.");
        }
    }
}





