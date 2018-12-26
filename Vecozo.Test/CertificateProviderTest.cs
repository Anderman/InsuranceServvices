using System.IO;
using System.Security.Cryptography.X509Certificates;
using SoapCore.SoapClient;

namespace Vecozo.Test
{
	public class CertificateProviderTest : ICertificateProvider
	{
		public byte[] Certificate => File.ReadAllBytes("c:/temp/sc.pfx");
		public X509Certificate2 Certificate2 => new X509Certificate2(Certificate, "Itzos");
	}
}