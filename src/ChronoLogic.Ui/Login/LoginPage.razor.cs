using ChronoLogic.Ui.Models;
using ChronoLogic.Ui.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ChronoLogic.Ui.Login;

public partial class LoginPage : ComponentBase
{
    [Inject] private IUserSessionService UserSessionService { get; set; } = null!;
    [Inject] private IUserApiService UserApiService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    
    private bool _loading = true;
    private IReadOnlyList<UserInformation> _users = [];

    protected override async Task OnInitializedAsync()
    {
        var result = await UserApiService.GetLoginUsersAsync();
        
        if (result.IsSuccess) _users = result.Value!;
        else Snackbar.Add(result.ErrorMessage!, Severity.Error);
        
        _loading = false;
    }
}