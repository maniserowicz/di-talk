using System;
using NSubstitute;
using Procent.DependencyInjection.app;
using Xunit;

namespace Procent.DependencyInjection.Tests
{
    public class UserController_RegisterUser_Tests
    {
        readonly UsersController _controller;
        readonly IEmailValidator _emailValidator;
        string _email;

        public UserController_RegisterUser_Tests()
        {
            _emailValidator = Substitute.For<IEmailValidator>();
            var linkGenerator = Substitute.For<IActivationLinkGenerator>();

            _controller = new UsersController(_emailValidator, linkGenerator);

            _email = "email";
        }

        void execute()
        {
            _controller.RegisterUser(_email);
        }

        [Fact]
        public void throws_when_email_not_valid()
        {
            _emailValidator.Validate(_email).Returns(false);

            Assert.Throws<ArgumentException>(
                () => execute()
            );
        }
    }
}