using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        CustomersVehicle[] pLot = new CustomersVehicle[100];
        Storage storage = new Storage(pLot); // Pass the parking lot to Storage class

        bool running = true;

        while (running)
        {
            Console.WriteLine("<<<<<<<<<<<<<<<<<<+>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("<<  Welcome to our luxury garage   >>");
            Console.WriteLine($"<< Current spaces: {storage.GetStatus()}  >>");
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
                    Console.Clear();
                    Console.WriteLine("----- Add New Customer -----");

                    string regNumber = GetValidRegistrationNumber();

                    Console.WriteLine("Enter 'CAR' to add a car or 'MC' to add a motorcycle: ");
                    string addInput = Console.ReadLine().ToUpper();

                    if (addInput == "CAR")
                    {
                        storage.AddCars(regNumber);
                    }
                    else if (addInput == "MC")
                    {
                        storage.AddMCs(regNumber);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 'CAR' or 'MC'.");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("----- Remove a Customer -----");
                    Console.WriteLine("Enter registration number to remove vehicle\nType 'EXIT' to return:");
                    string removeInput = Console.ReadLine().ToUpper();

                    if (removeInput == "EXIT")
                        break;

                    if (storage.Exists(removeInput))
                    {
                        storage.RemoveVehicle(removeInput);
                    }
                    else
                    {
                        Console.WriteLine("No vehicle found with the given registration number.");
                    }
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("----- Current vehicles parked -----");
                    storage.ViewLot();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("Enter the registration number to find the vehicle:");
                    string searchInput = Console.ReadLine().ToUpper();

                    (string vehicleType, int lot) = storage.GetVehicleTypeAndLot(searchInput);

                    if (vehicleType != null)
                    {
                        Console.WriteLine($"Vehicle found: {vehicleType} with registration number '{searchInput}' in lot {lot}.");
                    }
                    else
                    {
                        Console.WriteLine("No vehicle found with the given registration number.");
                    }
                    break;


                case "5":
                    Console.Clear();
                    Console.WriteLine("----- Move Vehicle to New Lot -----");
                    Console.WriteLine("Enter the registration number of the vehicle you want to move:");
                    string moveInput = Console.ReadLine().ToUpper();

                    if (storage.Exists(moveInput))
                    {
                        // Find the current lot of the vehicle
                        (string vehicletype, int currentLot) = storage.GetVehicleTypeAndLot(moveInput);

                        // Find an empty lot
                        int newLot = storage.FindEmptySpot();

                        if (newLot != -1)
                        {
                            // Move the vehicle to the new lot
                            storage.MoveVehicle(moveInput, currentLot, newLot);
                        }
                        else
                        {
                            Console.WriteLine("No empty lot available to move the vehicle.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No vehicle found with the given registration number.");
                    }
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

    // Helper function to get a valid registration number
    static string GetValidRegistrationNumber()
    {
        while (true)
        {
            Console.WriteLine("Please enter the registration number (1-10 characters): ");
            string regNumber = Console.ReadLine().ToUpper();

            if (regNumber.Length >= 1 && regNumber.Length <= 10 && !regNumber.Contains(" "))
            {
                return regNumber;
            }
            else
            {
                Console.WriteLine("Invalid registration number. It must be between 1 to 10 characters with no spaces.");
            }
        }
    }
}

// Define Storage Class
class Storage
{
    private CustomersVehicle[] pLot;
    private Dictionary<string, string> vehicles = new Dictionary<string, string>(); // regNumber -> vehicleType

    public Storage(CustomersVehicle[] pLot)
    {
        this.pLot = pLot;
    }

    public void AddCars(string regNumber)
    {
        if (!vehicles.ContainsKey(regNumber))
        {
            int spot = FindEmptySpot();
            if (spot != -1)
            {
                pLot[spot] = new CustomersVehicle(regNumber, "CAR", spot + 1);
                vehicles.Add(regNumber, "CAR");
                Console.WriteLine($"Car with reg number '{regNumber}' added to spot {spot + 1}.");
            }
            else
            {
                Console.WriteLine("No available space to add a car.");
            }
        }
        else
        {
            Console.WriteLine("Vehicle with this registration number already exists.");
        }
    }

    public void AddMCs(string regNumber)
    {
        if (!vehicles.ContainsKey(regNumber))
        {
            int spot = FindSpotForMC();
            if (spot != -1)
            {
                if (pLot[spot] == null) // If the spot is empty, add the first MC
                {
                    pLot[spot] = new CustomersVehicle(regNumber, "MC", spot + 1);
                }
                else if (pLot[spot].VehicleType == "MC1" || pLot[spot].VehicleType == "MC") // Add a second MC to the lot
                {
                    pLot[spot].PlateNum += $" & {regNumber}";
                    pLot[spot].VehicleType = "MC2"; // Mark second MC
                }

                vehicles.Add(regNumber, "MC");
                Console.WriteLine($"Motorcycle with reg number '{regNumber}' added to spot {spot + 1}.");
            }
            else
            {
                Console.WriteLine("No available space to add a motorcycle.");
            }
        }
        else
        {
            Console.WriteLine("Vehicle with this registration number already exists.");
        }
    }

    // Remove a vehicle by registration number
    public void RemoveVehicle(string regNumber)
    {
        if (vehicles.ContainsKey(regNumber))
        {
            for (int i = 0; i < pLot.Length; i++)
            {
                if (pLot[i] != null && (pLot[i].PlateNum.Contains(regNumber)))
                {
                    Console.WriteLine($"{pLot[i].VehicleType} with registration number '{regNumber}' removed from spot {i + 1}.");

                    // If it's two MCs, just remove the correct one
                    if (pLot[i].VehicleType == "MC2")
                    {
                        pLot[i].PlateNum = pLot[i].PlateNum.Replace($" & {regNumber}", "");
                        pLot[i].VehicleType = "MC1"; // Reset back to single MC
                    }
                    else
                    {
                        pLot[i] = null; // Remove the car or single MC
                    }

                    vehicles.Remove(regNumber);
                    break;
                }
            }
        }
    }

    // Check if a vehicle exists by registration number
    public bool Exists(string regNumber)
    {
        return vehicles.ContainsKey(regNumber);
    }

    // Get the vehicle type and lot by registration number
    public (string, int) GetVehicleTypeAndLot(string regNumber)
    {
        for (int i = 0; i < pLot.Length; i++)
        {
            if (pLot[i] != null && pLot[i].PlateNum.Contains(regNumber))
            {
                // Return only the matching MC or vehicle type for that registration
                if (pLot[i].VehicleType == "MC2" && pLot[i].PlateNum.Contains(regNumber))
                {
                    return ("MC", i + 1);
                }
                else if (pLot[i].VehicleType == "MC1" || pLot[i].VehicleType == "MC")
                {
                    return ("MC", i + 1);
                }
                return (pLot[i].VehicleType, i + 1);
            }
        }
        return (null, -1); // Vehicle not found
    }

    // Find an empty spot for a car
    public int FindEmptySpot()
    {
        for (int i = 0; i < pLot.Length; i++)
        {
            if (pLot[i] == null)
            {
                return i;
            }
        }
        return -1; // No empty spot available
    }

    // Find a spot for a motorcycle (either an empty spot or one with only one motorcycle)
    private int FindSpotForMC()
    {
        for (int i = 0; i < pLot.Length; i++)
        {
            if (pLot[i] == null || pLot[i].VehicleType == "MC" || pLot[i].VehicleType == "MC1") // Check if there's room for 2nd MC
            {
                return i;
            }
        }
        return -1; // No spot available
    }

    // View the parking lot (with separate MC1 and MC2)
    public void ViewLot()
    {
        for (int i = 0; i < pLot.Length; i++)
        {
            if (pLot[i] != null)
            {
                if (pLot[i].VehicleType == "MC2") // If two MCs are in the same spot
                {
                    string[] mcPlates = pLot[i].PlateNum.Split('&');
                    Console.WriteLine($"Lot {i + 1}: MC1 with reg number {mcPlates[0].Trim()}, MC2 with reg number {mcPlates[1].Trim()}");
                }
                else
                {
                    Console.WriteLine($"Lot {i + 1}: {pLot[i].VehicleType} with reg number {pLot[i].PlateNum}");
                }
            }
        }
    }

    public void MoveVehicle(string regNumber, int currentLot, int newLot)
    {
        if (vehicles.ContainsKey(regNumber))
        {
            for (int i = 0; i < pLot.Length; i++)
            {
                if (pLot[i] != null && pLot[i].PlateNum.Contains(regNumber))
                {
                    pLot[newLot] = pLot[i];
                    pLot[i] = null;
                    Console.WriteLine($"{pLot[newLot].VehicleType} with registration number '{regNumber}' moved from lot {currentLot + 1} to lot {newLot + 1}.");
                    break;
                }
            }
        }
    }

    public string GetStatus()
    {
        int availableSpaces = 0;
        for (int i = 0; i < pLot.Length; i++)
        {
            if (pLot[i] == null || (pLot[i].VehicleType == "MC" && !pLot[i].PlateNum.Contains("&"))) // Account for single MC spaces
            {
                availableSpaces++;
            }
        }
        return $"Available: {availableSpaces} / {pLot.Length}";
    }
}

// Define the CustomersVehicle class
public class CustomersVehicle
{
    public string PlateNum { get; set; }
    public string VehicleType { get; set; }
    public int TicketLot { get; set; }

    public CustomersVehicle(string plateNum, string vehicleType, int ticketLot)
    {
        this.PlateNum = plateNum;
        this.VehicleType = vehicleType;
        this.TicketLot = ticketLot;
    }
}

