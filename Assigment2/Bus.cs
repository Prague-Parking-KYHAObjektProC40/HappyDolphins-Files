namespace FordonApp
{
    //Buss-klass
    public class Bus : Vehicle
        {
            public Bus(string registreringsNummer)
                : base(registreringsNummer, VehicleTyp.Bus, 16) { }

            public override void Starta()
            {
                Console.WriteLine($"Bus med registreringsnummer {RegistrationNumber} startar.");
            }
        }
    }
            


        

