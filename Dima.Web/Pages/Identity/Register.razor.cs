using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
    #region Dependencies

    [Inject]
    public IAccountHandler Handler { get; set; } = null!;

    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion

    #region Properties

    public bool IsBusy { get; set; } = false;

    public RegisterRequest InputModel { get; set; } = new();

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
        {
            NavigationManager.NavigateTo("/");
        }
    }

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            Console.WriteLine(InputModel);
            var result = await Handler.RegisterAsync(InputModel);

            if (result.IsSuccess)
            {
                SnackBar.Add(result.Message!, Severity.Success);
                NavigationManager.NavigateTo("/login");
            }
            else
                SnackBar.Add(result.Message!, Severity.Error);
        }
        catch (Exception e)
        {
            SnackBar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}
