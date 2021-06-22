using FirebaseAuthentication.SDK.Http;
using FirebaseAuthentication.SDK.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseAuthentication.SDK.Extensions
{
    public static class AddFirebaseAuthenticationSDKHostBuilderExtensions
    {
        private const string IDENTITY_TOOLKIT_BASE_URL = "https://identitytoolkit.googleapis.com";
        private const string SECURE_TOKEN_BASE_URL = "https://securetoken.googleapis.com";

        public static IHostBuilder AddFirebaseAuthenticationSDK(this IHostBuilder hostBuilder, string apiKey)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<FirebaseApiKeyHttpMessageHandler>(s => new FirebaseApiKeyHttpMessageHandler(apiKey));

                services.AddRefitClient<IFirebaseRegisterService>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(IDENTITY_TOOLKIT_BASE_URL))
                    .AddHttpMessageHandler<FirebaseApiKeyHttpMessageHandler>();

                services.AddRefitClient<IFirebaseLoginService>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(IDENTITY_TOOLKIT_BASE_URL))
                    .AddHttpMessageHandler<FirebaseApiKeyHttpMessageHandler>();

                services.AddRefitClient<IFirebaseRefreshService>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(SECURE_TOKEN_BASE_URL))
                    .AddHttpMessageHandler<FirebaseApiKeyHttpMessageHandler>();
            });

            return hostBuilder;
        }
    }
}
