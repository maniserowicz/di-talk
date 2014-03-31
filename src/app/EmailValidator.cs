using System.Text.RegularExpressions;

namespace Procent.DependencyInjection.app
{
    public class EmailValidator
    {
        const string EMAIL_REGEX = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}";

        public bool Validate(string email)
        {
            return Regex.IsMatch(email, EMAIL_REGEX);
        }
    }
}