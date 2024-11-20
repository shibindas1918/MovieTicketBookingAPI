namespace MovieTicketBookingAPI.Models
{
    public class ShowTime
    {
        public int ShowTimeId { get; set; }
        public int MovieId { get; set; }
        public int TheaterId { get; set; }
        public DateTime ShowDateTime { get; set; }
        public decimal Price { get; set; }

        public Movie Movie { get; set; }
        public Theater Theater { get; set; }
    }
}

