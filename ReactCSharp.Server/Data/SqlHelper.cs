using ReactCSharp.Server.Dto;
using ReactCSharp.Server.Models;

namespace ReactCSharp.Server.Data
{
    public class SqlHelper(string connectionString)
    {
        private readonly string _connectionString = connectionString;
        readonly SqlDataAccess sqlDataAccess = new();

        public async Task<List<User>> RegisterUser(UserDto userDto)
        {
            string sqlProcedure = "InsertNewUser";

            // TODO: Fix data in response (empty array)
            var users = await Task.FromResult(sqlDataAccess.RegisterUser(userDto, sqlProcedure, _connectionString));

            return users;
        }
    }
}
