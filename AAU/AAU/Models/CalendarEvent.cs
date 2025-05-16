namespace AAU.Models
{
    public class CalendarEvent
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        
        public string StartISO => Start.ToString("yyyy-MM-ddTHH:mm:ss");
        public string EndISO => End.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}
