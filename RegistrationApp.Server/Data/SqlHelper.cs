using RegistrationApp.Server.Dto;
using RegistrationApp.Server.Models;

namespace RegistrationApp.Server.Data
{
    public class SqlHelper(string connectionString)
    {
        private readonly string _connectionString = connectionString;
        readonly SqlDataAccess sqlDataAccess = new();

        public async Task<User> RegisterUser(CreateUserDto createUserDto)
        {
            string sqlProcedure = "InsertNewUser";

            return await sqlDataAccess.RegisterUser(createUserDto, sqlProcedure, _connectionString);
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

        public async Task<User> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            string sqlProcedure = "UpdateUser";

            return await sqlDataAccess.UpdateUser(id, updateUserDto, sqlProcedure, _connectionString);
        }

        public async Task<User> DeleteUser(int id)
        {
            string sqlProcedure = "DeleteUser";

            return await sqlDataAccess.DeleteUser(id, sqlProcedure, _connectionString);
        }
    }
}
