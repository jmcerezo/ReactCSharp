using ReactCSharp.Server.Dto;
using ReactCSharp.Server.Models;
using System.Data.SqlClient;

namespace ReactCSharp.Server.Data
{
    public class SqlDataAccess
    {
        public List<User> RegisterUser(UserDto userDto, string sqlQuery, string connectionString)
        {
            List<User> users = [];

            using SqlConnection conn = new(connectionString);
            conn.Open();
            SqlCommand cmd = new(sqlQuery, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@first_name", userDto.FirstName);
            cmd.Parameters.AddWithValue("@last_name", userDto.LastName);
            cmd.Parameters.AddWithValue("@email_address", userDto.EmailAddress);
            cmd.Parameters.AddWithValue("@phone_number", userDto.PhoneNumber);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User book = new()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    FirstName = Convert.ToString(reader["first_name"])!,
                    LastName = Convert.ToString(reader["last_name"])!,
                    EmailAddress = Convert.ToString(reader["email_address"])!,
                    PhoneNumber = Convert.ToInt64(reader["phone_number"]),
                };

                users.Add(book);
            }
            reader.Close();

            return users;
        }
    }
}
