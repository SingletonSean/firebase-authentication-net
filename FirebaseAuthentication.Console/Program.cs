using FirebaseAuthentication.SDK.Extensions;
using FirebaseAuthentication.SDK.Http;
using FirebaseAuthentication.SDK.Requests;
using FirebaseAuthentication.SDK.Responses;
using FirebaseAuthentication.SDK.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirebaseAuthentication.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "AIzaSyC-K9UCzMYvrlamPFCFAZKnhF6fci6E2PE";

            IHost host = Host.CreateDefaultBuilder(args)
                .AddFirebaseAuthenticationSDK(apiKey)
                .Build();

            host.Start();

            try
            {
                IFirebaseRegisterService registerService = host.Services.GetRequiredService<IFirebaseRegisterService>();
                RegisterResponse registerResponse = await registerService.Register(new RegisterRequest
                {
                    Email = "singletonsean2@gmail.com",
                    Password = "test123!"
                });
            }
            catch (ApiException) { }

            IFirebaseLoginService loginService = host.Services.GetRequiredService<IFirebaseLoginService>();
            LoginResponse loginResponse = await loginService.Login(new LoginRequest
            {
                Email = "singletonsean2@gmail.com",
                Password = "test123!"
            });

            IFirebaseRefreshService refreshService = host.Services.GetRequiredService<IFirebaseRefreshService>();
            RefreshResponse refreshResponse = await refreshService.Refresh(new RefreshRequest
            {
                RefreshToken = loginResponse.RefreshToken
            });

            host.Dispose();

            Console.ReadKey();
        }
    }
}
