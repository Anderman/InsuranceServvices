using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SoapCore.SoapServices;

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Vecozo.Connected_Services.ReturnInfoServices
{
	public class EIRetourbestandenRequest : object
	{
		//[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:zvl:v1")]
		public EIRetourbestand EIRetourbestand { get; set; }
	}

	public class EIRetourbestand : object
	{
		public long RetourbestandId { get; set; }
		public long DeclaratieId { get; set; }
		public Bestand Bestand { get; set; }
		public DeclaratieStatus DeclaratieStatus { get; set; }
		public string EmailAdresZorgverzekeraar { get; set; }
		public string ReferentieZorgverzekeraar { get; set; }
		public EIStandaard EIStandaard { get; set; }
		public DateTime GoedgekeurdOp { get; set; }
		public DateTime TeruggestuurdOp { get; set; }
	}

	public class Bestand : object
	{
		public string Bestandsnaam { get; set; }
		public long Bestandsgrootte { get; set; }
		public byte[] Data { get; set; }
	}

	public class EIStandaard : object
	{
		public int StandaardCEI { get; set; }
		public string StandaardCode { get; set; }
		public int StandaardVersie { get; set; }
		public int StandaardSubVersie { get; set; }
	}

	public enum DeclaratieStatus
	{
		OntvangenDoorVecozo = 0,
		CorrectBevondenDoorVecozo = 1,
		AfgekeurdDoorVecozo = 2,
		SuccesvolOntvangenDoorZorgverzekeraar = 3,
		AfgehandeldDoorZorgverzekeraar = 4
	}

	public class EIRetourbestandenResponse : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:messages:vsp:edp:zvl:v1")]
		public Resultaat Resultaat { get; set; }
	}

	public class Resultaat : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:zvl:v1")]
		public Resultaatcode Resultaatcode { get; set; }
	}

	public enum Resultaatcode
	{
		VSPEDP106 = 0,
		VSPEDP108 = 1,
		VSPEDP109 = 2,
		VSPEDP314 = 3,
		VSPEDP317 = 4,
		VSPEDP318 = 5
	}

	public class OntvangenPdfRequest : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:zvl:v1")]
		public PdfRetourbestand PdfRetourbestand { get; set; }
	}

	public class PdfRetourbestand : object
	{
		public long DeclaratieId { get; set; }

		public Bestand Bestand { get; set; }
	}

	public class OntvangenPdfResponse : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:zvl:v1")]
		public Resultaatcode ResultaatCode { get; set; }
	}

	public class ResultaatMeldingRequest : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:zvl:v1")]
		public Statuswijzigingsmelding Statuswijzigingsmelding { get; set; }
	}

	public class Statuswijzigingsmelding : object
	{
		public DateTime GewijzigdOp { get; set; }
		public Melding Melding { get; set; }
	}

	public class Melding : object
	{
	}

	public class DeclaratieStatuswijzigingsmelding : Statuswijzigingsmelding
	{
		public long DeclaratieId { get; set; }
		public DeclaratieStatus Status { get; set; }
		public bool? IndicatieRetourbestandVolgt { get; set; }
	}

	public class RetourinformatieStatuswijzigingsmelding : Statuswijzigingsmelding
	{
		public long RetourbestandId { get; set; }
		public long DeclaratieId { get; set; }
		public RetourinformatieStatus Status { get; set; }
		public Melding Melding1 { get; set; }
	}

	public class VecozoMelding : Melding
	{
		public string Code { get; set; }
		public string Omschrijving { get; set; }
	}

	public class VektisMelding : Melding
	{
		public string Code { get; set; }
		public string Omschrijving { get; set; }
		public short KenmerkRecord { get; set; }
		public long? IdentificatieDetailRecord { get; set; }
	}

	public enum RetourinformatieStatus
	{
		Onbekend = 0,
		TeruggestuurdDoorZorgverzekeraar = 1,
		CorrectBevondenDoorVecozo = 2,
		AfgekeurdDoorVecozo = 3,
		GedownloadDoorZorgaanbieder = 4,
		Verwijderd = 5
	}

	public class ResultaatMeldingResponse : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:types:vsp:edp:zvl:v1")]
		public Resultaatcode Resultaatcode { get; set; }
	}

	[ServiceContract(Namespace = "urn:www-vecozo-nl:vsp:edp:v1", ConfigurationName = "Vvt.Vecozo.WebServices.VspEdpOutgoingZvl")]
	public interface IReceiver
	{
		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:v1:EIRetourbestanden", ReplyAction = "urn:www-vecozo-nl:vsp:edp:v1:EIRetourbestandenResponse")]
		//[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:v1:EIRetourbestandenFault", Name = "VspEdpFaultContract")]
		EIRetourbestandenResponse EIRetourbestanden(EIRetourbestandenRequest EIRetourbestandenRequest);

		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:v1:OntvangenPdf", ReplyAction = "urn:www-vecozo-nl:vsp:edp:v1:OntvangenPdfResponse")]
		//[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:v1:OntvangenPdfFault", Name = "VspEdpFaultContract")]
		Task<OntvangenPdfResponse> OntvangenPdf(OntvangenPdfRequest OntvangenPdfRequest);

		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:v1:ResultaatMelding", ReplyAction = "urn:www-vecozo-nl:vsp:edp:v1:ResultaatMeldingResponse")]
		//[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:v1:ResultaatMeldingFault", Name = "VspEdpFaultContract")]
		Task<ResultaatMeldingResponse> ResultaatMelding(ResultaatMeldingRequest ResultaatMeldingRequest);
	}
}