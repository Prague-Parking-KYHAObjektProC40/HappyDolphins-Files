// using Newtonsoft.Json;

using Spectre.Console;
using static FordonApp.ParkingSpot;
using System;
using System.Linq;

namespace FordonApp
{
    public class Price_Display
    {
        public static void DisplayPriceList()
        {
            // Create a table for a simple price display
            var table = new Table();

            // Add columns for vehicle type and hourly rate
            table.AddColumn("Vehicle Type");
            table.AddColumn("Hourly Rate (CZK)");

            // Populate the table with each vehicle type and its hourly rate
            foreach (VehiclePrice vehicle in Enum.GetValues(typeof(VehiclePrice)))
            {
                table.AddRow(vehicle.ToString(), $"${(int)vehicle}");
            }

            // Display the table
            AnsiConsole.Write(table);

            // Add a note below the table about the free 10 minutes
            AnsiConsole.MarkupLine("[gray]* The first 10 minutes are free. Charges start after that.[/]");
        }

    }
}
