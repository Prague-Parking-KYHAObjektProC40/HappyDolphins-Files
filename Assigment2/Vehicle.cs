namespace FordonApp
{
    public abstract class Vehicle
        {
            public string RegistrationNumber { get; set; }
            public VehicleTyp Typ { get; set; }
            public int Size { get; set; } // Storleksegenskap

            public Vehicle(string registrationNumber, VehicleTyp typ, int size)
            {
                RegistrationNumber = registrationNumber;
                Typ = typ;
                Size = size;
            }

            public abstract void Starta();
        }
    }
            


        

