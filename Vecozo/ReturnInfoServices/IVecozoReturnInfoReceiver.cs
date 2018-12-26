namespace Vecozo.ReturnInfoServices
{
	public interface IVecozoReturnInfoReceiver
	{
		void SaveReturnInfo(byte[] file, string eiCode, int version, int subVersion);
	}
}