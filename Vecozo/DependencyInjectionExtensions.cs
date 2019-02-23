using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vecozo.Certificate;
using Vecozo.Connected_Services.ReturnInfoServices;
using Vecozo.Connected_Services.Vecozo.IsAliveInterface;
using Vecozo.Cov;
using Vecozo.DeclarationClients;
using Vecozo.Infrastructure;
using Vecozo.ReturnInfoClients;
using Vecozo.ReturnInfoServices;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddVecozoSoapServices<TFile>(this IServiceCollection services) where TFile : class, IVecozoReturnInfoReceiver
		{
			// Push
			
			// Return info services (vecozo Push) 
			services.AddTransient<IAliveService, ReturnInfoServicesIsAlive>();
			services.AddTransient<IReceiver, ReturnInfoService>();
			// Allow vecozo to authenticate with client certificate 
			services.Configure<HttpsConnectionAdapterOptions>(options =>
			{
				options.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
				options.CheckCertificateRevocation = false;
				options.ClientCertificateValidation = (certificate2, chain, policyErrors) => true; // accept any cert (testing purposes only)
			});
			services.AddTransient<IVecozoReturnInfoReceiver, TFile>();
			services.TryAddTransient<IClientCertificateAuthorization, ClientCertificateAuthorization>();

			// Pull

			// Soap client
			services.AddSoapClients();
			//Vecozo COV client
			services.AddTransient<CovClient>();
			// Declaration clients
			services.AddTransient<DeclarationClient>();
			// Return info (pull vecozo)
			services.AddTransient<ReturnInfoClientFind>();
			services.AddTransient<ReturnInfoClientFile>();
			services.AddTransient<ReturnInfoClientPdfFile>();
			services.AddTransient<VecozoEnvironmentTest>();
			services.AddTransient<VecozoEnvironmentAcceptance>();
			services.AddTransient<VecozoEnvironmentProduction>();

			return services;
		}
	}
}