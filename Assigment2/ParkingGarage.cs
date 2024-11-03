using Spectre.Console;

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
            {
                AnsiConsole.MarkupLine($"[red]No available parking spaces for vehicle with registration numbers: {vehicle.RegistrationNumber}[/]");
                AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
                Console.ReadKey();
            }
        }
        public void RemoveVehicles(string registrationNumber)
        {
            foreach (var spot in ParkingSpaces)
            {
                var vehicle = spot.VehicleOnLot.Find(f => f.RegistrationNumber == registrationNumber);
                if (vehicle != null)
                { spot.RemoveVehicles(vehicle); return; }
            }
            AnsiConsole.MarkupLine($"[red]Vehicle with registration number: {registrationNumber} does not exist in the system.[/]");
            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
            Console.ReadKey();
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
                AnsiConsole.MarkupLine("[red]Invalid parking spot number.[/]");
                AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
                Console.ReadKey();
                return false;
            }
            var foundSpot = FindVehicleByRegistration(registrationNumber);
            if (!foundSpot.HasValue)
            {
                AnsiConsole.MarkupLine("[red]Vehicle not found.[/]");
                AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
                Console.ReadKey();
                return false;
            }
            var vehicleToMove = foundSpot.Value.vehicle;
            int originalPlaceNumber = foundSpot.Value.PlaceNumber;
            if (vehicleToMove.Type == VehicleType.Bus && targetSpotNumber > 50)
            {
                AnsiConsole.MarkupLine("[red]Buses can only park in spots 1-50.[/]");
                AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
                Console.ReadKey();
                return false;
            }
            ParkingSpot targetSpot = ParkingSpaces[targetSpotNumber - 1];
            if (!targetSpot.CanPark(vehicleToMove))
            {
                AnsiConsole.MarkupLine($"[red]Spot {targetSpotNumber} cannot accommodate this vehicle.[/]");
                AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
                Console.ReadKey();
                return false;
            }
            ParkingSpot currentSpot = ParkingSpaces[originalPlaceNumber - 1];
            currentSpot.RemoveVehicles(vehicleToMove);
            //måste lägga till ny text när de flyttas...
            targetSpot.ParkVehicle(vehicleToMove);
            return true;
        }
    }
}