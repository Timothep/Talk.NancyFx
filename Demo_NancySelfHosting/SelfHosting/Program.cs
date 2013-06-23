using System;
using Nancy;
using Nancy.Hosting.Self;

namespace SelfHostingNancy
{
	class Program
	{
		private const string serverUri = "http://localhost:12345";

		static void Main(string[] args)
		{
			// initialize an instance of NancyHost (found in the Nancy.Hosting.Self package)
			var host = new NancyHost(new Uri(serverUri));
			host.Start(); // start hosting
			Console.ReadLine();
			Console.WriteLine("You are about to close the application.");
			host.Stop();  // stop hosting
		}
	}

	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ => "Hello World";
		}
	}
}
