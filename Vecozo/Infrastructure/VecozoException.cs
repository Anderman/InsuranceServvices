using System;

namespace Vecozo.Infrastructure
{
	public sealed class VecozoException : Exception
	{
		public VecozoException(string s, string code) : base(s)
		{
			Data.Add("Code", code);
		}
	}
}