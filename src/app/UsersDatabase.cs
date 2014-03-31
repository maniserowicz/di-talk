using System.Data;

namespace Procent.DependencyInjection.app
{
    public interface IUsersDatabase
    {
        bool IsEmailTaken(string email);
        void InsertUser(User user);
    }

    public class UsersDatabase : IUsersDatabase
    {
        private readonly IDbConnection _dbConnection;

        public UsersDatabase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public bool IsEmailTaken(string email)
        {
            return false;
        }

        public void InsertUser(User user)
        {
            
        }
    }
}