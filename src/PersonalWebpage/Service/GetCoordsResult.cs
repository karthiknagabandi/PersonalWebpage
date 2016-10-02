namespace PersonalWebpage.Service
{
    public class GetCoordsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }
    }
}