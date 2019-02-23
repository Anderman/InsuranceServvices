using System.Linq;
using Vecozo.Connected_Services.ReturnInfoClients;
using Vecozo.Infrastructure;

// ReSharper disable StringLiteralTypo

namespace Vecozo.ReturnInfoClients
{
	public static class ReturnInfoResultExtensions
	{
		private static readonly Resultaatcode[] SuccessCodes = { Resultaatcode.VSPEDP107, Resultaatcode.VSPEDP113, Resultaatcode.VSPEDP114, Resultaatcode.VSPEDP115, Resultaatcode.VSPEDP201 };

		public static void EnsureSuccess(this Resultaatcode result)
		{
			if (!SuccessCodes.Contains(result)) throw new VecozoException($"Fout opgetreden met Vecozo {result}", result.ToString());
		}
	}
}