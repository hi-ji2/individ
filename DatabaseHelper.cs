using Npgsql;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace TourBookingApp
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Tour Methods
        public DataTable GetActiveTours()
        {
            return ExecuteQuery("SELECT * FROM tours WHERE is_active = TRUE");
        }

        public DataTable GetTourDetails(int tourId)
        {
            return ExecuteQuery(@"
                SELECT t.tour_id, t.tour_name, tt.type_name, h.hotel_name, c.city_name, co.country_name, 
                       t.start_date, t.end_date, t.price, t.available_seats
                FROM tours t
                JOIN tour_types tt ON t.type_id = tt.type_id
                JOIN hotels h ON t.hotel_id = h.hotel_id
                JOIN cities c ON h.city_id = c.city_id
                JOIN countries co ON c.country_id = co.country_id
                WHERE t.tour_id = @tourId",
                new NpgsqlParameter("tourId", tourId));
        }

        public void AddTour(string name, int typeId, int hotelId, DateTime startDate, DateTime endDate, decimal price, int seats)
        {
            ExecuteNonQuery(
                "INSERT INTO tours (tour_name, type_id, hotel_id, start_date, end_date, price, available_seats) " +
                "VALUES (@name, @typeId, @hotelId, @startDate, @endDate, @price, @seats)",
                new NpgsqlParameter("name", name),
                new NpgsqlParameter("typeId", typeId),
                new NpgsqlParameter("hotelId", hotelId),
                new NpgsqlParameter("startDate", startDate),
                new NpgsqlParameter("endDate", endDate),
                new NpgsqlParameter("price", price),
                new NpgsqlParameter("seats", seats));
        }

        public void UpdateTour(int tourId, string name, int typeId, int hotelId, DateTime startDate, DateTime endDate, decimal price, int seats, bool isActive)
        {
            ExecuteNonQuery(
                "UPDATE tours SET tour_name = @name, type_id = @typeId, hotel_id = @hotelId, " +
                "start_date = @startDate, end_date = @endDate, price = @price, " +
                "available_seats = @seats, is_active = @isActive WHERE tour_id = @tourId",
                new NpgsqlParameter("tourId", tourId),
                new NpgsqlParameter("name", name),
                new NpgsqlParameter("typeId", typeId),
                new NpgsqlParameter("hotelId", hotelId),
                new NpgsqlParameter("startDate", startDate),
                new NpgsqlParameter("endDate", endDate),
                new NpgsqlParameter("price", price),
                new NpgsqlParameter("seats", seats),
                new NpgsqlParameter("isActive", isActive));
        }

        public void DeleteTour(int tourId)
        {
            ExecuteNonQuery(
                "DELETE FROM tours WHERE tour_id = @tourId",
                new NpgsqlParameter("tourId", tourId));
        }
        #endregion

        #region Client Methods
        public DataTable GetAllClients()
        {
            return ExecuteQuery("SELECT * FROM clients");
        }

        public DataTable GetClientDetails(int clientId)
        {
            return ExecuteQuery(
                "SELECT * FROM clients WHERE client_id = @clientId",
                new NpgsqlParameter("clientId", clientId));
        }

        public void AddClient(string firstName, string lastName, string passport, string phone, string email, DateTime? birthDate)
        {
            ExecuteNonQuery(
                "INSERT INTO clients (first_name, last_name, passport_number, phone, email, birth_date) " +
                "VALUES (@firstName, @lastName, @passport, @phone, @email, @birthDate)",
                new NpgsqlParameter("firstName", firstName),
                new NpgsqlParameter("lastName", lastName),
                new NpgsqlParameter("passport", passport),
                new NpgsqlParameter("phone", phone ?? (object)DBNull.Value),
                new NpgsqlParameter("email", email ?? (object)DBNull.Value),
                new NpgsqlParameter("birthDate", birthDate ?? (object)DBNull.Value));
        }

        public void UpdateClient(int clientId, string firstName, string lastName, string passport, string phone, string email, DateTime? birthDate)
        {
            ExecuteNonQuery(
                "UPDATE clients SET first_name = @firstName, last_name = @lastName, " +
                "passport_number = @passport, phone = @phone, email = @email, birth_date = @birthDate " +
                "WHERE client_id = @clientId",
                new NpgsqlParameter("clientId", clientId),
                new NpgsqlParameter("firstName", firstName),
                new NpgsqlParameter("lastName", lastName),
                new NpgsqlParameter("passport", passport),
                new NpgsqlParameter("phone", phone ?? (object)DBNull.Value),
                new NpgsqlParameter("email", email ?? (object)DBNull.Value),
                new NpgsqlParameter("birthDate", birthDate ?? (object)DBNull.Value));
        }
        #endregion

        #region Booking Methods
        public DataTable GetAllBookings()
        {
            return ExecuteQuery("SELECT * FROM bookings");
        }

        public void AddBooking(int tourId, int clientId, int seats, decimal totalPrice)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Добавляем бронирование
                        ExecuteNonQuery(conn,
                            "INSERT INTO bookings (tour_id, client_id, seats, total_price) " +
                            "VALUES (@tourId, @clientId, @seats, @totalPrice)",
                            new NpgsqlParameter("tourId", tourId),
                            new NpgsqlParameter("clientId", clientId),
                            new NpgsqlParameter("seats", seats),
                            new NpgsqlParameter("totalPrice", totalPrice));

                        // Обновляем количество мест
                        ExecuteNonQuery(conn,
                            "UPDATE tours SET available_seats = available_seats - @seats " +
                            "WHERE tour_id = @tourId",
                            new NpgsqlParameter("tourId", tourId),
                            new NpgsqlParameter("seats", seats));

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void CancelBooking(int bookingId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Получаем информацию о бронировании
                        var bookingInfo = ExecuteQuery(conn,
                            "SELECT tour_id, seats FROM bookings WHERE booking_id = @bookingId",
                            new NpgsqlParameter("bookingId", bookingId));

                        if (bookingInfo.Rows.Count == 0)
                            throw new Exception("Бронирование не найдено");

                        int tourId = Convert.ToInt32(bookingInfo.Rows[0]["tour_id"]);
                        int seats = Convert.ToInt32(bookingInfo.Rows[0]["seats"]);

                        // Обновляем статус бронирования
                        ExecuteNonQuery(conn,
                            "UPDATE bookings SET status = 'cancelled' WHERE booking_id = @bookingId",
                            new NpgsqlParameter("bookingId", bookingId));

                        // Возвращаем места
                        ExecuteNonQuery(conn,
                            "UPDATE tours SET available_seats = available_seats + @seats " +
                            "WHERE tour_id = @tourId",
                            new NpgsqlParameter("tourId", tourId),
                            new NpgsqlParameter("seats", seats));

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region Reference Data Methods
        public DataTable GetCountries()
        {
            return ExecuteQuery("SELECT * FROM countries");
        }

        public DataTable GetCities(int countryId)
        {
            return ExecuteQuery(
                "SELECT * FROM cities WHERE country_id = @countryId",
                new NpgsqlParameter("countryId", countryId));
        }

        public DataTable GetHotels(int cityId)
        {
            return ExecuteQuery(
                "SELECT * FROM hotels WHERE city_id = @cityId",
                new NpgsqlParameter("cityId", cityId));
        }

        public DataTable GetTourTypes()
        {
            return ExecuteQuery("SELECT * FROM tour_types");
        }

        public DataTable GetRevenueByCountry()
        {
            return ExecuteQuery(@"
                SELECT co.country_name, SUM(b.total_price) AS total_revenue
                FROM bookings b
                JOIN tours t ON b.tour_id = t.tour_id
                JOIN hotels h ON t.hotel_id = h.hotel_id
                JOIN cities c ON h.city_id = c.city_id
                JOIN countries co ON c.country_id = co.country_id
                WHERE b.status != 'cancelled'
                GROUP BY co.country_name
                ORDER BY total_revenue DESC");
        }
        #endregion

        #region User Authentication
        public bool AuthenticateUser(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var result = ExecuteScalar<long>(
                "SELECT COUNT(1) FROM users WHERE username = @username AND password = @password",
                new NpgsqlParameter("username", username),
                new NpgsqlParameter("password", hashedPassword));

            return result == 1;
        }

        public bool RegisterUser(string username, string password)
        {
            // Проверяем, существует ли пользователь
            var userExists = ExecuteScalar<long>(
                "SELECT COUNT(1) FROM users WHERE username = @username",
                new NpgsqlParameter("username", username)) > 0;

            if (userExists)
                return false;

            // Регистрируем нового пользователя
            var hashedPassword = HashPassword(password);
            var rowsAffected = ExecuteNonQuery(
                "INSERT INTO users (username, password, role) VALUES (@username, @password, 'user')",
                new NpgsqlParameter("username", username),
                new NpgsqlParameter("password", hashedPassword));

            return rowsAffected == 1;
        }

        public string GetUserRole(string username)
        {
            return ExecuteScalar<string>(
                "SELECT role FROM users WHERE username = @username",
                new NpgsqlParameter("username", username)) ?? "user";
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion

        #region Helper Methods
        private DataTable ExecuteQuery(string query, params NpgsqlParameter[] parameters)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SET SEARCH_PATH TO individ; " + query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        private DataTable ExecuteQuery(NpgsqlConnection conn, string query, params NpgsqlParameter[] parameters)
        {
            using (var cmd = new NpgsqlCommand("SET SEARCH_PATH TO individ; " + query, conn))
            {
                cmd.Parameters.AddRange(parameters);
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private int ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SET SEARCH_PATH TO individ; " + query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        private int ExecuteNonQuery(NpgsqlConnection conn, string query, params NpgsqlParameter[] parameters)
        {
            using (var cmd = new NpgsqlCommand("SET SEARCH_PATH TO individ; " + query, conn))
            {
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        private T ExecuteScalar<T>(string query, params NpgsqlParameter[] parameters)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SET SEARCH_PATH TO individ; " + query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    var result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
                }
            }
        }
        #endregion
    }
}