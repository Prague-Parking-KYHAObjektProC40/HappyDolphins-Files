class Vehicle
{
    public virtual void VehicleAlert()
    {
        Console.WriteLine("Vehicle is an object.");
    }
}

class Car : Vehicle
{
    public override void VehicleAlert()
    {
        Console.WriteLine("Car is an type of Vehicle");
    }
}

class MC : Vehicle
{
    public override void VehicleAlert()
    {
        Console.WriteLine("MC is a smaller typ of Vehicle!");
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        // Lägg tre Vehicle i en array. Bara för att vi kan...
        Vehicle[] vehicles = new Vehicle[3];

        vehicles[0] = new Vehicle();
        vehicles[1] = new Car();
        vehicles[2] = new MC();

        foreach (var vehicle in vehicles)
        {
            vehicle.VehicleAlert();
        }


        Console.Write("\n\n\nTryck på en tangent...");
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
    }
}