using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SoapCore.SoapServices;
using Vecozo.Connected_Services.CovClients.Response;

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Vecozo.Connected_Services.CovClients.Request
{
	[ServiceContract(Namespace = "http://schemas.vecozo.nl/VZ801802/v1", ConfigurationName = "COV.vz801802Port")]
	public interface vz801802Port
	{
		[OperationContract(Action = "http://schemas.vecozo.nl/VZ801802/v1/Controleer", ReplyAction = "http://schemas.vecozo.nl/VZ801802/v1/Controleer/reply")]
		Task<ControleerResponse> ControleerAsync(Request request);
	}

	[XmlType(Namespace = "http://schemas.vecozo.nl/VZ801802/v1/messages", TypeName = "request")]
	public class Request
	{
		public Zorgaanbieder Zorgaanbieder { get; set; }

		[XmlArrayItem(Namespace = "http://schemas.vecozo.nl/VZ801802/v1/types", IsNullable = false)]
		public Zoekopdracht[] Zoekopdrachten { get; set; }
	}

	[XmlType(Namespace = "http://schemas.vecozo.nl/VZ801802/v1/types")]
	public class Zorgaanbieder
	{
		[XmlElement("AGB-zorgverlenersoort", Order = 0)]
		public short AGBzorgverlenersoort { get; set; }

		[XmlElement("AGB-nummer", Order = 1)]
		public int AGBnummer { get; set; }
	}

	public class Zoekopdracht
	{
		public short Volgnummer { get; set; }
		public DateTime Geboortedatum { get; set; }
		public DateTime Peildatum { get; set; }
		public string Bsn { get; set; }
		public string Verzekerdenummer { get; set; }
		public string Postcode { get; set; }
		public string ReferentieZorgaanbieder { get; set; }
		public string Huisnummer { get; set; }
		public string Huisnummertoevoeging { get; set; }
	}
}