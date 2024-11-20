namespace MovieTicketBookingAPI.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } 
        public DateTime ReleaseDate { get; set; }
    }
}
