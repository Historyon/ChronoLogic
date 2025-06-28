using ChronoLogic.Api.Client;
using ChronoLogic.Ui.Common;
using ChronoLogic.Ui.Mappers;
using ChronoLogic.Ui.Models;

namespace ChronoLogic.Ui.Services;

public interface IUserApiService
{
    Task<Result<IReadOnlyList<UserInformation>>> GetLoginUsersAsync();
}

internal class UserApiService(ChronoLogicApiClient apiClient) : IUserApiService
{
    public async Task<Result<IReadOnlyList<UserInformation>>> GetLoginUsersAsync()
    {
        try
        {
            var users = await apiClient.GetUsersAllAsync();
            return Result<IReadOnlyList<UserInformation>>
                .Success(users.Select(user => user.ToUserInformation()).ToList());
        }
        catch
        {
            return Result<IReadOnlyList<UserInformation>>.Failure("Es wurden keine Benutzer gefunden.");
        }
    }
}