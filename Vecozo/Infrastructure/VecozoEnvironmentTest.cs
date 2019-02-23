namespace Vecozo.Infrastructure
{
	public class VecozoEnvironmentTest : IVecozoEnvironment
	{
		public VecozoEnvironment Environment => VecozoEnvironment.Test;
	}
}