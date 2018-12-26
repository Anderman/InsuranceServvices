namespace Vecozo.Certificate
{
	public interface IClientCertificateAuthorization
	{
		bool IsAuthenticated { get; }
	}
}