using NetHN.model;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Simple.Data;
using Simple.Data.MongoDB;

namespace NetHN
{
    public class SecureModule : NancyModule
    {
        private const string ConnectionString = @"mongodb://localhost:27017/dotnethn";
        private readonly dynamic db = Database.Opener.OpenMongo(ConnectionString);

        public SecureModule()
            : base("/secure")
        {
            this.RequiresAuthentication();

            // Redirection to the "submit" page
            Get["/submit"] = _ => View["submit"];

            // Post action upon submit
            Post["/submit"] =
                _ =>
                    {
                        //return "Post['/submit']";
                        var newSubmission = this.Bind<Submission>();
                        newSubmission.SetUserContext(Context.CurrentUser.UserName);
                        db.Submissions.Insert(newSubmission);
                        return Response.AsRedirect("/");
                    };

            // Increase the score of the given submission
            Get["/voteup/{id}"] =
                parameters =>
                    {
                        //return "Get['/voteup/{id}']";
                        var existingSubmission = db.Submissions.FindById(parameters.id.ToString());
                        existingSubmission.Votes++;
                        db.Submissions.Update(existingSubmission);
                        return Response.AsRedirect("/");
                    };
        }
    }
}