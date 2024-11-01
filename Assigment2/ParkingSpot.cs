namespace FordonApp
{
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
    }
            


        

