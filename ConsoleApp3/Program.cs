using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace V2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<VehicleInput> vehicles = new List<VehicleInput>();

            Console.WriteLine("Enter vehicle type:");
            string vehicleType = Console.ReadLine();

            Console.WriteLine("Enter plate number:");
            string plateNumber = Console.ReadLine();

            VehicleInput vehicle = new VehicleInput
            {
                VehicleType = vehicleType,
                PlateNumber = plateNumber
            };

            vehicles.Add(vehicle);

            // Serialize to JSON
            string json = JsonConvert.SerializeObject(vehicles, Formatting.Indented);

            // Save to file
            File.WriteAllText("vehicles.json", json);

            Console.WriteLine("Data saved to vehicles.json");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "vehicles.json"); File.WriteAllText(filePath, json); Console.WriteLine($"Data saved to {filePath}");
        }
    }

    public class VehicleInput
    {
        public string VehicleType { get; set; }
        public string PlateNumber { get; set; }
    }
}
