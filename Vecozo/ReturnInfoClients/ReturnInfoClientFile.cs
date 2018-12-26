using System.Threading.Tasks;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.ReturnInfoClients;

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

		public async Task<byte[]> DownloadByDeclarationId(long declarationId)
		{
			var request = new DownloadRequest { DeclaratieId = declarationId };

			var result = await _client.PostAsync(request);
			result.Resultaatcode.EnsureSuccess();
			return result.EIRetourbestand.Bestand.Data;
		}

		public class Config : ReturnInfoConfig
		{
			public override string SoapActionElementName => "Download";
		}
	}
}