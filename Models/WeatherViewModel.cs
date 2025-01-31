namespace WeatherApp.Models
{
    public class WeatherViewModel
    {
        public string? Location { get; set; }
        public string? Description { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }
    }
}
