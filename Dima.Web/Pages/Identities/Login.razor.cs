using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identities;

public partial class LoginPage : ComponentBase
{
    #region Dependencies

    [Inject]
    public IAccountHandler Handler { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion

    #region Properties

    public bool IsBusy { get; set; } = false;

    public LoginRequest InputModel { get; set; } = new();

    #endregion

    #region Methods

    public async Task OnValidSubmit()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.LoginAsync(InputModel);

            if (result.IsSuccess)
                NavigationManager.NavigateTo("/");
            else
                Snackbar.Add(result.Message!, Severity.Error);
        }
        catch (System.Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}
