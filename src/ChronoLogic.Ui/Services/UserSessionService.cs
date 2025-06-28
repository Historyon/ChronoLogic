using ChronoLogic.Ui.Models;

namespace ChronoLogic.Ui.Services;

public interface IUserSessionService
{
    bool IsUserLoggedIn { get; }
    UserInformation? User { get; }
    void Login(UserInformation userInformation);
    void Logout();
}

internal class UserSessionService : IUserSessionService
{
    public bool IsUserLoggedIn => User is not null;
    public UserInformation? User { get; private set; }
    
    public void Login(UserInformation userInformation)
    {
        User = userInformation;
    }

    public void Logout()
    {
        User = null;
    }
}