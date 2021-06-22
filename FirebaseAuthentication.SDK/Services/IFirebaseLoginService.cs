using FirebaseAuthentication.SDK.Requests;
using FirebaseAuthentication.SDK.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseAuthentication.SDK.Services
{
    public interface IFirebaseLoginService
    {
        [Post("/v1/accounts:signInWithPassword")]
        Task<LoginResponse> Login([Body] LoginRequest loginRequest);
    }
}
