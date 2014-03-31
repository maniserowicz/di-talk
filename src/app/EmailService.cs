namespace Procent.DependencyInjection.app
{
    public interface IEmailService
    {
        void RegistrationEmail(string email, string link);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailTemplateGenerator _templateGenerator;

        public EmailService(IEmailTemplateGenerator templateGenerator)
        {
            _templateGenerator = templateGenerator;
        }

        public void RegistrationEmail(string email, string link)
        {
            string template = _templateGenerator.ActivationTemplate(link);
            
            // send email...
        }
    }

    public interface IEmailTemplateGenerator
    {
        string ActivationTemplate(string link);
    }
}