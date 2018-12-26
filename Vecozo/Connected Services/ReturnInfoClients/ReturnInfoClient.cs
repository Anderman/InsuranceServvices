using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SoapCore.SoapServices;

// ReSharper disable CommentTypo

// ReSharper disable UnusedMember.Global
// ReSharper disable OperationContractWithoutServiceContract
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Vecozo.Connected_Services.ReturnInfoClients
{
	public class OpvragenTeVerwerkenRetourInformatieRequest : object
	{
		public long? DeclaratieId { get; set; }

		public ArrayOfEIStandaardZoekCriteria EIStandaarden { get; set; }

		public long? LaatstOntvangenRetourbestandId { get; set; }

		public bool? NegeerPdfIds { get; set; }
	}

	public class ArrayOfEIStandaardZoekCriteria : List<EIStandaardZoekCriteria>
	{
	}

	public class EIStandaardZoekCriteria : object
	{
		public string StandaardCode { get; set; }

		public short StandaardVersie { get; set; }

		public short? StandaardSubVersie { get; set; }
	}

	public class OpvragenTeVerwerkenRetourInformatieResponse : object
	{
		public Declaratie[] Resultaten { get; set; }

		public Resultaatcode Resultaatcode { get; set; }

		public long AantalResultaten { get; set; }
	}

	public class Declaratie : object
	{
		public long DeclaratieId { get; set; }

		public DeclaratieStatus DeclaratieStatus { get; set; }

		public ArrayOfEIRetourbestanden Retourbestanden { get; set; }

		public ArrayOfPdfRetourbestanden PdfBestanden { get; set; }
	}

	public enum Resultaatcode
	{
		VSPEDP107 = 0,
		VSPEDP113 = 1,
		VSPEDP114 = 2,
		VSPEDP115 = 3,
		VSPEDP201 = 4,
		VSPEDP315 = 5,
		VSPEDP316 = 6,
		VSPEDP323 = 7,
		VSPEDP324 = 8,
		VSPEDP326 = 9,
		VSPEDP341 = 10,
		VSPEDP344 = 11,
		VSPEDP346 = 12,
		VSPEDP356 = 13,
		VSPEDP361 = 14
	}

	public enum DeclaratieStatus
	{
		OntvangenDoorVecozo = 0,
		CorrectBevondenDoorVecozo = 1,
		AfgekeurdDoorVecozo = 2,
		KlaarVoorVerzendingNaarZorgverzekeraar = 3,
		SuccesvolOntvangenDoorZorgverzekeraar = 4,
		AfgehandeldDoorZorgverzekeraar = 5
	}

	public class ArrayOfEIRetourbestanden : List<EIRetourbestandDetail>
	{
	}

	public class ArrayOfPdfRetourbestanden : List<PdfRetourbestandDetail>
	{
	}

	public class EIRetourbestandDetail : object
	{
		public long RetourbestandId { get; set; }
		public EIStandaard EIStandaard { get; set; }
		public DateTime GoedgekeurdOp { get; set; }
		public DateTime IngediendOp { get; set; }
	}

	public class EIStandaard : object
	{
		public short StandaardCEI { get; set; }
		public string StandaardCode { get; set; }
		public short StandaardVersie { get; set; }
		public short StandaardSubVersie { get; set; }
	}

	public class PdfRetourbestandDetail : object
	{
		public long PdfId { get; set; }
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

	public class RaadplegenDeclaratiestatusRequest : object
	{
		public ArrayOfDeclaratieId Declaraties { get; set; }
	}

	public class ArrayOfDeclaratieId : List<long>
	{
	}

	public class RaadplegenDeclaratiestatusResponse : object
	{
		public DeclaratiestatusResultaat[] ArrayOfDeclaratiestatusResultaat { get; set; }
	}

	public class DeclaratiestatusResultaat : object
	{
		public long DeclaratieId { get; set; }
		public Resultaatcode Resultaatcode { get; set; }
		public DeclaratieStatuswijzigingsmelding Declaratie { get; set; }
	}

	public class DeclaratieStatuswijzigingsmelding : Statuswijzigingsmelding
	{
		public DeclaratieStatus Status { get; set; }
	}

	public class Statuswijzigingsmelding : object
	{
		public Melding Melding { get; set; }
	}

	public class Melding : object
	{
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

	public class DownloadRequest : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:messages:vsp:edp:declareren:downloaden:v1")]
		public long? DeclaratieId { get; set; }

		[XmlElement(Namespace = "urn:www-vecozo-nl:messages:vsp:edp:declareren:downloaden:v1")]
		public long? RetourbestandId { get; set; }
	}

	public class DownloadResponse : object
	{
		public EIRetourbestand EIRetourbestand { get; set; }
		public Resultaatcode Resultaatcode { get; set; }
	}

	public class EIRetourbestand : object
	{
		public Bestand Bestand { get; set; }
		public long DeclaratieId { get; set; }
		public DeclaratieStatus DeclaratieStatus { get; set; }
		public long RetourbestandId { get; set; }
		public RetourbestandStatus RetourbestandStatus { get; set; }
		public EIStandaard EIStandaard { get; set; }
		public DateTime GoedgekeurdOp { get; set; }
		public DateTime IngediendOp { get; set; }
		public string EmailAdresZorgverzekeraar { get; set; }
		public string ReferentieZorgverzekeraar { get; set; }
	}

	public class Bestand : object
	{
		public string Bestandsnaam { get; set; }
		public long Bestandsgrootte { get; set; }
		public byte[] Data { get; set; }
	}

	public enum RetourbestandStatus
	{
		Onbekend = 0,
		TeruggestuurdDoorZorgverzekeraar = 1,
		CorrectBevondenDoorVecozo = 2,
		AfgekeurdDoorVecozo = 3,
		GedownloadDoorZorgaanbieder = 4,
		Verwijderd = 5
	}

	public class DownloadPdfRequest : object
	{
		[XmlElement(Namespace = "urn:www-vecozo-nl:messages:vsp:edp:declareren:downloaden:v1")]
		public long PdfId { get; set; }
	}

	public class DownloadPdfResponse : object
	{
		public PdfRetourbestand PdfRetourbestand { get; set; }
		public Resultaatcode Resultaatcode { get; set; }
	}

	public class PdfRetourbestand : object
	{
		public long DeclaratieId { get; set; }
		public Bestand Bestand { get; set; }
	}

	public interface VspEdpIncomingZvlDownloaden
	{
		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:OpvragenTeVerwerkenRetourInformatie", ReplyAction = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:OpvragenTeVerwerkenRetourInformatieResponse")]
		//	[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:IndienenFault", Name = "VspEdpFaultContract")]
		Task<OpvragenTeVerwerkenRetourInformatieResponse> OpvragenTeVerwerkenRetourInformatieAsync(OpvragenTeVerwerkenRetourInformatieRequest OpvragenTeVerwerkenRetourInformatieRequest);

		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:RaadplegenDeclaratiestatus", ReplyAction = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:RaadplegenDeclaratiestatusResponse")]
		//	[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:RaadplegenDeclaratiestatusFault", Name = "VspEdpFaultContract")]
		Task<RaadplegenDeclaratiestatusResponse> RaadplegenDeclaratiestatusAsync(RaadplegenDeclaratiestatusRequest RaadplegenDeclaratiestatusRequest);

		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:Download", ReplyAction = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:DownloadResponse")]
		//	[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:IndienenFault", Name = "VspEdpFaultContract")]
		Task<DownloadResponse> DownloadAsync(DownloadRequest DownloadRequest);

		[OperationContract(Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:DownloadPdf", ReplyAction = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:DownloadPdfResponse")]
		//	[FaultContract(typeof(VspEdpFaultContract), Action = "urn:www-vecozo-nl:vsp:edp:declareren:downloaden:v1:IndienenFault", Name = "VspEdpFaultContract")]
		Task<DownloadPdfResponse> DownloadPdfAsync(DownloadPdfRequest DownloadPdfRequest);
	}
}