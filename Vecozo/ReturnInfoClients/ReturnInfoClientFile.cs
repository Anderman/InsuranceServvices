using System.Threading.Tasks;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.ReturnInfoClients;

// ReSharper disable CommentTypo

namespace Vecozo.ReturnInfoClients
{
	public class ReturnInfoClientFile
	{
		private readonly SoapClient<Config, DownloadRequest, DownloadResponse> _client;

		public ReturnInfoClientFile(SoapClient<Config, DownloadRequest, DownloadResponse> client)
		{
			_client = client;
		}

		public async Task<byte[]> DownloadByFileId(long fileId)
		{
			var request = new DownloadRequest { RetourbestandId = fileId };

			var result = await _client.PostAsync(request);
			result.Resultaatcode.EnsureSuccess();
			return result.EIRetourbestand.Bestand.Data;
		}


		/// <summary>
		///     Het ophalen van een retour bestand van een specifieke declaratie
		///     <exception cref="VecozoException">Als er geen bestand kan worden opgehaalt</exception>
		/// </summary>
		/// <param name="declarationId">Dit is een ID gegeneerd door Vecozo voor iedere declaratie</param>
		/// <returns></returns>
		public async Task<byte[]> DownloadByDeclarationId(long declarationId)
		{
			var request = new DownloadRequest { DeclaratieId = declarationId };

			var result = await _client.PostAsync(request);
			result.Resultaatcode.EnsureSuccess();
			return result.EIRetourbestand.Bestand.Data;
		}

		/// <summary>
		///     Probeert het retour bestand op te halen als deze al/nog beschikbaar is.
		/// </summary>
		/// <param name="declarationId">Dit is een ID gegeneerd door Vecozo voor iedere declaratie</param>
		/// <returns></returns>
		public async Task<byte[]> TryDownloadByDeclarationId(long declarationId)
		{
			var request = new DownloadRequest { DeclaratieId = declarationId };

			var result = await _client.PostAsync(request);
			return result.EIRetourbestand?.Bestand?.Data;
		}

		public class Config : ReturnInfoConfig
		{
			public override string SoapActionElementName => "Download";
		}
	}
}