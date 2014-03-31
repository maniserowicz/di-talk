namespace Procent.DependencyInjection.app
{
    public interface IActivationLinkGenerator
    {
        string GenerateLink(string token, string email);
    }

    public class ActivationLinkGenerator : IActivationLinkGenerator
    {
        public string GenerateLink(string token, string email)
        {
            return string.Format(
                "http://myapp.com/confirm?email={0}&token={1}"
                , email, token
            );
        }
    }
}