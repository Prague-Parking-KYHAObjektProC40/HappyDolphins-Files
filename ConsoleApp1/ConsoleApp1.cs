[Flags]
public enum Vehicle
{
    None = 0,  // 0
    Bike = 1,  // 1
    Mc = 2,  // 2
    Car = 4,  // 4
    Buss = 8,  // 8

    MC2 = Mc + Mc

}

public class FlagsEnumExample
{
    public static void Main()
    {
        Vehicle vehicle_Type = Vehicle.None | Vehicle.Bike | Vehicle.Mc | Vehicle.Car | Vehicle.Buss;
        Console.WriteLine(vehicle_Type); // Output: Bike, Mc, Car, Buss

        Vehicle none_Vehicle = Vehicle.None;
        Console.WriteLine(none_Vehicle);
        // Output:
        // None

        Vehicle vehicle_Bike = Vehicle.Bike;
        Console.WriteLine(vehicle_Bike);
        // Output:
        // Bike

        Vehicle vehicle_Mc = Vehicle.Mc;
        Console.WriteLine(vehicle_Mc);
        // Output:
        // Mc

        Vehicle vehicle_Car = Vehicle.Car;
        Console.WriteLine(vehicle_Car);
        // Output:
        // Car

        Vehicle vehicle_Buss = Vehicle.Buss;
        Console.WriteLine(vehicle_Buss);
        // Output:
        // Buss

        /* Vehicle vehicle_Car = Vehicle.Car;
        Console.WriteLine($"Get youre parked car {vehicle_Type & vehicle_Car}");
        Output:
        Join a meeting by phone on Friday */

        /*bool is_CarParked = (vehicle_Type = Vehicle.Car);
        Console.WriteLine($"Get youre parked Car : {is_CarParked}"); // Output: True */

        bool isVehicleParked = (vehicle_Type != Vehicle.None);
        Console.WriteLine($"Get youre parked vehicle : {isVehicleParked}"); // Output: True

        var a = (Vehicle)100;
        Console.WriteLine(a); // Output: 100


        Vehicle vehicleType = Vehicle.None | Vehicle.Bike | Vehicle.Mc | Vehicle.Car | Vehicle.Buss;
        Console.WriteLine(vehicleType); // Output: Bike, Mc, Car, Buss

        Console.WriteLine($"Get your parked vehicle: {vehicleType}");

        Console.WriteLine($"Is Car parked: {IsVehicleParked(vehicleType, Vehicle.Car)}"); // Output: True
        Console.WriteLine($"Is Bike parked: {IsVehicleParked(vehicleType, Vehicle.Bike)}"); // Output: True
        Console.WriteLine($"Is MC parked: {IsVehicleParked(vehicleType, Vehicle.Mc)}"); // Output: True
        Console.WriteLine($"Is Buss parked: {IsVehicleParked(vehicleType, Vehicle.Buss)}"); // Output: True
        Console.WriteLine($"Is None parked: {IsVehicleParked(vehicleType, Vehicle.None)}"); // Output: False
    }

    public static bool IsVehicleParked(Vehicle parkedVehicles, Vehicle vehicle)
    {
        return (parkedVehicles & vehicle) == vehicle;
    }


}