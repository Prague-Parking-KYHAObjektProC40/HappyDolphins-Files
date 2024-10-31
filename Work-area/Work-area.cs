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


    // Parkeringsplats-klass
    public class ParkingSpot
    {
        public int PlaceNumber { get; set; }
        public int MaxSize { get; set; }
        public bool GotHighCeilings { get; set; } // För bussar
        public int BusySize { get; private set; } = 0; // Total upptagen storlek
        public List<Vehicle> VehicleOnLot { get; private set; } = new List<Vehicle>();

        public ParkingSpot(int placeNumber, bool _HighCeilings)
        {
            PlaceNumber = placeNumber;
            // Sätt MaxStorlek till 16 för bussar, annars 4 för bilar
            MaxSize = _HighCeilings ? 16 : 4;
            GotHighCeilings = _HighCeilings;
        }

        public bool CanPark(Vehicle vehicle)
        {
            // Kontrollera om det redan finns ett fordon på platsen
            if (VehicleOnLot.Count > 0)
            {
                return false; // Platsen är upptagen
            }

            // Kontrollera om platsen har tillräcklig ledig storlek
            if (vehicle.Typ == VehicleTyp.Bus)
            {
                // Kontrollera om platsen är en av de första 50 och har högt tak
                if (!GotHighCeilings)
                {
                    return false; // Returnera falskt utan att skriva ut meddelandet
                }
            }
            // Kontrollera om platsen har tillräcklig ledig storlek
            return (BusySize + vehicle.Size <= MaxSize);
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
                Console.WriteLine($"Plats {PlaceNumber} har inte tillräckligt med utrymme eller höjd för detta fordon, eller så är platsen redan upptagen.");
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

        // Parkeringshus-klass
        public class ParkeringsHus
        {
            private List<ParkingSpot> ParkingSpaces = new List<ParkingSpot>();

            public ParkeringsHus(int totalPlatser)
            {
                for (int i = 1; i <= totalPlatser; i++)
                {
                    bool harHogtTak = i <= 50; // Endast de första 50 platserna har högt tak för bussar
                    ParkingSpaces.Add(new ParkingSpot(i, harHogtTak));
                }
            }
            public void ParkVehicles(Vehicle vehicle)
            {
                var lämpligPlats = ParkingSpaces.Find(plats => plats.CanPark(vehicle));
                if (lämpligPlats != null)
                {
                    lämpligPlats.ParkVehicle(vehicle);
                }
                else
                {
                    Console.WriteLine($"Inga lediga parkeringsplatser med tillräckligt utrymme tillgänglig, \nför fordon med registreringsnummer {vehicle.RegistrationNumber}");
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
                Console.WriteLine("Fordonet hittades inte på någon parkeringsplats.");
            }
        }
        // Testar klasserna i Main-metoden
        class Program
        {
            static void Main(string[] args)
            {
                ParkeringsHus parkeringsHus = new ParkeringsHus(100);


                /*
                Buss buss1 = new Buss("BUS001");
                Buss buss2 = new Buss("BUS02");
                Buss buss3 = new Buss("BUS003");
                Buss buss4 = new Buss("BUS004");
                Buss buss5 = new Buss("BUS005");
                Buss buss6 = new Buss("BUS006");
                Buss buss7 = new Buss("BUS007");
                Buss buss8 = new Buss("BUS008");
                Buss buss9 = new Buss("BUS009");
                Buss buss10 = new Buss("BUS010");

                Buss buss11 = new Buss("BUS011");
                Buss buss12 = new Buss("BUS012");
                Buss buss13 = new Buss("BUS013");
                Buss buss14 = new Buss("BUS014");
                Buss buss15 = new Buss("BUS015");
                Buss buss16 = new Buss("BUS016");
                Buss buss17 = new Buss("BUS017");
                Buss buss18 = new Buss("BUS018");
                Buss buss19 = new Buss("BUS019");
                Buss buss20 = new Buss("BUS020");

                Buss buss21 = new Buss("BUS021");
                Buss buss22 = new Buss("BUS022");
                Buss buss23 = new Buss("BUS023");
                Buss buss24 = new Buss("BUS024");
                Buss buss25 = new Buss("BUS025");
                Buss buss26 = new Buss("BUS026");
                Buss buss27 = new Buss("BUS027");
                Buss buss28 = new Buss("BUS028");
                Buss buss29 = new Buss("BUS029");
                Buss buss30 = new Buss("BUS030");

                Buss buss31 = new Buss("BUS031");
                Buss buss32 = new Buss("BUS032");
                Buss buss33 = new Buss("BUS033");
                Buss buss34 = new Buss("BUS034");
                Buss buss35 = new Buss("BUS035");
                Buss buss36 = new Buss("BUS036");
                Buss buss37 = new Buss("BUS037");
                Buss buss38 = new Buss("BUS038");
                Buss buss39 = new Buss("BUS039");
                Buss buss40 = new Buss("BUS040");

                Buss buss41 = new Buss("BUS041");
                Buss buss42 = new Buss("BUS042");
                Buss buss43 = new Buss("BUS043");
                Buss buss44 = new Buss("BUS044");
                Buss buss45 = new Buss("BUS045");
                Buss buss46 = new Buss("BUS046");
                Buss buss47 = new Buss("BUS047");
                Buss buss48 = new Buss("BUS048");
                Buss buss49 = new Buss("BUS049");
                Buss buss50 = new Buss("BUS050");

                Buss buss51 = new Buss("BUS051"); */

                Car car1 = new Car("ABC123");
                MC mc1 = new MC("XYZ789");
                Bicycle bicycle1 = new Bicycle("CYC101");

                // Parkera olika fordon


                /*
                parkeringsHus.Parkera(buss1);
                parkeringsHus.Parkera(buss2);
                parkeringsHus.Parkera(buss3);
                parkeringsHus.Parkera(buss4);
                parkeringsHus.Parkera(buss5);
                parkeringsHus.Parkera(buss6);
                parkeringsHus.Parkera(buss7);
                parkeringsHus.Parkera(buss8);
                parkeringsHus.Parkera(buss9);
                parkeringsHus.Parkera(buss10);

                parkeringsHus.Parkera(buss11);
                parkeringsHus.Parkera(buss12);
                parkeringsHus.Parkera(buss13);
                parkeringsHus.Parkera(buss14);
                parkeringsHus.Parkera(buss15);
                parkeringsHus.Parkera(buss16);
                parkeringsHus.Parkera(buss17);
                parkeringsHus.Parkera(buss18);
                parkeringsHus.Parkera(buss19);
                parkeringsHus.Parkera(buss20);

                parkeringsHus.Parkera(buss21);
                parkeringsHus.Parkera(buss22);
                parkeringsHus.Parkera(buss23);
                parkeringsHus.Parkera(buss24);
                parkeringsHus.Parkera(buss25);
                parkeringsHus.Parkera(buss26);
                parkeringsHus.Parkera(buss27);
                parkeringsHus.Parkera(buss28);
                parkeringsHus.Parkera(buss29);
                parkeringsHus.Parkera(buss30);

                parkeringsHus.Parkera(buss31);
                parkeringsHus.Parkera(buss32);
                parkeringsHus.Parkera(buss33);
                parkeringsHus.Parkera(buss34);
                parkeringsHus.Parkera(buss35);
                parkeringsHus.Parkera(buss36);
                parkeringsHus.Parkera(buss37);
                parkeringsHus.Parkera(buss38);
                parkeringsHus.Parkera(buss39);
                parkeringsHus.Parkera(buss40);

                parkeringsHus.Parkera(buss41);
                parkeringsHus.Parkera(buss42);
                parkeringsHus.Parkera(buss43);
                parkeringsHus.Parkera(buss44);
                parkeringsHus.Parkera(buss45);
                parkeringsHus.Parkera(buss46);
                parkeringsHus.Parkera(buss47);
                parkeringsHus.Parkera(buss48);
                parkeringsHus.Parkera(buss49);
                parkeringsHus.Parkera(buss50);

                parkeringsHus.Parkera(buss51);*/


                parkeringsHus.ParkVehicles(car1);
                parkeringsHus.ParkVehicles(mc1);
                parkeringsHus.ParkVehicles(bicycle1);

                // Ta bort fordon
                parkeringsHus.RemoveVehicles("ABC123");
                parkeringsHus.RemoveVehicles("BUS001");
            }
        }
    }
}