using Procent.DependencyInjection.app;
using Xunit;

namespace Procent.DependencyInjection.Tests
{
    public class EmailValidatorTests
    {
        readonly EmailValidator _validator;
        string _email;

        public EmailValidatorTests()
        {
            _validator = new EmailValidator();
        }

        bool execute()
        {
            return _validator.Validate(_email);
        }

        [Fact]
        public void validates_gmail_email_address_with_dot()
        {
            _email = "my.email@gmail.com";

            bool result = execute();

            Assert.True(result);
        }
    }
}