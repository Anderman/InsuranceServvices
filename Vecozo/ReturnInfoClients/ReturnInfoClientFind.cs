using System.Threading.Tasks;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.ReturnInfoClients;
// ReSharper disable CommentTypo

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

		/// <summary>
		/// Opvragen van nog niet opgevraagde bestanden. Het resultaat geeft ids terug voor het ophalen van retour-pdf bestanden
		/// Om dit te kunnen koppelen aan de declaratie moet je zorgen dat je het declaratieId hebt opgeslagen bij de declaratie.
		/// Dit declaratie krijg je bij het indienen van een declaratie.
		/// Deze interface zou je kunnen gebruiken voor het bepalen welke bestanden er klaar staan.
		/// Je zou bestanden met een declaratieId kunnen opvragen om direct te downloaden. 
		/// </summary>
		/// <param name="eiCode"></param>
		/// <param name="version"></param>
		/// <param name="subversion"></param>
		/// <returns></returns>
		public async Task<Declaratie[]> FindByDeclarationVersion(string eiCode, short version, short? subversion)
		{
			var search = new EIStandaardZoekCriteria { StandaardCode = eiCode, StandaardVersie = version, StandaardSubVersie = subversion };
			var searchArray = new ArrayOfEIStandaardZoekCriteria { search };
			var request = new OpvragenTeVerwerkenRetourInformatieRequest { EIStandaarden = searchArray };

			var result = await _client.PostAsync(request);
			result.Resultaatcode.EnsureSuccess();
			return result.Resultaten;
		}

		/// <summary>
		/// Hiermee kan de bijlage gevonden worden die bij een retour bericht hoort van een declaratie
		/// Als vecozo al / nog de bijlage heeft dan zal het resultaat gevuld zijn.
		/// </summary>
		/// <param name="declarationId"></param>
		/// <returns></returns>
		public async Task<Declaratie[]> FindByDeclarationId(long? declarationId)
		{
			var request = new OpvragenTeVerwerkenRetourInformatieRequest { DeclaratieId = declarationId, NegeerPdfIds = true };

			var result = await _client.PostAsync(request);
			return result.Resultaten;
		}
		public async Task<Declaratie[]> FindInclusivePdfByDeclarationId(long? declarationId)
		{
			var request = new OpvragenTeVerwerkenRetourInformatieRequest { DeclaratieId = declarationId, NegeerPdfIds = false, };

			var result = await _client.PostAsync(request);
			return result.Resultaten;
		}

		public class Config : ReturnInfoConfig
		{
			public override string SoapActionElementName => "OpvragenTeVerwerkenRetourInformatie";
		}
	}
}