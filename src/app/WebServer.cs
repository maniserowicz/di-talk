namespace Procent.DependencyInjection.app
{
    public class WebServer
    {
        public void RegisterUser(string email)
        {
            var emailValidator = new EmailValidator();
            var activationLinkGenerator = new ActivationLinkGenerator();
            var emailService = new EmailService(new IEmailTemplateGenerator());
            var controller = new UsersController(emailValidator, activationLinkGenerator, emailService);
            controller.RegisterUser(email);
        }
    }
}