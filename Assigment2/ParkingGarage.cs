namespace FordonApp
{
    public class ParkingGarage
    {
        private List<ParkingSpot> ParkingSpaces = new List<ParkingSpot>();
        public ParkingGarage(int totalPlatser)
        {
            for (int i = 1; i <= totalPlatser; i++)
            {
                bool harHogtTak = i <= 50;
                ParkingSpaces.Add(new ParkingSpot(i, harHogtTak));
            }
        }
        public void ParkVehicles(Vehicle vehicle)
        {
            var ledigPlats = ParkingSpaces.Find(plats => plats.CanPark(vehicle));
            if (ledigPlats != null)
            {
                ledigPlats.ParkVehicle(vehicle);
            }
            else
            {
                Console.WriteLine($"Inga lediga parkeringsplatser för fordon med registreringsnummer {vehicle.RegistrationNumber}");
            }
        }

        public void RemoveVehicles(string registreringsNummer)
        {
            foreach (var plats in ParkingSpaces)
            {
                var vehicle = plats.VehicleOnLot.Find(f => f.RegistrationNumber == registreringsNummer);
                if (vehicle != null)
                {
                    plats.RemoveVehicles(vehicle);
                    return;
                }
            }
            Console.WriteLine($"Fordonet med registreringsnummer {registreringsNummer} hittades inte.");
        }

        // New method to get all parking spots
        public List<ParkingSpot> GetAllParkingSpots()
        {
            return ParkingSpaces;
        }

        // New method to find a vehicle by registration number
        public (Vehicle vehicle, int PlaceNumber)? FindVehicleByRegistration(string registreringsNummer)
        {
            foreach (var plats in ParkingSpaces)
            {
                var vehicle = plats.VehicleOnLot.Find(f => f.RegistrationNumber == registreringsNummer);
                if (vehicle != null)
                {
                    return (vehicle, plats.PlaceNumber);
                }
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
            Console.WriteLine($"Vehicle {vehicleToMove.Type} with registration {vehicleToMove.RegistrationNumber} moved to spot {targetSpotNumber}.");
            return true;
        }
    }
}





