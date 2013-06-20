namespace Demo
{
    using Nancy;

    public class MainModule : NancyModule
    {
        public MainModule()
        {
            // Root of the application, gather all submissions
            Get["/"] = _ =>
            {
                return "Nancy HackerNews Main Page";
            };

            // Redirection to the "submit" page
            Get["/submit"] = _ =>
            {
                return "Submit a new link";
            };

            // Post action upon submit
            Post["/submit"] = _ =>
            {
                return "Process the new submission";
            };

            // Increase the score of the given submission
            Get["/voteup/{id}"] = parameters =>
            {
                return "Vote up submission " + parameters.id;
            };
        }
    }
}