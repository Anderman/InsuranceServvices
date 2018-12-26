using Microsoft.AspNetCore.Hosting;

namespace Vecozo
{
	public static class HostingEnvironmentExtensions
	{
		public static string VecozoEnvironment(this IHostingEnvironment env)
		{
			return env.IsDevelopment() ? "tst" : env.IsStaging() ? "acc" : "";
		}
	}
}