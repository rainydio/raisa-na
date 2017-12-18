using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace MySecondTest {
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class DriversDataAttribute : DataAttribute {
		public override IEnumerable<object[]> GetData(MethodInfo methodInfo) {
			var assembly = this.GetType().Assembly;
			var resource = $"{assembly.GetName().Name}.Resources.Drivers.json";

			using (var stream = assembly.GetManifestResourceStream(resource)) {
				using (var sr = new StreamReader(stream)) {
					using (var jr = new JsonTextReader(sr)) {
						var serializer = new JsonSerializer();
						var driversCapabilities = serializer.Deserialize<List<Dictionary<string, string>>>(jr);

						foreach (var driverCapabilities in driversCapabilities) {
							yield return new object[] { driverCapabilities };
						}
					}
				}
			}
		}
	}
}