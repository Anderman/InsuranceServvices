using System;
using System.Threading.Tasks;
using Vecozo.Certificate;
using Vecozo.Connected_Services.ReturnInfoServices;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace Vecozo.ReturnInfoServices
{
	public class ReturnInfoService : IReceiver
	{
		private static readonly EIRetourbestandenResponse _successResponse = new EIRetourbestandenResponse { Resultaat = new Resultaat { Resultaatcode = Resultaatcode.VSPEDP108 } };
		private readonly IVecozoReturnInfoReceiver _returnInfoReceiver;

		public ReturnInfoService(IClientCertificateAuthorization authorization, IVecozoReturnInfoReceiver returnInfoReceiver)
		{
			if (!authorization.IsAuthenticated) throw new Exception("No valid certificate found");
			_returnInfoReceiver = returnInfoReceiver;
		}

		public EIRetourbestandenResponse EIRetourbestanden(EIRetourbestandenRequest request)
		{
			var data = request.EIRetourbestand.Bestand.Data;
			var fileInfo = request.EIRetourbestand.EIStandaard;
			_returnInfoReceiver.SaveReturnInfo(data, fileInfo.StandaardCode, fileInfo.StandaardVersie, fileInfo.StandaardSubVersie);

			return _successResponse;
		}

		public Task<OntvangenPdfResponse> OntvangenPdf(OntvangenPdfRequest OntvangenPdfRequest)
		{
			var x = new OntvangenPdfResponse { ResultaatCode = Resultaatcode.VSPEDP109 };
			return Task.FromResult(x);
		}

		public Task<ResultaatMeldingResponse> ResultaatMelding(ResultaatMeldingRequest ResultaatMeldingRequest)
		{
			var x = new ResultaatMeldingResponse { Resultaatcode = Resultaatcode.VSPEDP106 };
			return Task.FromResult(x);
		}
	}
}