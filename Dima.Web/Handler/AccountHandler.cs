using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Web.Handler;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(
        Configuration.HttpClienteName
    );

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var response = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return response.IsSuccessStatusCode
            ? new Response<string>(null, 200, "Login realizado com sucesso")
            : new Response<string>(
                null,
                (int)response.StatusCode,
                "Não foi possível realizar o login"
            );
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var response = await _client.PostAsJsonAsync("v1/identity/register", request);
        return response.IsSuccessStatusCode
            ? new Response<string>("Usuário criado com sucesso", 201, null)
            : new Response<string>(
                null,
                (int)response.StatusCode,
                "Não foi possível realizar o seu cadastro"
            );
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}");
        await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
    }
}
