using System.Text.RegularExpressions;

namespace Procent.DependencyInjection.app
{
    public class EmailValidator
    {
        const string EMAIL_REGEX = @"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}";

        public bool Validate(string email)
        {
            return Regex.IsMatch(email, EMAIL_REGEX);
        }
    }
}