using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
// using Newtonsoft.Json;

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
              */
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
    }
            


        

