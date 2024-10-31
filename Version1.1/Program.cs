namespace FordonApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkeringsHus parkeringsHus = new ParkeringsHus(100);

            bool running = true;

            while (running)
            {
                Console.WriteLine("<<<<<<<<<<<<<<<<<<+>>>>>>>>>>>>>>>>>>");
                Console.WriteLine("<<  Welcome to our luxury garage   >>");
                Console.WriteLine("<<<<<<<<<<<<<<<<<<+>>>>>>>>>>>>>>>>>>");
                Console.WriteLine("Please choose from the menu options"
                     + "\n1: Add New Customer"
                     + "\n2: Remove Customer"
                     + "\n3: View Current Vehicles Parked"
                     + "\n4: Find Vehicle"
                     + "\n0: Exit Program");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("----- Add New Customer -----");
                            Console.WriteLine("Enter 'CAR' to add a car, 'MC' to add a motorcycle, 'BUS' to add a bus, or 'BIKE' to add a bicycle: ");
                            string vehicleType = Console.ReadLine()?.ToUpper();

                            // Validation logic: 2 to 4 characters, no spaces
                            if (!string.IsNullOrEmpty(vehicleType) && vehicleType.Length >= 2 && vehicleType.Length <= 4 && !vehicleType.Contains(" "))
                            {
                                string registrationNumber = null;

                                // Validate registration number input
                                while (true)
                                {
                                    Console.WriteLine("Enter the registration number: ");
                                    registrationNumber = Console.ReadLine()?.ToUpper();

                                    // Validation logic: 1 to 10 characters, no spaces
                                    if (!string.IsNullOrEmpty(registrationNumber) && registrationNumber.Length >= 1 && registrationNumber.Length <= 10 && !registrationNumber.Contains(" "))
                                    {
                                        break; // Exit the inner loop if valid registration number
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid registration number. It must be between 1 to 10 characters with no spaces. Please try again.");
                                    }
                                }

                                // Once we have valid inputs, we create the vehicle and park it
                                if (!string.IsNullOrEmpty(vehicleType) && !string.IsNullOrEmpty(registrationNumber))
                                {
                                    Vehicle? vehicle = vehicleType switch
                                    {
                                        "CAR" => new Car(registrationNumber),
                                        "MC" => new MC(registrationNumber),
                                        "BUS" => new Bus(registrationNumber),
                                        "BIKE" => new Bicycle(registrationNumber),
                                        _ => null
                                    };

                                    if (vehicle != null)
                                    {
                                        parkeringsHus.ParkVehicles(vehicle);
                                        Console.WriteLine("Vehicle added successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid vehicle type entered.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please provide both vehicle type and registration number.");
                                }

                                break; // Exit the outer loop after adding the vehicle
                            }
                            else
                            {
                                Console.WriteLine("Invalid vehicle type. It must be one of CAR, MC, BUS, or BIKE with no spaces. Please try again.");
                            }
                        }
                        break;



                    case "2":
                        Console.Clear();
                        Console.WriteLine("----- Remove a Customer -----");
                        Console.WriteLine("Enter registration number to remove vehicle\nType 'EXIT' to return:");

                        string regNumberToRemove = Console.ReadLine();

                        if (!string.IsNullOrEmpty(regNumberToRemove))
                        {
                            parkeringsHus.RemoveVehicles(regNumberToRemove);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please provide a registration number.");
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("----- Current vehicles parked -----");
                        foreach (var spot in parkeringsHus.GetAllParkingSpots())
                        {
                            if (spot.VehicleOnLot.Count > 0)
                            {
                                Console.WriteLine($"Parking Spot {spot.PlaceNumber} contains:");
                                foreach (var parkedVehicle in spot.VehicleOnLot)
                                {
                                    Console.WriteLine($"- {parkedVehicle.Typ} with registration number {parkedVehicle.RegistrationNumber}");
                                }
                            }
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter the registration number to find the vehicle:");
                        string regNumberToFind = Console.ReadLine();

                        if (!string.IsNullOrEmpty(regNumberToFind))
                        {
                            var foundVehicle = parkeringsHus.FindVehicleByRegistration(regNumberToFind);

                            if (foundVehicle.HasValue)
                            {
                                // Accessing the properties safely after null check
                                Console.WriteLine($"Vehicle found: {foundVehicle.Value.vehicle.Typ} with registration number {foundVehicle.Value.vehicle.RegistrationNumber} at spot {foundVehicle.Value.PlaceNumber}.");
                            }
                            else
                            {
                                Console.WriteLine("Vehicle not found.");
                            }
                        }
                        break;


                    case "5":
                        Console.Clear();
                        Console.WriteLine("----- Move a Vehicle -----");
                        Console.WriteLine("Enter the registration number to move the vehicle:");
                        string regNumberToMove = Console.ReadLine();

                        if (!string.IsNullOrEmpty(regNumberToMove))
                        {
                            var foundSpot = parkeringsHus.FindVehicleByRegistration(regNumberToMove); 
                            if (foundSpot.HasValue)
                            {
                                var vehicleToMove = foundSpot.Value.vehicle;
                                Console.WriteLine($"Moving vehicle {vehicleToMove.Typ} with registration number {vehicleToMove.RegistrationNumber}."); 
                                parkeringsHus.RemoveVehicles(regNumberToMove);
                                parkeringsHus.ParkVehicles(vehicleToMove);
                            }
                            else { Console.WriteLine("Vehicle not found."); }
                        }
                        else { Console.WriteLine("Invalid input. Please provide a registration number."); }
                        break;


                    case "0":
                        Console.WriteLine("Exiting program...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // FordonApp namespace och klasser...


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

            // New method to get all parking spots
            public List<ParkingSpot> GetAllParkingSpots()
            {
                return ParkingSpaces;
            }

            // New method to find a vehicle by registration number
            public (Vehicle vehicle, int PlaceNumber)? FindVehicleByRegistration(string registreringsNummer)
            {
                foreach (var plats in ParkingSpaces)
                {
                    var vehicle = plats.VehicleOnLot.Find(f => f.RegistrationNumber == registreringsNummer);
                    if (vehicle != null)
                    {
                        return (vehicle, plats.PlaceNumber);
                    }
                }
                return null; // Return null if no vehicle is found
            }
        }
    }
}
            


        

