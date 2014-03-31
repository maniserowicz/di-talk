namespace Procent.DependencyInjection.app
{
    public class WebServer
    {
        public void RegisterUser(string email)
        {
            var emailValidator = new EmailValidator();
            var activationLinkGenerator = new ActivationLinkGenerator();
            var controller = new UsersController(emailValidator, activationLinkGenerator);
            controller.RegisterUser(email);
        }
    }
}