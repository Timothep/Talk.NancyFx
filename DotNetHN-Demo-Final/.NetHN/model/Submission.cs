namespace NetHN.model
{
    using System;
    using System.Globalization;

    public class Submission
    {
        private readonly Random rand = new Random(DateTime.Now.Millisecond);

        public string Id { get; set; }
        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                // Set via autobinding, 
                //  only chance we have 
                //  to implicitely set
                //  the other parameters
                if (value.StartsWith("www."))
                    url = "http://" + value;
                else
                    url = value;
                Source = GetDomainFromUrl();
                Votes = 0;
                Id = GetNewStringId();
            }
        }
        public string Title { get; set; }
        public string Source { get; set; }
        public int Votes { get; set; }
        public string User { get; set; }
        public long Timestamp { get; set; }
        public int Origin { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Fills up the fields that could not be automagically bound
        /// </summary>
        public void SetUserContext(string userLogin = "")
        {
            this.User = !string.IsNullOrEmpty(userLogin) ? userLogin : "an0nymous";
        }

        /// <summary>
        /// Returns a new ID
        /// </summary>
        private string GetNewStringId()
        {
            return rand.Next().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Extracts the domain from a full URL
        /// </summary>
        /// <returns>The domain part of the URL if found, an empty string otherwise</returns>
        private string GetDomainFromUrl()
        {
            Uri givenUri;
            string host;

            if (Uri.TryCreate(this.Url, UriKind.Absolute, out givenUri))
                host = givenUri.Host;
            else
                throw new ArgumentException("Wrong URL format");

            host = host.Remove(0, 4);

            return host;
        }
    }
}