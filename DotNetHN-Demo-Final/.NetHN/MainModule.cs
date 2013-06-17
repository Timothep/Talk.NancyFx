namespace NetHN
{
	using System.Collections.Generic;
	using System.Dynamic;
	using System.Linq;
	using Authentication;
	using model;
	using Nancy;
	using Nancy.Authentication.Forms;
	using Nancy.Extensions;
	using Simple.Data;
	using Simple.Data.MongoDB;

	public class MainModule : NancyModule
	{
		private const string ConnectionString = @"mongodb://localhost:27017/dotnethn";
		private readonly dynamic db = Database.Opener.OpenMongo(ConnectionString);

		public MainModule()
		{
			// Root of the application, gather all submissions
			Get["/"] =
				_ =>
				{
					//return "Get['/']";
					dynamic model = new ExpandoObject();
					var submissions = db.Submissions.All().OrderByTimestampDescending();
					model.items = submissions.ToList();
					model.username = Context.CurrentUser != null ? Context.CurrentUser.UserName : null;
					return View["list", model];
				};

			// Redirection to the "login" page
			Get["/login"] = _ => View["login"];

			// Base logout and redirect
			Get["/logout"] = _ => this.LogoutAndRedirect("~/");

			// Validate the credentials and redirect
			Post["/login"] =
				_ =>
				{
					//return "Post['/login']";
					var userGuid = UserMapper.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);
					if (userGuid == null)
						return Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
					return this.LoginAndRedirect(userGuid.Value);
				};
		}
	}
}