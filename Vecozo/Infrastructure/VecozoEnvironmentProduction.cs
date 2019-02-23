namespace Vecozo.Infrastructure
{
	public class VecozoEnvironmentProduction : IVecozoEnvironment
	{
		public VecozoEnvironment Environment => VecozoEnvironment.Production;
	}
}