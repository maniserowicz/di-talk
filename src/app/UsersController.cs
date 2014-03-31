using System;
using System.Text.RegularExpressions;

namespace Procent.DependencyInjection.app
{
    public class UsersController
    {
        const string EMAIL_REGEX = @"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}";

        public void RegisterUser(string email)
        {
            // check if email is valid
            if (Regex.IsMatch(email, EMAIL_REGEX) == false)
            {
                throw new ArgumentException("Invalid email address");
            }

            // check if email is not taken
            if (UsersDatabase.IsEmailTaken(email))
            {
                throw new InvalidOperationException("Email already taken");
            }

            // create new user
            var newUser = new User
            {
                Email = email,
                RegistrationToken = Guid.NewGuid().ToString(),
            };

            // insert user
            UsersDatabase.InsertUser(newUser);

            // generate activation link
            string registrationLink = string.Format(
                "http://myapp.com/confirm?email={0}&token={1}"
                , newUser.Email, newUser.RegistrationToken
            );

            EmailService.RegistrationEmail(newUser.Email, registrationLink);
        }
    }
}