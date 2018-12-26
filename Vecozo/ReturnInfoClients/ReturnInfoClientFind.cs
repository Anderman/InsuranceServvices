using System.Threading.Tasks;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.ReturnInfoClients;

// ReSharper disable StringLiteralTypo

namespace Vecozo.ReturnInfoClients
{
	public class ReturnInfoClientFind
	{
		private readonly SoapClient<Config, OpvragenTeVerwerkenRetourInformatieRequest, OpvragenTeVerwerkenRetourInformatieResponse> _client;

		public ReturnInfoClientFind(SoapClient<Config, OpvragenTeVerwerkenRetourInformatieRequest, OpvragenTeVerwerkenRetourInformatieResponse> client)
		{
			_client = client;
		}

		public async Task<Declaratie[]> FindByDeclarationVersion(string eiCode, short version, short? subversion)
		{
			var search = new EIStandaardZoekCriteria { StandaardCode = eiCode, StandaardVersie = version, StandaardSubVersie = subversion };
			var searchArray = new ArrayOfEIStandaardZoekCriteria { search };
			var request = new OpvragenTeVerwerkenRetourInformatieRequest { EIStandaarden = searchArray };

			var result = await _client.PostAsync(request);
			result.Resultaatcode.EnsureSuccess();
			return result.Resultaten;
		}

		public async Task<Declaratie[]> FindByDeclarationId(long declarationId)
		{
			var request = new OpvragenTeVerwerkenRetourInformatieRequest { DeclaratieId = declarationId };

			var result = await _client.PostAsync(request);
			result.Resultaatcode.EnsureSuccess();
			return result.Resultaten;
		}

		public class Config : ReturnInfoConfig
		{
			public override string SoapActionElementName => "OpvragenTeVerwerkenRetourInformatie";
		}
	}
}