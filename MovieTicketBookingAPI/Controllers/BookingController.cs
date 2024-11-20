using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingAPI.Data;
using MovieTicketBookingAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly DBHelper _dBHelper;
        public BookingController(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }
        [HttpPost]
        public IActionResult BookTickets(int showtimeid, int numoftickets)
        {
            try
            {
                string query = @"INSERT INTO Bookings (ShowTimeId, NumberOfTickets, TotalPrice, BookingDate)
                             VALUES (@ShowTimeId, @NumberOfTickets, 
                             (SELECT Price * @NumberOfTickets FROM ShowTimes WHERE ShowTimeId = @ShowTimeId),
                             GETDATE());";
                SqlParameter[] sp = new SqlParameter[]
                {
                   new SqlParameter("@ShowTimeId",showtimeid),
                   new SqlParameter("@NumberOfTickets",numoftickets)
                };
                _dBHelper.ExecuteQuery(query, sp);
                return Ok("The ticket is successfully booked ");

            }
            catch (Exception ex)
            {
             return    StatusCode(500, "internal server error:" + ex.Message); 
            }

        }
        [HttpPost("POSt")]
        public IActionResult NaturalHeavy(int showtimeid, int numberoftickets, int collectiondate, int totalprice, int bookingdate)
        {
            
            
                string query = @"insert into bookings(showtimeid,numberoftickets,totalprice,bookingdate)
                                  values(@showtimeid,@numberoftickets,
                                   (select price * @numberoftickets from showtimes where showtimeid = @showtimeid),getdate());";
                string query2 = @"insert into bookings(showtimeid,numberoftickets,collectionId,collectiondate,totalprice,bookingdate)
                                  values(@showtimeid,@numberoftickets,@collectionid,@collectiondate,@totalprice,@bookingdate)
                                    (select price * @numberoftickets from showtimes where showtimeid = @showtimeid),getdate());";
                SqlParameter[] sp = new SqlParameter[]
                {
                    new SqlParameter("@showtimeid", showtimeid),
                    new SqlParameter("@numberoftickets", numberoftickets),
                    new SqlParameter("@collectiondate", collectiondate),
                    new SqlParameter("@totalprice", totalprice),
                    new SqlParameter("@bookingdate", bookingdate),
                };
                _dBHelper.ExecuteQuery(query, sp);
                _dBHelper.ExecuteQuery(query2, sp);
                return Ok("The ticket has been booked ");
                        }

        [HttpGet]
        public ActionResult<List<Booking>> AllBookings()
        {
            var books = _dBHelper.ExecuteGetQuery();
            return Ok(books);
        }
    }
}
