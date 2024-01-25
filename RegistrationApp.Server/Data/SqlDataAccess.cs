using RegistrationApp.Server.Dto;
using RegistrationApp.Server.Models;
using System.Data.SqlClient;

namespace RegistrationApp.Server.Data
{
    public class SqlDataAccess
    {
        public async Task<User> RegisterUser(CreateUserDto createUserDto, string sqlQuery, string connectionString)
        {
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();

            SqlCommand cmd = new(sqlQuery, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@first_name", createUserDto.FirstName);
            cmd.Parameters.AddWithValue("@last_name", createUserDto.LastName);
            cmd.Parameters.AddWithValue("@email_address", createUserDto.EmailAddress);
            cmd.Parameters.AddWithValue("@phone_number", createUserDto.PhoneNumber);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<User> users = [];
            await ConvertData(reader, users);

            return users[0];
        }

        public async Task<List<User>> GetAllUsers(string sqlQuery, string connectionString)
        {
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();

            SqlCommand cmd = new(sqlQuery, conn);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<User> users = [];
            await ConvertData(reader, users);

            return users;
        }

        public async Task<User> GetUserById(int id, string sqlQuery, string connectionString)
        {
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();

            SqlCommand cmd = new(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<User> users = [];
            await ConvertData(reader, users);

            return users[0];
        }

        public async Task<User> UpdateUser(int id, UpdateUserDto updateUserDto, string sqlQuery, string connectionString)
        {
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();

            SqlCommand cmd = new(sqlQuery, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@first_name", updateUserDto.FirstName);
            cmd.Parameters.AddWithValue("@last_name", updateUserDto.LastName);
            cmd.Parameters.AddWithValue("@email_address", updateUserDto.EmailAddress);
            cmd.Parameters.AddWithValue("@phone_number", updateUserDto.PhoneNumber);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<User> users = [];
            await ConvertData(reader, users);

            return users[0];
        }

        public async Task<User> DeleteUser(int id, string sqlQuery, string connectionString)
        {
            using SqlConnection conn = new(connectionString);
            await conn.OpenAsync();

            SqlCommand cmd = new(sqlQuery, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<User> users = [];
            await ConvertData(reader, users);

            return users[0];
        }

        private static async Task ConvertData(SqlDataReader reader, List<User> users)
        {
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
        }
    }
}
