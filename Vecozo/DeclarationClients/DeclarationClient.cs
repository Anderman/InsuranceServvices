using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.DeclarationClients;

// ReSharper disable StringLiteralTypo

namespace Vecozo.DeclarationClients
{
	public class DeclarationClient
	{
		private const string Email = "noreply@vecozo.nl";
		private readonly SoapClient<Config, IndienenRequest, IndienenResponse> _client;
		private readonly EmailNotificaties _emailReply = new EmailNotificaties { IndicatieAfkeuringResultaat = false, IndicatieControleResultaat = false };

		public DeclarationClient(SoapClient<Config, IndienenRequest, IndienenResponse> client)
		{
			_client = client;
		}

		public async Task<long?[]> Upload(byte[] data)
		{
			var file = new Bestand { Bestandsnaam = "filename.txt", Data = data, Bestandsgrootte = data.Length };
			var request = new IndienenRequest { Declaratie = new Declaratie { DeclaratieBestand = file, EmailNotificaties = _emailReply, IndienerEmailadres = Email } };

			var results = await _client.PostAsync(request);
			foreach (var result in results.Resultaten)
				result.Resultaatcode.EnsureSuccess();
			return results.Resultaten.Select(x => x.DeclaratieId).ToArray();
		}

		public class Config : ISoapConfig
		{
			public string Namespace => "urn:www-vecozo-nl:vsp:edp:declareren:indienen:v1";
			public string SoapActionElementName => "Indienen";
			public string SoapAction => $"{Namespace}:{SoapActionElementName}";

			public string GetUrl(IHostingEnvironment env)
			{
				return $"https://{env.VecozoEnvironment()}edpwebservice.vecozo.nl/Router.V1.svc/IndienenDeclaratieV1";
			}
		}
	}
}