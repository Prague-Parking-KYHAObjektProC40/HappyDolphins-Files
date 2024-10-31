// FordonApp namespace och klasser...

namespace FordonApp
{
    public enum VehicleTyp
    {
        Car,
        MC,
        Bus,
        Bicycle
    }

    public enum ParkingSpotStatus
    {
        Empty,
        Busy
    }


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
    // Fortsättning av klasser...

    // Parkeringsplats-klass
    public class ParkingSpot
    {
        public int PlaceNumber { get; set; }
        public int MaxSize { get; set; }
        public bool GotHighCeilings { get; set; }
        public int BusySize { get; private set; } = 0;
        public List<Vehicle> VehicleOnLot { get; private set; } = new List<Vehicle>();

        public ParkingSpot(int placeNumber, bool _HighCeilings)
        {
            PlaceNumber = placeNumber;
            MaxSize = _HighCeilings ? 16 : 4;
            GotHighCeilings = _HighCeilings;
        }

        public bool CanPark(Vehicle vehicle)
        {
            // Returnera false om platsen är upptagen eller har inte tillräcklig plats.
            return VehicleOnLot.Count == 0 &&
                   BusySize + vehicle.Size <= MaxSize &&
                   (vehicle.Typ != VehicleTyp.Bus || GotHighCeilings);
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            if (CanPark(vehicle))
            {
                VehicleOnLot.Add(vehicle);
                BusySize += vehicle.Size;
                Console.WriteLine($"Fordon med registreringsnummer {vehicle.RegistrationNumber} parkerat på plats {PlaceNumber}.");
            }
            else
            {
                Console.WriteLine($"Plats {PlaceNumber} är redan upptagen eller inte lämplig för fordon {vehicle.RegistrationNumber}.");
            }
        }

        public void RemoveVehicles(Vehicle vehicle)
        {
            if (VehicleOnLot.Remove(vehicle))
            {
                BusySize -= vehicle.Size;
                Console.WriteLine($"Fordon med registreringsnummer {vehicle.RegistrationNumber} har lämnat plats {PlaceNumber}.");
            }
        }
    }

    public class ParkeringsHus
    {
        private List<ParkingSpot> ParkingSpaces = new List<ParkingSpot>();

        public ParkeringsHus(int totalPlatser)
        {
            for (int i = 1; i <= totalPlatser; i++)
            {
                bool harHogtTak = i <= 50;
                ParkingSpaces.Add(new ParkingSpot(i, harHogtTak));
            }
        }

        public void ParkVehicles(Vehicle vehicle)
        {
            var ledigPlats = ParkingSpaces.Find(plats => plats.CanPark(vehicle));
            if (ledigPlats != null)
            {
                ledigPlats.ParkVehicle(vehicle);
            }
            else
            {
                Console.WriteLine($"Inga lediga parkeringsplatser för fordon med registreringsnummer {vehicle.RegistrationNumber}");
            }
        }

        public void RemoveVehicles(string registreringsNummer)
        {
            foreach (var plats in ParkingSpaces)
            {
                var vehicle = plats.VehicleOnLot.Find(f => f.RegistrationNumber == registreringsNummer);
                if (vehicle != null)
                {
                    plats.RemoveVehicles(vehicle);
                    return;
                }
            }
            Console.WriteLine($"Fordonet med registreringsnummer {registreringsNummer} hittades inte.");
        }
    }

    // Main-metod för att testa klasserna
    class Program
    {
        static void Main(string[] args)
        {
            ParkeringsHus parkeringsHus = new ParkeringsHus(100);

            Car car1 = new Car("ABC123");
            MC mc1 = new MC("XYZ789");
            Bicycle bicycle1 = new Bicycle("CYC101");

            parkeringsHus.ParkVehicles(car1);
            parkeringsHus.ParkVehicles(mc1);
            parkeringsHus.ParkVehicles(bicycle1);

            parkeringsHus.RemoveVehicles("ABC123");
            parkeringsHus.RemoveVehicles("BUS001");
        }
    }
}
