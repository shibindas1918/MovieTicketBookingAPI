namespace MovieTicketBookingAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int ShowTimeId { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }


        public ShowTime ShowTime { get; set; }
    }
}