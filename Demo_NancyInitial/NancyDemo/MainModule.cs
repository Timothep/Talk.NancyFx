using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using Simple.Data;
using Simple.Data.MongoDB;

namespace NancyDemo
{
    public class MainModule : NancyModule
    {
        private const string connectionString = @"mongodb://localhost:27017/dotnethn";
        private dynamic db = Database.Opener.OpenMongo(connectionString);

        public MainModule()
        {
            // Root of the application, gather all submissions
            Get["/"] = _ =>
                {
                    var submissions = db.Submissions.All().OrderByVotesDescending().ToList(); 
                    return View["views/list", submissions]; 
                };

            // Redirection to the "submit" page
            Get["/submit"] = _ =>
                {
                    return View["views/submit"];
                };

            // Post action upon submit
            Post["/submit"] = _ =>
                {
                    var submission = this.Bind<Submission>();
                    db.Submissions.Insert(submission);
                    return Response.AsRedirect("/");
                };

            // Increase the score of the given submission
            Get["/voteup/{id}"] = parameters =>
                {
                    var submission = db.Submissions.FindById(parameters.id.ToString());
                    submission.Votes++;
                    db.Submissions.Update(submission);
                    return Response.AsRedirect("/");
                };
        }
    }
}