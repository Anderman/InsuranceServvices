using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Logging.Debug;
using SoapCore.SoapClient;
using Vecozo.Connected_Services.CovClients.Request;
using Vecozo.Connected_Services.CovClients.Response;
using Vecozo.Cov;
using Xunit;

// ReSharper disable IdentifierTypo

namespace Vecozo.Test
{
	public class CovCheckTests
	{
		private static SoapClient<T1, T2, T3> GetVecozoClient<T1, T2, T3>() where T1 : ISoapConfig, new() where T2 : class where T3 : class, new()
		{
			var hostingEnvironment = new HostingEnvironment { EnvironmentName = "Development" };
			var vecozoHttpClient = new SoapClient<T1, T2, T3>(hostingEnvironment, new DebugLogger("test"), new CertificateProviderTest());
			return vecozoHttpClient;
		}

		[Fact]
		public async Task TestClient()
		{
			var soapClient = GetVecozoClient<CovClient.Config, Request, ControleerResponse>();
			var client = new CovClient(soapClient);

			var dateOfBirth = new DateTime(1962, 03, 15);
			var result = await client.Check(999979668, dateOfBirth);

			Assert.Equal(ZoekresultaatType.Gevonden, result.Resultaat);
			Assert.Equal(VerzekerderesultaatType.ActieveVerzekeringen, result.Verzekerde.Resultaat);
			Assert.Equal(dateOfBirth, result.Verzekerde.Geboortedatum);
			Assert.Equal(Geslacht.Vrouw, result.Verzekerde.Geslacht);
			Assert.Equal(2, result.Verzekerde.CodeGeslacht);
			Assert.False(result.Verzekerde.Overleden);
			Assert.Equal("Ommeren", result.Verzekerde.Achternaam1);
			Assert.Equal(999979668, result.Verzekerde.Bsn);
			Assert.Equal("OP", result.Verzekerde.Voorletters);
			Assert.Equal("van", result.Verzekerde.Voorvoegsel1);
			Assert.Equal("Prater", result.Verzekerde.Achternaam2);
			Assert.Equal("de", result.Verzekerde.Voorvoegsel2);
			Assert.Single(result.Verzekerde.Verzekeringen);
			var verzekering1 = result.Verzekerde.Verzekeringen[0];
			Assert.Equal(VerzekeringresultaatType.Actief, verzekering1.Resultaat);
			Assert.Equal(3000, verzekering1.UZOVI);
			Assert.Equal("559", verzekering1.Verzekerdenummer);
			Assert.Equal(SoortVerzekering.Aanvullend, verzekering1.SoortVerzekering);
			Assert.Equal(new DateTime(2017, 1, 1), verzekering1.Ingangsdatum);
		}
	}
}