using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public interface IUserGameService
    {
        Task<int> Purchase(Purchase purchase);
    }
}
