using Microsoft.AspNetCore.Http;

namespace Vecozo.Certificate
{
	public class ClientCertificateAuthorization : IClientCertificateAuthorization
	{
		public ClientCertificateAuthorization(IHttpContextAccessor httpContextAccessor)
		{
			var httpContext = httpContextAccessor.HttpContext;

			//warning when running under azure  you should use Headers ["X-ARR-ClientCert"]
			var certificate = httpContext.Connection.ClientCertificate;
			if (certificate == null)
				return;

			IsAuthenticated = true;
			httpContext.AddNameClaim(certificate.IssuerName.Name);
		}

		public bool IsAuthenticated { get; }
	}
}