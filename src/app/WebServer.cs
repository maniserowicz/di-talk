namespace Procent.DependencyInjection.app
{
    public class WebServer
    {
        public void RegisterUser(string email)
        {
            var container = new PoorMansContainer();

            // register

            container.Register<IEmailValidator, EmailValidator>();
            container.Register<IActivationLinkGenerator, ActivationLinkGenerator>();
            container.Register<IEmailService, EmailService>();
            // application compiles, but will throw in runtime
            // container.Register<IEmailTemplateGenerator, ???>();

            container.RegisterType<UsersController>();

            // resolve
            var controller = container.Resolve<UsersController>();

            controller.RegisterUser(email);
        }
    }
}