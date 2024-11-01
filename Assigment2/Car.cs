namespace FordonApp
{
    // Bil-klass
    public class Car : Vehicle
        {
            public Car(string registreringsNummer)
                : base(registreringsNummer, VehicleTyp.Car, 4) { }

            public override void Starta()
            {
                Console.WriteLine($"Bilen med registreringsnummer {RegistrationNumber} startar.");
            }
        }
    }
            


        

