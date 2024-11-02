namespace FordonApp
{
    public class ParkingGarage
    {
        private List<ParkingSpot> ParkingSpaces = new List<ParkingSpot>();
        public ParkingGarage(int totalSpot)
        {
            for (int i = 1; i <= totalSpot; i++)
            {
                bool highRoof = i <= 50;
                ParkingSpaces.Add(new ParkingSpot(i, highRoof));
            }
        }
        public void ParkVehicles(Vehicle vehicle, DateTime Now) //kev
        {
            var freeSpot = ParkingSpaces.Find(plats => plats.CanPark(vehicle));
            if (freeSpot != null)
            { freeSpot.ParkVehicle(vehicle); }
            else
            { Console.WriteLine($"No available parking spaces for vehicle with registration numbers: {vehicle.RegistrationNumber}"); }
        }
        public void RemoveVehicles(string registrationNumber)
        {
            foreach (var spot in ParkingSpaces)
            {
                var vehicle = spot.VehicleOnLot.Find(f => f.RegistrationNumber == registrationNumber);
                if (vehicle != null)
                { spot.RemoveVehicles(vehicle); return; }
            }
            Console.WriteLine($"Vehicle with registration number: {registrationNumber} does not exist in the system.");
        }
        // New method to get all parking spots
        public List<ParkingSpot> GetAllParkingSpots()
        { return ParkingSpaces; }
        // New method to find a vehicle by registration number
        public (Vehicle vehicle, int PlaceNumber)? FindVehicleByRegistration(string registrationNumber)
        {
            foreach (var spot in ParkingSpaces)
            {
                var vehicle = spot.VehicleOnLot.Find(f => f.RegistrationNumber == registrationNumber);
                if (vehicle != null)
                { return (vehicle, spot.PlaceNumber); }
            }
            return null; // Return null if no vehicle is found
        }
        public bool MoveVehicle(string registrationNumber, int targetSpotNumber)
        {
            if (targetSpotNumber < 1 || targetSpotNumber > ParkingSpaces.Count)
            {
                Console.WriteLine("Invalid parking spot number.");
                return false;
            }
            var foundSpot = FindVehicleByRegistration(registrationNumber);
            if (!foundSpot.HasValue)
            {
                Console.WriteLine("Vehicle not found.");
                return false;
            }
            var vehicleToMove = foundSpot.Value.vehicle;
            int originalPlaceNumber = foundSpot.Value.PlaceNumber;
            if (vehicleToMove.Type == VehicleType.Bus && targetSpotNumber > 50)
            {
                Console.WriteLine("Buses can only park in spots 1-50.");
                return false;
            }
            ParkingSpot targetSpot = ParkingSpaces[targetSpotNumber - 1];
            if (!targetSpot.CanPark(vehicleToMove))
            {
                Console.WriteLine($"Spot {targetSpotNumber} cannot accommodate this vehicle.");
                return false;
            }
            ParkingSpot currentSpot = ParkingSpaces[originalPlaceNumber - 1];
            currentSpot.RemoveVehicles(vehicleToMove);
            targetSpot.ParkVehicle(vehicleToMove);
            return true;
        }
    }
}