using System;
using Vecozo.Certificate;
using Vecozo.Connected_Services.Vecozo.IsAliveInterface;

namespace Vecozo.ReturnInfoServices
{
	public class ReturnInfoServicesIsAlive : IAliveService
	{
		public ReturnInfoServicesIsAlive(IClientCertificateAuthorization authorization)
		{
			if (!authorization.IsAuthenticated) throw new Exception("No valid certificate found");
		}

		public IsAliveResponse IsAlive(IsAliveRequest request)
		{
			return new IsAliveResponse { Resultaat = true };
		}
	}
}