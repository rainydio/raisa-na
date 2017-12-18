using System;
using System.Net;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Xunit;

namespace MySecondTest {
	public class JoinAboutMeTest {
		private static readonly DateTime _start = DateTime.UtcNow;
		private static long _emailCounter = 0;

		[Theory]
		[DriversData()]
		public void TestNormalForm(Dictionary<string, string> dc) {
			using (var d = BrowserStackFactory.CreateDriver(dc)) {
				_emailCounter++;
				var dt = _start.ToString("yyyyMMddHHmmss");
				var email = $"raisa.gusak+{dt}_{_emailCounter}@mailinator.com";

				d.Navigate().GoToUrl("http://hello.neyber.co.uk/join/about-me");
				d.FindElement(By.Name("title")).SendKeys("Prof");
				d.FindElement(By.Name("first_name")).SendKeys("Simon");
				d.FindElement(By.Name("last_name")).SendKeys("Smith");
				d.FindElement(By.Name("day")).SendKeys("1");
				d.FindElement(By.Name("month")).SendKeys("June");
				d.FindElement(By.Name("year")).SendKeys("1986");
				d.FindElement(By.Name("email")).SendKeys(email);
				d.FindElement(By.Name("verify_email")).SendKeys(email);
				d.FindElement(By.XPath("//input[@type='password']")).SendKeys("Password1");
				d.FindElement(By.Name("terms_accepted")).Click();
				d.FindElement(By.Name("opt_out_email")).Click();
			}
		}

		[Theory]
		[DriversData()]
		public void TestInvalidForm(Dictionary<string, string> dc) {
			using (var d = BrowserStackFactory.CreateDriver(dc)) {
				d.Navigate().GoToUrl("http://hello.neyber.co.uk/join/about-me");
				Assert.Throws<NoSuchElementException>(() => {
					d.FindElement(By.Name("titles"));
				});
			}
		}
	}
}
