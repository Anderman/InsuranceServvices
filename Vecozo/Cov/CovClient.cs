using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.CovClients.Request;
using Vecozo.Connected_Services.CovClients.Response;
using Vecozo.Infrastructure;

// ReSharper disable IdentifierTypo

// ReSharper disable StringLiteralTypo

namespace Vecozo.Cov
{
	public class CovClient<T> : CovClient where T : IVecozoEnvironment
	{
		public CovClient(SoapClient<Config, Request, ControleerResponse> client, T env) : base(client)
		{
			client.Url = $"https://{env.Environment.ToEnvironmentString()}covwebservice.vecozo.nl/v1/VZ801802.svc";
		}
	}


	public class CovClient
	{
		private readonly SoapClient<Config, Request, ControleerResponse> _client;

		public CovClient(SoapClient<Config, Request, ControleerResponse> client)
		{
			_client = client;
		}


		public async Task<Zoekresultaat> Check(int bsn, DateTime dateOfBirth)
		{
			return await Check(bsn, dateOfBirth, DateTime.Today);
		}

		public async Task<Zoekresultaat> Check(int bsn, DateTime dateOfBirth, DateTime refDate)
		{
			var request = new Request { Zoekopdrachten = new[] { new Zoekopdracht { Bsn = bsn.ToString("D9"), Geboortedatum = dateOfBirth, Volgnummer = 1, Peildatum = refDate } } };

			var results = await _client.PostAsync(request);
			if (results.Resultaat != RetourberichtresultaatType.VerzoekAkkoord.ToString())
				throw new ArgumentException($"Vecozo response {results.Resultaat}, Arguments sent bsn:{bsn}, DateOfBirth={dateOfBirth}");

			return results.Zoekresultaten[0];
		}

		public class Config : ISoapConfig
		{
			public string Namespace => "http://schemas.vecozo.nl/VZ801802/v1";
			public string SoapActionElementName => "Controleer";
			public string SoapAction => $"{Namespace}/{SoapActionElementName}";

			public string GetUrl(IHostingEnvironment env)
			{
				return $"https://{env.VecozoEnvironment()}covwebservice.vecozo.nl/v1/VZ801802.svc";
			}
		}
	}
}