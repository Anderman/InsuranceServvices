using Microsoft.AspNetCore.Hosting;

namespace Vecozo.Infrastructure
{
	public static class HostingEnvironmentExtensions
	{
		public static string VecozoEnvironment(this IHostingEnvironment env)
		{
			return env.IsDevelopment() ? "tst" : env.IsStaging() ? "acc" : "";
		}

		public static string ToEnvironmentString(this VecozoEnvironment environment)
		{
			return environment == Infrastructure.VecozoEnvironment.Test
				? "tst"
				: environment == Infrastructure.VecozoEnvironment.Acceptation
					? "acc"
					: "";
		}
	}
}