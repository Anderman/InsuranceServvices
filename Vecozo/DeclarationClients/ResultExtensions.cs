using System.Linq;
using Vecozo.Connected_Services.DeclarationClients;

namespace Vecozo.DeclarationClients
{
	public static class ResultExtensions
	{
		private static readonly Resultaatcode[] SuccessCodes = { Resultaatcode.VSPEDP102 };

		public static void EnsureSuccess(this Resultaatcode result)
		{
			if (!SuccessCodes.Contains(result)) throw new VecozoException($"Fout opgetreden met Vecozo {result}", result.ToString());
		}
	}
}