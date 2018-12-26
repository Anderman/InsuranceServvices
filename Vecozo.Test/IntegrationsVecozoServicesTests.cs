using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Logging.Debug;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.DeclarationClients;
using Vecozo.Connected_Services.ReturnInfoClients;
using Vecozo.DeclarationClients;
using Vecozo.ReturnInfoClients;
using Xunit;
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace Vecozo.Test
{
	// Test if client can receive soap request from vecozo and send soap message to vecozo
	public class IntegrationsVecozoServicesTests
	{
		private static SoapClient<T1, T2, T3> GetVecozoClient<T1, T2, T3>() where T1 : ISoapConfig, new() where T2 : class where T3 : class, new()
		{
			var hostingEnvironment = new HostingEnvironment { EnvironmentName = "Development" };
			var vecozoHttpClient = new SoapClient<T1, T2, T3>(hostingEnvironment, new DebugLogger("test"), new CertificateProviderTest());
			return vecozoHttpClient;
		}

		[Fact]
		public async Task Download_an_existing_file()
		{
			var vecozoHttpClient = GetVecozoClient<ReturnInfoClientFile.Config, DownloadRequest, DownloadResponse>();
			var client = new ReturnInfoClientFile(vecozoHttpClient);

			var result = await Assert.ThrowsAsync<VecozoException>(() => client.DownloadByDeclarationId(927718));
			Assert.Equal("VSPEDP323", result.Data["Code"]);
		}

		[Fact]
		public async Task DownloadFile()
		{
			var vecozoHttpClient = GetVecozoClient<ReturnInfoClientFile.Config, DownloadRequest, DownloadResponse>();
			var client = new ReturnInfoClientFile(vecozoHttpClient);

			var result = await Assert.ThrowsAsync<VecozoException>(() => client.DownloadByFileId(0));
			Assert.Equal("VSPEDP324", result.Data["Code"]);
		}

		[Fact]
		public async Task DownloadPdf()
		{
			var vecozoHttpClient = GetVecozoClient<ReturnInfoClientPdfFile.Config, DownloadPdfRequest, DownloadPdfResponse>();
			var client = new ReturnInfoClientPdfFile(vecozoHttpClient);
			var result = await Assert.ThrowsAsync<VecozoException>(() => client.Download(0));
			Assert.Equal("VSPEDP326", result.Data["Code"]);
		}

		[Fact]
		public async Task FindDeclarationInfo()
		{
			var vecozoHttpClient = GetVecozoClient<ReturnInfoClientFind.Config, OpvragenTeVerwerkenRetourInformatieRequest, OpvragenTeVerwerkenRetourInformatieResponse>();
			var client = new ReturnInfoClientFind(vecozoHttpClient);

			var result = await client.FindByDeclarationVersion("WMO303", 2, null);
			Assert.NotNull(result);
			Assert.Empty(result);
		}

		[Fact]
		public async Task UploadPm304File()
		{
			var vecozoHttpClient = GetVecozoClient<DeclarationClient.Config, IndienenRequest, IndienenResponse>();
			var client = new DeclarationClient(vecozoHttpClient);

			var bytes = File.ReadAllBytes($"{TestHelper.TestResources}/PM304.dat");
			var result = await client.Upload(bytes);
			Assert.NotNull(result);
			Assert.Single(result);
		}
	}
}