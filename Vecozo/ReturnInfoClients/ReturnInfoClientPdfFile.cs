using System.Threading.Tasks;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.ReturnInfoClients;

namespace Vecozo.ReturnInfoClients
{
	public class ReturnInfoClientPdfFile
	{
		private readonly SoapClient<Config, DownloadPdfRequest, DownloadPdfResponse> _client;

		public ReturnInfoClientPdfFile(SoapClient<Config, DownloadPdfRequest, DownloadPdfResponse> client)
		{
			_client = client;
		}

		public async Task<byte[]> Download(long pdfId)
		{
			var request = new DownloadPdfRequest { PdfId = pdfId };

			var result = await _client.PostAsync(request);
			result.Resultaatcode.EnsureSuccess();
			return result.PdfRetourbestand.Bestand.Data;
		}

		public class Config : ReturnInfoConfig
		{
			public override string SoapActionElementName => "DownloadPdf";
		}
	}
}