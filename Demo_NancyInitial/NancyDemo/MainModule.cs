using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using Simple.Data;
using Simple.Data.MongoDB;

namespace NancyDemo
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            // Root of the application, gather all submissions
            Get["/"] = _ =>
                {
                    return "Here's the Home";
                };

            // Redirection to the "submit" page
            Get["/submit"] = _ =>
                {
                    return "Here's where we submit stuff";
                };

            // Post action upon submit
            Post["/submit"] = _ =>
                {
                    return "Here's a submission POST";
                };

            // Increase the score of the given submission
            Get["/voteup/{id}"] = parameters =>
                {
                    return "Here's we would vote up the submission " + parameters.id;
                };
        }
    }
}