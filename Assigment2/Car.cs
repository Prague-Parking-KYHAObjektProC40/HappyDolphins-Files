namespace FordonApp
{
    // Bil-klass
    public class Car : Vehicle
    {
        public Car(string registreringsNummer)
            : base(registreringsNummer, VehicleType.Car, 4, DateTime.Now) { } //kev

        public override void Starta()
        {
            Console.WriteLine($"Bilen med registreringsnummer {RegistrationNumber} startar.");
        }
    }
}