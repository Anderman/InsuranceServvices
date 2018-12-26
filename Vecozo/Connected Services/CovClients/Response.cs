using System;
using System.Xml.Serialization;

#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo


namespace Vecozo.Connected_Services.CovClients.Response
{
	public class Verzekering
	{
		public VerzekeringresultaatType Resultaat { get; set; }
		public short UZOVI { get; set; }
		public string Verzekerdenummer { get; set; }
		public SoortVerzekering SoortVerzekering { get; set; }
		public string Pakketcode { get; set; }
		public string Pakketnaam { get; set; }
		public string Labelcode { get; set; }
		public string Labelnaam { get; set; }
		public DateTime? Ingangsdatum { get; set; }
		public DateTime? Einddatum { get; set; }
	}

	public enum VerzekeringresultaatType
	{
		Actief,
		Inactief,
		NietGeautoriseerdVoorZorgverzekeraar
	}

	public enum SoortVerzekering
	{
		Aanvullend,
		AanvullendPlusTand,
		AWBZ,
		Basis,
		Hoofdverzekering,
		Tand
	}

	public class Verzekerde
	{
		public VerzekerderesultaatType Resultaat { get; set; }
		public DateTime Geboortedatum { get; set; }
		public Geslacht Geslacht { get; set; }
		public int CodeGeslacht { get; set; }
		public bool Overleden { get; set; }
		public string Achternaam1 { get; set; }
		public int? Bsn { get; set; }
		public bool BsnSpecified { get; set; }
		public string Voorletters { get; set; }
		public string Voorvoegsel1 { get; set; }
		public string Achternaam2 { get; set; }
		public string Voorvoegsel2 { get; set; }
		public Verzekering[] Verzekeringen { get; set; }
	}

	public enum VerzekerderesultaatType
	{
		ActieveVerzekeringen,
		DubbeleActieveVerzekeringenGevonden,
		InactieveVerzekeringen
	}

	public enum Geslacht
	{
		Man,
		Vrouw,
		Onbekend,
		NietGespecificeerd
	}

	public class Zoekresultaat
	{
		public short Volgnummer { get; set; }
		public ZoekresultaatType Resultaat { get; set; }
		public string ReferentieZorgaanbieder { get; set; }
		public Verzekerde Verzekerde { get; set; }
	}

	public enum ZoekresultaatType
	{
		BsnOnbekend,
		BsnVoldoetNietAanElfProef,
		CombinatieBsnGeboortedatumOnbekend,
		GeenGeldigZoekpad,
		Gevonden,
		GevondenPeildatumInToekomst,
		MeerdereZoekpadenOpgegeven,
		MeerderePersonenGevonden,
		NietGevonden,
		Peildatum2JaarOfOuder
	}

	public class ControleerResponse
	{
		public string Resultaat { get; set; }

		[XmlArrayItem(IsNullable = false)]
		public Zoekresultaat[] Zoekresultaten { get; set; }
	}

	public enum RetourberichtresultaatType
	{
		GeenZoekopdrachten,
		MeerDan20Zoekopdrachten,
		VerzoekAkkoord
	}
}