namespace DownHillParkAPI.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
