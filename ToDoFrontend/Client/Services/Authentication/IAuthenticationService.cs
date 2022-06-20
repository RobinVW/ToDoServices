using ToDoFrontend.Client.Services.Base;

namespace ToDoFrontend.Client.Services.Authentication
{
        public interface IAuthenticationService
        {
            Task<Response<AuthResponse>> AuthenticateAsync(LoginModel loginModel); 
            public Task Logout();
        }
}