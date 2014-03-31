using System;

namespace Procent.DependencyInjection.app
{
    public class UsersController
    {
        private readonly IEmailValidator _emailValidator;
        private readonly IActivationLinkGenerator _activationLinkGenerator;
        private readonly IEmailService _emailService;

        public UsersController(IEmailValidator emailValidator, IActivationLinkGenerator activationLinkGenerator, IEmailService emailService)
        {
            _emailValidator = emailValidator;
            _activationLinkGenerator = activationLinkGenerator;
            _emailService = emailService;
        }

        public void RegisterUser(string email)
        {
            // check if email is valid
            if (_emailValidator.Validate(email) == false)
            {
                throw new ArgumentException("Invalid email address");
            }

            // check if email is not taken
            if (UsersDatabase.IsEmailTaken(email))
            {
                throw new InvalidOperationException("Email already taken");
            }

            // create new user
            var newUser = new User
            {
                Email = email,
                RegistrationToken = Guid.NewGuid().ToString(),
            };

            // insert user
            UsersDatabase.InsertUser(newUser);

            // generate activation link
            string registrationLink = _activationLinkGenerator.GenerateLink(newUser.RegistrationToken, newUser.Email);

            _emailService.RegistrationEmail(newUser.Email, registrationLink);
        }
    }
}