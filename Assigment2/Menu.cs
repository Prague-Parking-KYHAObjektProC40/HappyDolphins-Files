using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
// using Newtonsoft.Json;

using Spectre.Console;
using System.Globalization;
using System.Linq;

namespace FordonApp
{
    public class Menu
    {
        /*ParkeringsHus parkeringsHus = LoadParkingData();*/
        ParkingGarage parkingGarage = new ParkingGarage(100);
        public Menu(ParkingGarage parkingGarage)
        {
            this.parkingGarage = parkingGarage;
        }

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
        public void MainMenu()

        {
            while (true)
            {
                DisplayHeader();
                string selectedOption = DisplayOptions();

                string choice = selectedOption.Split(':')[0]; // Extract the number from the selected option
                switch (choice)
                {
                    case "1": AddVehicle(); break;
                    case "2": RemoveVehicle(); break;
                    case "3": ViewVehicleParked(); break;
                    case "4": FindVehicle(); break;
                    case "5": MoveVehicle(); break;
                    case "6": ParkingMap(); break;
                    case "0": AnsiConsole.Markup("[red]Exiting Program...[/]"); Environment.Exit(0); break;
                    default: AnsiConsole.Markup("[red]Invalid Option. Please try again![/]"); break;
                }
            }
        }
        public static void DisplayHeader()
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText(" Luxury Garage")
                    .Centered()
                    .Color(Color.Gold1));

            AnsiConsole.MarkupLine("[bold underline]Welcome to our luxury garage[/]");
            AnsiConsole.MarkupLine($"[dim]{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss, dddd", CultureInfo.InvariantCulture)}[/]");
            // Call DisplayPriceList from the Price_Display class
            Price_Display.DisplayPriceList();


            /*
                Console.WriteLine("<<<<<<<<<<<<<<<<<<+>>>>>>>>>>>>>>>>>>"
                              + "\n<<  Welcome to our luxury garage   >>"
                + $"\n<<  {DateTime.Now}, {DateTime.Today.DayOfWeek}  >>"
                           + "\n<<<<<<<<<<<<<<<<<<+>>>>>>>>>>>>>>>>>>\n");
            */
        }


        public static string DisplayOptions()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please choose from the [green]menu options[/]:")
                    .AddChoices(new[]
                    {
                        "1: Add New Customer",
                        "2: Remove Customer",
                        "3: View Parked Vehicle",
                        "4: Find Vehicle",
                        "5: Move a Vehicle",
                        "6: View Map",
                        "0: Exit Program"
                    }));
        }

        public void AddVehicle()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]----- Add New Customer -----[/]");

            string vehicleType = AnsiConsole.Ask<string>("Enter 'CAR' to add a car, 'MC' to add a motorcycle, 'BUS' to add a bus, or 'BIKE' to add a bicycle:").ToUpper();
            while (true)
            {
                // Validation logic: 2 to 4 characters, no spaces
                if (!string.IsNullOrEmpty(vehicleType) && vehicleType.Length >= 2 && vehicleType.Length <= 4 && !vehicleType.Contains(" "))
                {
                    string registrationNumber = null;
                    // Validate registration number input
                    while (true)
                    {
                        registrationNumber = AnsiConsole.Ask<string>("Enter the registration number:");
                        registrationNumber = registrationNumber.ToUpper();

                        // Validation logic: 1 to 10 characters, no spaces
                        if (!string.IsNullOrEmpty(registrationNumber) && registrationNumber.Length >= 1 && registrationNumber.Length <= 10 && !registrationNumber.Contains(" "))
                        {
                            break;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Invalid registration number. It must be between 1 to 10 characters with no spaces. Please try again.[/]");
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
                            parkingGarage.ParkVehicles(vehicle, DateTime.Now);
                            AnsiConsole.MarkupLine("[green]Vehicle added successfully.[/]");
                            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]"); Console.ReadKey();
                            break;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Invalid vehicle type entered. Please try again.[/]");
                            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]"); Console.ReadKey();
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Invalid input. Please provide both vehicle type and registration number. Try again.[/]");
                        AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]"); Console.ReadKey();
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Invalid vehicle type. It must be one of CAR, MC, BUS, or BIKE with no spaces. Please try again.[/]");
                    AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]"); Console.ReadKey();
                }
                break;
            }
        }
        public void RemoveVehicle()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold red]----- Remove a Customer -----[/]");
            string regNumberToRemove = AnsiConsole.Ask<string>("Enter registration number to remove vehicle\nType 'EXIT' to return:");
            if (!string.IsNullOrEmpty(regNumberToRemove))
            {
                parkingGarage.RemoveVehicles(regNumberToRemove);
            }
            //else  //denna else behövs inte då de fångar redan upp en annan error message i parkingSpot
            //{
            //    AnsiConsole.MarkupLine("[red]Invalid input. Please provide a registration number.[/]");
            //}
        }
        public void ViewVehicleParked()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold yellow]----- Current vehicles parked -----[/]");

            foreach (var spot in parkingGarage.GetAllParkingSpots())
            {
                if (spot.VehicleOnLot.Count > 0)
                {
                    AnsiConsole.MarkupLine($"[yellow]Parking Spot {spot.PlaceNumber} contains:[/]");
                    foreach (var parkedVehicle in spot.VehicleOnLot.OrderBy(v => v.ParkedTime))
                    {
                        AnsiConsole.MarkupLine($"- [green]{parkedVehicle.Type}[/] with registration number [blue]{parkedVehicle.RegistrationNumber}[/], Parked since: {parkedVehicle.ParkedTime}");
                    }
                }
            }

            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
            Console.ReadKey();
        }
        public void FindVehicle()
        {
            AnsiConsole.Clear();
            string regNumberToFind = AnsiConsole.Ask<string>("Enter the registration number to find the vehicle:");

            if (!string.IsNullOrEmpty(regNumberToFind))
            {
                var foundVehicle = parkingGarage.FindVehicleByRegistration(regNumberToFind);
                if (foundVehicle.HasValue)
                {
                    AnsiConsole.MarkupLine($"[green]Vehicle found:[/] {foundVehicle.Value.vehicle.Type} with registration number {foundVehicle.Value.vehicle.RegistrationNumber} at spot {foundVehicle.Value.PlaceNumber}.");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Vehicle not found.[/]");
                }
            }
        }
        public void MoveVehicle()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]----- Move a Vehicle -----[/]");
            string regNumberToMove = AnsiConsole.Ask<string>("Enter the registration number to move the vehicle:");

            if (!string.IsNullOrEmpty(regNumberToMove))
            {
                int targetSpotNumber = AnsiConsole.Ask<int>("Enter the target parking spot number:");
                bool success = parkingGarage.MoveVehicle(regNumberToMove, targetSpotNumber);

                if (!success)
                {
                    AnsiConsole.MarkupLine("[red]Unable to move the vehicle to the specified spot.[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid input. Please provide a registration number.[/]");
            }
        }
        public void ParkingMap()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]----- Parking Map -----[/]");

            var allParkingSpots = parkingGarage.GetAllParkingSpots();

            for (int i = 0; i < allParkingSpots.Count; i++)
            {
                var spot = allParkingSpots[i];
                if (spot.VehicleOnLot.Count > 0)
                {
                    AnsiConsole.MarkupLine($"[red]{Parkeringsruta[i]} - Occupied by {spot.VehicleOnLot[0].Type}[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[green]{Parkeringsruta[i]} - Available[/]");
                }
            }

            AnsiConsole.MarkupLine("[dim]Press any key to return to the main menu...[/]");
            Console.ReadKey();
        }
        //    Function to save parking data to JSON
        //    static void SaveParkingData(ParkeringsHus parkeringsHus)
        //{
        //    string json = JsonConvert.SerializeObject(parkeringsHus, Newtonsoft.Json.Formatting.Indented);
        //    File.WriteAllText("parkingData.json", json);
        //    Console.WriteLine("Parking data saved to parkingData.json.");
        //}


        //Function to load parking data from JSON
        //    static ParkeringsHus LoadParkingData()
        //{
        //    if (File.Exists("parkingData.json"))
        //    {
        //        string json = File.ReadAllText("parkingData.json");
        //        return JsonConvert.DeserializeObject<ParkeringsHus>(json) ?? new ParkeringsHus(100);
        //    }
        //    else
        //    {
        //        Console.WriteLine("No saved data found. Initializing with empty parking structure.");
        //        return new ParkeringsHus(100);
        //    }
        //}
    }





    public enum VehicleType //Kanske behöver flyttas
    { Car, MC, Bus, Bicycle }
    public enum ParkingSpotStatus
    { Empty, Busy }
}