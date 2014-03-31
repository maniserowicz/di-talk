namespace Procent.DependencyInjection.app
{
    public interface IUsersDatabase
    {
        bool IsEmailTaken(string email);
        void InsertUser(User user);
    }

    public class UsersDatabase : IUsersDatabase
    {
        public bool IsEmailTaken(string email)
        {
            return false;
        }

        public void InsertUser(User user)
        {
            
        }
    }
}