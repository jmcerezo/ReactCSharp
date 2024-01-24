using RegistrationApp.Server.Dto;
using RegistrationApp.Server.Models;

namespace RegistrationApp.Server.Data
{
    public class SqlHelper(string connectionString)
    {
        private readonly string _connectionString = connectionString;
        readonly SqlDataAccess sqlDataAccess = new();

        public async Task<List<User>> RegisterUser(UserDto userDto)
        {
            string sqlProcedure = "InsertNewUser";

            return await sqlDataAccess.RegisterUser(userDto, sqlProcedure, _connectionString);
        }

        public async Task<List<User>> GetAllUsers()
        {
            string sqlString = "SELECT * FROM Users ORDER BY id";

            return await sqlDataAccess.GetAllUsers(sqlString, _connectionString);
        }

        public async Task<User> GetUserById(int id)
        {
            string sqlString = "SELECT * FROM Users WHERE id = @id";

            return await sqlDataAccess.GetUserById(id, sqlString, _connectionString);
        }
    }
}
