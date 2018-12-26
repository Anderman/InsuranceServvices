using System.Xml.Serialization;
using SoapCore.SoapServices;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Vecozo.Connected_Services.Vecozo.IsAliveInterface
{
	public class IsAliveResponse : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:messages:isalive:v1")]
		public bool Resultaat { get; set; }
	}

	public class IsAliveRequest : object
	{
	}

	[ServiceContract(Namespace = "urn:www-vecozo-nl:isalive:v1")]
	public interface IAliveService
	{
		[OperationContract(Action = "urn:www-vecozo-nl:v1:isalive", ReplyAction = "urn:www-vecozo-nl:v1:isaliveresponse")]
		IsAliveResponse IsAlive(IsAliveRequest request);
	}
}