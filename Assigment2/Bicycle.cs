namespace FordonApp
{
    //Cykel-klass
    public class Bicycle : Vehicle
        {
            public Bicycle(string registreringsNummer)
                : base(registreringsNummer, VehicleTyp.Bicycle, 1) { }

            public override void Starta()
            {
                Console.WriteLine($"Bicycle med registreringsnummer {RegistrationNumber} startar.");
            }
        }
    }
            


        

