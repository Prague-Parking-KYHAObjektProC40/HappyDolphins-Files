using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

[Flags]
public enum Vehicle
{
    None = 0,
    Bike = 1,
    Mc = 2,
    Car = 4,
    Buss = 16,
    MC2 = Mc + Mc
    BIKE2 = Bike + Bike
    BIKE3 = Bike + Bike + Bike
    BIKE4 = Bike + Bike + Bike + Bike
}

public class VehicleInfo
{
    public int ID { get; set; }
    public string Type { get; set; }
}

public class ParkingApp
{
    public static void Main()
    {
        // Read vehicles from JSON
        string json = File.ReadAllText("vehicles.json");
        var vehicles = JsonConvert.DeserializeObject<Dictionary<string, List<VehicleInfo>>>(json)["Vehicles"];

        foreach (var vehicle in vehicles)
        {
            Console.WriteLine($"ID: {vehicle.ID}, Type: {vehicle.Type}");
        }

        // Use vehicle data as needed
    }
}
