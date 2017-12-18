using System;
using System.Collections.Generic;
using System.Net;
using OpenQA.Selenium.Remote;

namespace MySecondTest {
	public class BrowserStackFactory {
		private static readonly string _user;
		private static readonly string _key;
		private static readonly Uri _endpoint;

		class NoProxy : IWebProxy {
			public ICredentials Credentials { get ; set; }
			public Uri GetProxy(Uri destination) => null;
			public bool IsBypassed(Uri host) => true;
		}

		static BrowserStackFactory() {
			WebRequest.DefaultWebProxy = new NoProxy(); // Workaround for Linux
			
			_user = Environment.GetEnvironmentVariable("BROWSERSTACK_USER");
			_key = Environment.GetEnvironmentVariable("BROWSERSTACK_KEY");
			_endpoint = new Uri("http://hub-cloud.browserstack.com/wd/hub/");

			if (_user == null || _key == null) {
				var msg = "Set BROWSERSTACK_USER and BROWSERSTACK_KEY environemnt variables";
				throw new ApplicationException(msg);
			}
		}

		public static RemoteWebDriver CreateDriver(Dictionary<string, string> dc) {
			var capabilities = new DesiredCapabilities();
			capabilities.SetCapability("browserstack.user", _user);
			capabilities.SetCapability("browserstack.key", _key);
			foreach (var kv in dc) {
				capabilities.SetCapability(kv.Key, kv.Value);
			}
			return new RemoteWebDriver(_endpoint, capabilities);
		}
	}
}