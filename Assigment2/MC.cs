namespace FordonApp
{
    // MC-klass
    public class MC : Vehicle
        {
            public MC(string registreringsNummer)
                : base(registreringsNummer, VehicleTyp.MC, 2) { }

            public override void Starta()
            {
                Console.WriteLine($"MC:n med registreringsnummer {RegistrationNumber} startar.");
            }
        }
    }
            


        

