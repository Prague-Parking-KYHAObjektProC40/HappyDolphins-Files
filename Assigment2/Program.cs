using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace FordonApp
{
    class Program
    {
        static void Main(string[] args)
        {

            /*ParkeringsHus parkeringsHus = LoadParkingData();*/
            ParkeringsHus parkeringsHus = new ParkeringsHus(100);

            // Parking spot labels
            string[] Parkeringsruta = {
                "P-Ruta 1", "P-Ruta 2", "P-Ruta 3", "P-Ruta 4", "P-Ruta 5", "P-Ruta 6", "P-Ruta 7", "P-Ruta 8", "P-Ruta 9", "P-Ruta 10",
                "P-Ruta 11", "P-Ruta 12", "P-Ruta 13", "P-Ruta 14", "P-Ruta 15", "P-Ruta 16", "P-Ruta 17", "P-Ruta 18", "P-Ruta 19", "P-Ruta 20",
                "P-Ruta 21", "P-Ruta 22", "P-Ruta 23", "P-Ruta 24", "P-Ruta 25", "P-Ruta 26", "P-Ruta 27", "P-Ruta 28", "P-Ruta 29", "P-Ruta 30",
                "P-Ruta 31", "P-Ruta 32", "P-Ruta 33", "P-Ruta 34", "P-Ruta 35", "P-Ruta 36", "P-Ruta 37", "P-Ruta 38", "P-Ruta 39", "P-Ruta 40",
                "P-Ruta 41", "P-Ruta 42", "P-Ruta 43", "P-Ruta 44", "P-Ruta 45", "P-Ruta 46", "P-Ruta 47", "P-Ruta 48", "P-Ruta 49", "P-Ruta 50",
                "P-Ruta 51", "P-Ruta 52", "P-Ruta 53", "P-Ruta 54", "P-Ruta 55", "P-Ruta 56", "P-Ruta 57", "P-Ruta 58", "P-Ruta 59", "P-Ruta 60",
                "P-Ruta 61", "P-Ruta 62", "P-Ruta 63", "P-Ruta 64", "P-Ruta 65", "P-Ruta 66", "P-Ruta 67", "P-Ruta 68", "P-Ruta 69", "P-Ruta 70",
                "P-Ruta 71", "P-Ruta 72", "P-Ruta 73", "P-Ruta 74", "P-Ruta 75", "P-Ruta 76", "P-Ruta 77", "P-Ruta 78", "P-Ruta 79", "P-Ruta 80",
                "P-Ruta 81", "P-Ruta 82", "P-Ruta 83", "P-Ruta 84", "P-Ruta 85", "P-Ruta 86", "P-Ruta 87", "P-Ruta 88", "P-Ruta 89", "P-Ruta 90",
                "P-Ruta 91", "P-Ruta 92", "P-Ruta 93", "P-Ruta 94", "P-Ruta 95", "P-Ruta 96", "P-Ruta 97", "P-Ruta 98", "P-Ruta 99", "P-Ruta 100"
            };

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
                     + "\n5 Move a Vehicle"
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
                            Console.WriteLine("Enter the target parking spot number:");
                            if (int.TryParse(Console.ReadLine(), out int targetSpotNumber))
                            {
                                bool success = parkeringsHus.MoveVehicle(regNumberToMove, targetSpotNumber);
                                if (!success)
                                {
                                    Console.WriteLine("Unable to move the vehicle to the specified spot.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for spot number. Please enter a numeric value.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please provide a registration number.");
                        }
                        break;

                         case "6":  // New case for viewing the parking map
                        Console.Clear();
                        Console.WriteLine("----- Parking Map -----");

                        // Get the status of each parking spot
                        var allParkingSpots = parkeringsHus.GetAllParkingSpots();

                        for (int i = 0; i < allParkingSpots.Count; i++)
                        {
                            var spot = allParkingSpots[i];
                            if (spot.VehicleOnLot.Count > 0)
                            {
                                // Spot is occupied
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{Parkeringsruta[i]} - Occupied by {spot.VehicleOnLot[0].Typ}");
                            }
                            else
                            {
                                // Spot is free
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{Parkeringsruta[i]} - Available");
                            }
                        }
                        Console.ResetColor();
                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey();
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

        /* Function to save parking data to JSON
        static void SaveParkingData(ParkeringsHus parkeringsHus)
        {
            string json = JsonConvert.SerializeObject(parkeringsHus, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("parkingData.json", json);
            Console.WriteLine("Parking data saved to parkingData.json.");
        }
        */

        // Function to load parking data from JSON
        static ParkeringsHus LoadParkingData()
        {
            if (File.Exists("parkingData.json"))
            {
                string json = File.ReadAllText("parkingData.json");
                return JsonConvert.DeserializeObject<ParkeringsHus>(json) ?? new ParkeringsHus(100);
            }
            else
            {
                Console.WriteLine("No saved data found. Initializing with empty parking structure.");
                return new ParkeringsHus(100);
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
                // Check if the spot already has a car or bus; if so, it can't accept other vehicles.
                if (VehicleOnLot.Exists(v => v.Typ == VehicleTyp.Car || v.Typ == VehicleTyp.Bus))
                {
                    return false;
                }

                // Check if the vehicle is a bus, and only allow it if the spot is within 1-50 and has no other vehicles.
                if (vehicle.Typ == VehicleTyp.Bus)
                {
                    return PlaceNumber <= 50 && GotHighCeilings && VehicleOnLot.Count == 0;
                }

                // Allow two MCs in a spot
                if (vehicle.Typ == VehicleTyp.MC)
                {
                    return VehicleOnLot.Count(v => v.Typ == VehicleTyp.MC) < 2;
                }

                // Allow up to four bicycles in a spot
                if (vehicle.Typ == VehicleTyp.Bicycle)
                {
                    return VehicleOnLot.Count(v => v.Typ == VehicleTyp.Bicycle) < 4;
                }

                // Allow one car only if the spot is empty
                if (vehicle.Typ == VehicleTyp.Car)
                {
                    return VehicleOnLot.Count == 0;
                }

                return false; // Default to not allowing parking
            }


            public void ParkVehicle(Vehicle vehicle)
            {
                if (CanPark(vehicle))
                {
                    VehicleOnLot.Add(vehicle);
                    BusySize += vehicle.Size;
                    Console.WriteLine($"Vehicle {vehicle.Typ} with registration number {vehicle.RegistrationNumber} parked at spot {PlaceNumber}.");
                }
                else
                {
                    Console.WriteLine($"Spot {PlaceNumber} is not suitable for vehicle {vehicle.RegistrationNumber}.");
                }
            }


            public void RemoveVehicles(Vehicle vehicle)
            {
                if (VehicleOnLot.Remove(vehicle))
                {
                    BusySize -= vehicle.Size;
                    Console.WriteLine($"Vehicle {vehicle.Typ} with registration number {vehicle.RegistrationNumber} has left spot {PlaceNumber}.");
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

            public bool MoveVehicle(string registrationNumber, int targetSpotNumber)
            {
                if (targetSpotNumber < 1 || targetSpotNumber > ParkingSpaces.Count)
                {
                    Console.WriteLine("Invalid parking spot number.");
                    return false;
                }

                var foundSpot = FindVehicleByRegistration(registrationNumber);
                if (!foundSpot.HasValue)
                {
                    Console.WriteLine("Vehicle not found.");
                    return false;
                }

                var vehicleToMove = foundSpot.Value.vehicle;
                int originalPlaceNumber = foundSpot.Value.PlaceNumber;

                if (vehicleToMove.Typ == VehicleTyp.Bus && targetSpotNumber > 50)
                {
                    Console.WriteLine("Buses can only park in spots 1-50.");
                    return false;
                }

                ParkingSpot targetSpot = ParkingSpaces[targetSpotNumber - 1];
                if (!targetSpot.CanPark(vehicleToMove))
                {
                    Console.WriteLine($"Spot {targetSpotNumber} cannot accommodate this vehicle.");
                    return false;
                }

                ParkingSpot currentSpot = ParkingSpaces[originalPlaceNumber - 1];
                currentSpot.RemoveVehicles(vehicleToMove);

                targetSpot.ParkVehicle(vehicleToMove);
                Console.WriteLine($"Vehicle {vehicleToMove.Typ} with registration {vehicleToMove.RegistrationNumber} moved to spot {targetSpotNumber}.");
                return true;
            }





        }
    }
            


        

