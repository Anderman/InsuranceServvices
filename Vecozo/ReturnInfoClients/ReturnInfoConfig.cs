using Microsoft.AspNetCore.Hosting;
using SoapCore.SoapClient;

// ReSharper disable StringLiteralTypo

namespace Vecozo.ReturnInfoClients
{
	public class ReturnInfoConfig : ISoapConfig
	{
		public string Namespace => "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1";
		public virtual string SoapActionElementName => "Download";
		public string SoapAction => $"{Namespace}:{SoapActionElementName}";

		public string GetUrl(IHostingEnvironment env)
		{
			return $"https://{env.VecozoEnvironment()}edpwebservice.vecozo.nl/Router.V1.svc/DownloadenRetourinformatieV1";
		}
	}
}