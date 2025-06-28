using ChronoLogic.Api.Client;
using ChronoLogic.Ui.Models;

namespace ChronoLogic.Ui.Mappers;

public static class UserMappers
{
    public static UserInformation ToUserInformation(this UserLoginResponse userLoginResponse)
    {
        return new UserInformation
        {
            UserId = userLoginResponse.UserId,
            Username = userLoginResponse.Username
        };
    }
}