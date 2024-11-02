namespace FordonApp
{
    // Fortsättning av klasser...

    // Parkeringsplats-klass
    public class ParkingSpot
    {
        public ParkingSpot() { }
        public int PlaceNumber { get; set; }
        public int MaxSize { get; set; }
        public bool GotHighCeilings { get; set; }
        public int BusySize { get; private set; } = 0;
        public List<Vehicle> VehicleOnLot { get; private set; } = new List<Vehicle>();
        public enum VehiclePrice { Bicycle = 5, MC = 10, Car = 20, Bus = 80 }

        public ParkingSpot(int placeNumber, bool _HighCeilings)
        {
            PlaceNumber = placeNumber;
            MaxSize = _HighCeilings ? 16 : 4;
            GotHighCeilings = _HighCeilings;
        }

        public bool CanPark(Vehicle vehicle)
        {
            // Check if the spot already has a car or bus; if so, it can't accept other vehicles.
            if (VehicleOnLot.Exists(v => v.Type == VehicleType.Car || v.Type == VehicleType.Bus)) { return false; }

            // Check if the vehicle is a bus, and only allow it if the spot is within 1-50 and has no other vehicles.
            if (vehicle.Type == VehicleType.Bus) { return PlaceNumber <= 50 && GotHighCeilings && VehicleOnLot.Count == 0; }

            // Allow two MCs in a spot
            if (vehicle.Type == VehicleType.MC) { return VehicleOnLot.Count(v => v.Type == VehicleType.MC) < 2; }

            // Allow up to four bicycles in a spot
            if (vehicle.Type == VehicleType.Bicycle) { return VehicleOnLot.Count(v => v.Type == VehicleType.Bicycle) < 4; }

            // Allow one car only if the spot is empty
            if (vehicle.Type == VehicleType.Car) { return VehicleOnLot.Count == 0; } return false; // Default to not allowing parking
        }
        public void ParkVehicle(Vehicle vehicle)
        {
            if (CanPark(vehicle))
            {
                VehicleOnLot.Add(vehicle);
                BusySize += vehicle.Size;
                Console.WriteLine($"Vehicle {vehicle.Type} with registration number {vehicle.RegistrationNumber} parked at spot {PlaceNumber}.");
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
                double fee = CollectFee(vehicle); //kev
                BusySize -= vehicle.Size;
                Console.WriteLine($"Vehicle {vehicle.Type} with registration number {vehicle.RegistrationNumber} has left spot {PlaceNumber}. Fee: {fee} CZK."); //kev
            }
        }
        public double CollectFee(Vehicle vehicle) { RemoveVehicles(vehicle); return CalculateFeeCost(vehicle); } //kev
        public double CalculateFeeCost(Vehicle vehicle) //Kev
        {
            TimeSpan timeParked = DateTime.Now - Convert.ToDateTime(vehicle.ParkedTime);
            double afterTimeParked = timeParked.TotalMinutes;
            double totalFee = 0;
            if (afterTimeParked > 10 &&  afterTimeParked < 120)
            {
                if (vehicle.Type == VehicleType.Bicycle) { totalFee = (int)VehiclePrice.Bicycle * 2; }
                else if (vehicle.Type == VehicleType.MC) { totalFee = (int)VehiclePrice.MC * 2; }
                else if (vehicle.Type == VehicleType.Car) { totalFee = (int)VehiclePrice.Car * 2; }
                else if (vehicle.Type == VehicleType.Bus) { totalFee = (int)VehiclePrice.Bus * 2; }
            }
            else if (timeParked.TotalMinutes >= 120)
            {
                double parkedHours = Math.Ceiling(afterTimeParked / 60);
                if (vehicle.Type == VehicleType.Bicycle) { totalFee = parkedHours * (int)VehiclePrice.Bicycle * 2; }
                else if (vehicle.Type == VehicleType.MC) { totalFee = parkedHours * (int)VehiclePrice.MC * 2; }
                else if (vehicle.Type == VehicleType.Car) { totalFee = parkedHours * (int)VehiclePrice.Car * 2; }
                else if (vehicle.Type == VehicleType.Bus) { totalFee = parkedHours * (int)VehiclePrice.Bus * 2; }
            }
            return totalFee;
        }
    }
}