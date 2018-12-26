using System;
using System.IO;
// ReSharper disable IdentifierTypo

// ReSharper disable StringLiteralTypo

namespace Vecozo.Test
{
	public static class TestHelper
	{
		public static string TestResources => Root + "Test/resources/";
		public static string Root => Path.GetFullPath(".").ToLower().RemoveLiveTestPath().Split("test")[0];

		public static string RemoveLiveTestPath(this string s)
		{
			var x = s.IndexOf(".vs\\", StringComparison.Ordinal);
			if (x == -1) return s;
			var y = s.IndexOf("\\t\\", StringComparison.Ordinal);
			return s.Substring(0, x) + s.Substring(y).Replace("\\t\\", "");
		}
	}
}