using System;
using NUnit.Framework;
using Nancy.Testing;
using NetHN.Authentication;

namespace NancyTests
{
    [TestFixture]
    public class NancyTests
    {
        [Test]
        public void Should_redirect_to_home_with_credentials_correct()
        {
            // Given
            var bootstrapper = new FormsAuthBootstrapper();
            var browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/login/", (with) =>
                {
                    with.HttpRequest();
                    with.FormValue("username", "admin");
                    with.FormValue("password", "password");
                });

            // Then
            response.Body[".navbar"]
                .ShouldExistOnce()
                .And.ShouldContain("Logged in as admin", StringComparison.InvariantCultureIgnoreCase);
        }

        [Test]
        public void Should_redirect_to_login_with_error_details_incorrect()
        {
            // Given
            var bootstrapper = new FormsAuthBootstrapper();
            var browser = new Browser(bootstrapper);

            // When
            var response = browser.Post("/login/", (with) =>
                {
                    with.HttpRequest();
                    with.FormValue("username", "admin");
                    with.FormValue("password", "wrongpassword");
                });

            // Then
            response.ShouldHaveRedirectedTo("/login?error=true&username=admin");
        }
    }
}