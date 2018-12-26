using Microsoft.AspNetCore.Server.Kestrel.Https;
using Vecozo.Connected_Services.ReturnInfoServices;
using Vecozo.Connected_Services.Vecozo.IsAliveInterface;
using Vecozo.DeclarationClients;
using Vecozo.ReturnInfoClients;
using Vecozo.ReturnInfoServices;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
	public static class VecozoServiceCollectionExtensions
	{
		public static IServiceCollection AddVecozoSoapServices<TFile>(this IServiceCollection services) where TFile : class, IVecozoReturnInfoReceiver
		{
			//allow vecozo to authenticate with client certificate 
			services.Configure<HttpsConnectionAdapterOptions>(options =>
			{
				options.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
				options.CheckCertificateRevocation = false;
				options.ClientCertificateValidation = (certificate2, chain, policyErrors) => true; // accept any cert (testing purposes only)
			});
			services.AddTransient<IVecozoReturnInfoReceiver, TFile>();
			//Soap client
			services.AddSoapClients();
			//declaration clients
			services.AddTransient<DeclarationClient>();
			//return info (pull vecozo)
			services.AddTransient<ReturnInfoClientFind>();
			services.AddTransient<ReturnInfoClientFile>();
			services.AddTransient<ReturnInfoClientPdfFile>();
			//Return info services (vecozo Push) 
			services.AddTransient<IAliveService, ReturnInfoServicesIsAlive>();
			services.AddTransient<IReceiver, ReturnInfoService>();
			return services;
		}
	}
}