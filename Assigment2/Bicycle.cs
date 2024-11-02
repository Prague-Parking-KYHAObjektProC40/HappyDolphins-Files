namespace FordonApp
{
    //Cykel-klass
    public class Bicycle : Vehicle
    {
        public Bicycle(string registreringsNummer)
            : base(registreringsNummer, VehicleType.Bicycle, 1, DateTime.Now) { } //kev

        public override void Starta()
        {
            Console.WriteLine($"Bicycle med registreringsnummer {RegistrationNumber} startar.");
        }
    }
}





