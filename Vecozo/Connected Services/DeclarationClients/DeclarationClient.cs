using System.Threading.Tasks;
using System.Xml.Serialization;
using SoapCore.SoapServices;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

namespace Vecozo.Connected_Services.DeclarationClients
{
	public class IndienenRequest : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:messages:vsp:edp:declareren:indienen:v1")]
		public Declaratie Declaratie { get; set; }
	}

	public class Declaratie : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:declareren:indienen:v1")]
		public string IndienerEmailadres { get; set; }

		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:declareren:indienen:v1")]
		public string ReferentieZorgaanbieder { get; set; }

		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:declareren:indienen:v1")]
		public EmailNotificaties EmailNotificaties { get; set; }

		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:declareren:indienen:v1")]
		public Bestand DeclaratieBestand { get; set; }
	}

	public class EmailNotificaties : object
	{
		public bool IndicatieControleResultaat { get; set; }
		public bool IndicatieAfkeuringResultaat { get; set; }
	}

	public class Bestand : object
	{
		public string Bestandsnaam { get; set; }
		public int? Bestandsgrootte { get; set; }
		public byte[] Data { get; set; }
	}

	public class IndienenResponse : object
	{
		public DeclaratieResultaat[] Resultaten { get; set; }
	}

	public class DeclaratieResultaat : object
	{
		public Resultaatcode Resultaatcode { get; set; }
		public long? DeclaratieId { get; set; }
		public string Bestandsnaam { get; set; }
	}

	public enum Resultaatcode
	{
		VSPEDP102 = 0,
		VSPEDP303 = 1,
		VSPEDP304 = 2,
		VSPEDP332 = 3,
		VSPEDP333 = 4,
		VSPEDP350 = 5,
		VSPEDP360 = 6,
		VSPEDP365 = 7
	}

	public class VspEdpFaultContract : object
	{
		public Resultaatcode1 Resultaatcode { get; set; }
		public string Message { get; set; }
	}

	public enum Resultaatcode1
	{
		VSPEDP000 = 0,
		VSPEDP001 = 1,
		VSPEDP999 = 2
	}

	[ServiceContract(Namespace = "urn:www-vecozo-nl:vsp:edp:declareren:indienen:v1", ConfigurationName = "Vvt.Vecozo.Upload.VspEdpIncomingZvlIndienen")]
	public interface VspEdpIncomingZvlIndienen
	{
		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:declareren:indienen:v1:Indienen", ReplyAction = "urn:www-vecozo-nl:vsp:edp:declareren:indienen:v1:IndienenResponse")]
		//	[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:declareren:indienen:v1:IndienenFault", Name = "VspEdpFaultContract")]
		Task<IndienenResponse> IndienenAsync(IndienenRequest IndienenRequest);
	}
}