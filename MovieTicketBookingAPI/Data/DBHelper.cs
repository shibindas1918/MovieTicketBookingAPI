using Microsoft.AspNetCore.Http.HttpResults;
using MovieTicketBookingAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace MovieTicketBookingAPI.Data
{
    public class DBHelper
    {
        private readonly string _configuration;
        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnectionString");

        }

        public DataSet ExecuteQuery(string query, SqlParameter[] sqlParameters)
        {
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
                if (sqlParameters != null)
                {
                    sqlDataAdapter.SelectCommand.Parameters.AddRange(sqlParameters);
                }
                DataSet ds = new DataSet();
                sqlDataAdapter.Fill(ds);
                return ds;
            }
        }
        public List<Booking> ExecuteGetQuery()
        {
            List<Booking> booking = new List<Booking>();
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select *from bookings", conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Booking bookings = new Booking
                    {
                        BookingId = sqlDataReader.GetInt32(0),
                        ShowTimeId = sqlDataReader.GetInt32(1),
                        NumberOfTickets = sqlDataReader.GetInt32(2),
                        TotalPrice = sqlDataReader.GetDecimal(3),
                        BookingDate = sqlDataReader.GetDateTime(4)

                    };
                    booking.Add(bookings);
                    
                }
                while (sqlDataReader.Read())
                {
                    Movie movie = new Movie
                    {
                        Description = sqlDataReader.GetString(0),
                        MovieId = sqlDataReader.GetInt32(1),
                        Title = sqlDataReader.GetString(2),
                        Duration = sqlDataReader.GetInt32(3)
                    };
                }


                return booking;
            }


        }
        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select *from movies ", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Movie movie = new Movie
                    {
                        MovieId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        Duration = reader.GetInt32(3),
                        ReleaseDate = reader.GetDateTime(4)

                    };
                    movies.Add(movie);
                }
                return (movies);
            }
        }
        public List<Theater> GetTheater()
        {
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                List<Theater> theaters = new List<Theater>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("select *from theaters", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Theater theater = new Theater
                    {
                        TheaterId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                    };
                    theaters.Add(theater);
                }
                return theaters;
            }

        }
    }
}