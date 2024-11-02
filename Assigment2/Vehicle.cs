namespace FordonApp
{
    public abstract class Vehicle
    {
        public Vehicle(string registrationNumber, VehicleType type, int size, DateTime parkedTimeStarted) //Kev
        {
            RegistrationNumber = registrationNumber;
            Type = type;
            Size = size;
            ParkedTime = parkedTimeStarted; //Kev
        }
        public string RegistrationNumber { get; set; }
        public VehicleType Type { get; set; }
        public int Size { get; set; } // Storleksegenskap
        public DateTime ParkedTime { get; set; } //Kev
        public string GetParkedDate() { string spd = ParkedTime.ToString("dd:MM:yyyy"); return spd; } //Kev
        public string GetParkedTime() { string spt = ParkedTime.ToString("HH:mm:ss"); return spt; } //Kev

        public abstract void Starta();
    }
}