namespace NetHN.Authentication
{
	using Nancy;
	using Nancy.Authentication.Forms;
	using Nancy.Bootstrapper;
	using Nancy.Diagnostics;
	using Nancy.TinyIoc;

	/// <summary>
	/// Requires installation of the package
	/// Install-Package Nancy.Authentication.Forms
	/// </summary>
	public class FormsAuthBootstrapper : DefaultNancyBootstrapper
	{
		protected override DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new DiagnosticsConfiguration { Password = @"nancy" }; }
		}

		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);
			container.Register<IUserMapper, UserMapper>();
		}

		protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
		{
			var formsAuthConfiguration = new FormsAuthenticationConfiguration()
												{
													RedirectUrl = "/login",
													UserMapper = requestContainer.Resolve<IUserMapper>(),
												};

			FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
		}
	}
}