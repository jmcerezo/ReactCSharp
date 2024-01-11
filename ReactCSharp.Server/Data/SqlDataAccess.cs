using ReactCSharp.Server.Dto;
using ReactCSharp.Server.Models;
using System.Data.SqlClient;

namespace ReactCSharp.Server.Data
{
    public class SqlDataAccess
    {
        public async Task<List<User>> RegisterUser(UserDto userDto, string sqlQuery, string connectionString)
        {
            List<User> users = [];
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new(sqlQuery, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@first_name", userDto.FirstName);
            cmd.Parameters.AddWithValue("@last_name", userDto.LastName);
            cmd.Parameters.AddWithValue("@email_address", userDto.EmailAddress);
            cmd.Parameters.AddWithValue("@phone_number", userDto.PhoneNumber);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User user = new()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    FirstName = Convert.ToString(reader["first_name"])!,
                    LastName = Convert.ToString(reader["last_name"])!,
                    EmailAddress = Convert.ToString(reader["email_address"])!,
                    PhoneNumber = Convert.ToInt64(reader["phone_number"]),
                };

                users.Add(user);
            }
            await reader.CloseAsync();

            return users;
        }

        public async Task<List<User>> GetAllUsers(string sqlQuery, string connectionString)
        {
            List<User> users = [];
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new(sqlQuery, conn);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User user = new()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    FirstName = Convert.ToString(reader["first_name"])!,
                    LastName = Convert.ToString(reader["last_name"])!,
                    EmailAddress = Convert.ToString(reader["email_address"])!,
                    PhoneNumber = Convert.ToInt64(reader["phone_number"]),
                };

                users.Add(user);
            }
            await reader.CloseAsync();

            return users;
        }

        public async Task<User> GetUserById(int id, string sqlQuery, string connectionString)
        {
            List<User> users = [];
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                User _user = new()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    FirstName = Convert.ToString(reader["first_name"])!,
                    LastName = Convert.ToString(reader["last_name"])!,
                    EmailAddress = Convert.ToString(reader["email_address"])!,
                    PhoneNumber = Convert.ToInt64(reader["phone_number"]),
                };

                users.Add(_user);
            }
            await reader.CloseAsync();

            return users[0];
        }
    }
}
