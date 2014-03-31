namespace Procent.DependencyInjection.app
{
    public class WebServer
    {
        static PoorMansContainer _container;

        static void Main()
        {
            _container = new PoorMansContainer();

            _container.Register<IEmailValidator, EmailValidator>();
            _container.Register<IActivationLinkGenerator, ActivationLinkGenerator>();
            _container.Register<IEmailService, EmailService>();
            // application compiles, but will throw in runtime
            // container.Register<IEmailTemplateGenerator, ???>();

            _container.RegisterType<UsersController>();
        }

        static void Shutdown()
        {
            // our container does not implement this
            // _container.Dispose();
        }

        public void RegisterUser(string email)
        {
            var controller = _container.Resolve<UsersController>();

            controller.RegisterUser(email);
        }
    }
}