using Procent.DependencyInjection.app;
using Xunit;

namespace Procent.DependencyInjection.Tests
{
    public class ActivationLinkGeneratorTests
    {
        readonly ActivationLinkGenerator _generator;
        string _token = "a-token";
        string _email = "some-email";

        public ActivationLinkGeneratorTests()
        {
            _generator = new ActivationLinkGenerator();
        }

        string execute()
        {
            return _generator.GenerateLink(_token, _email);
        }

        [Fact]
        public void generates_link_using_given_token()
        {
            string result = execute();

            Assert.Contains("token=a-token", result);
        }

        [Fact]
        public void generates_link_using_given_email()
        {
            string result = execute();

            Assert.Contains("email=some-email", result);
        }
    }
}