namespace HairWizardWebAPI.DTOs
{

    public class BookingDto
    {
        public int BookingId { get; set; }
        public string Treatment { get; set; } = string.Empty;
        public string Employee { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}
