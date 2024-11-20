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
    public class MovieController : ControllerBase
    {
        private readonly DBHelper _dBHelper;
        public MovieController(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;   
        }
        [HttpPost("showtimes/{movieId}")] 
        public IActionResult GetShowTimes( int movieId)
        {
            try
            {
                string query = @"SELECT st.ShowTimeId, m.Title, t.Name AS TheaterName, st.ShowDateTime, st.Price 
                             FROM ShowTimes st
                             INNER JOIN Movies m ON st.MovieId = m.MovieId
                             INNER JOIN Theaters t ON st.TheaterId = t.TheaterId
                             WHERE m.MovieId = @MovieId";
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@MovieId",movieId)
                };
                DataSet dataSet = _dBHelper.ExecuteQuery(query, sqlParameter);
                DataTable showtimetb = dataSet.Tables[0];

                var showtime = new List<ShowTime>();
                foreach (DataRow row in showtimetb.Rows)
                {
                    showtime.Add(new ShowTime
                    {
                        ShowTimeId = Convert.ToInt32(row["ShowTimeId"]),
                        MovieId = movieId,
                        TheaterId = 0,
                        ShowDateTime = Convert.ToDateTime(row["ShowDateTime"]),
                        Price = Convert.ToDecimal(row["Price"])
                    });
                }
                return Ok(showtime);


            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal sever error" + ex.Message);
            }


        }
        [HttpGet("Get_movies")]
        public ActionResult GetMovies()
        {
            var movies = _dBHelper.GetMovies();
            return Ok(movies);  
        }
    }
}
