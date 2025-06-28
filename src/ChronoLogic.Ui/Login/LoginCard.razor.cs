using Microsoft.AspNetCore.Components;

namespace ChronoLogic.Ui.Login;

public partial class LoginCard : ComponentBase
{
    [Parameter] public string UserDisplayName { get; set; } = string.Empty;
    [Parameter] public EventCallback OnClick { get; set; }

    private string Initial => string.IsNullOrWhiteSpace(UserDisplayName) ? string.Empty : UserDisplayName[..1];

    private async Task HandleClickAsync()
    {
        await OnClick.InvokeAsync();
    }
}