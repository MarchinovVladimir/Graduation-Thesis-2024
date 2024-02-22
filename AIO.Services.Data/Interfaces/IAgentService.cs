namespace AIO.Services.Data.Interfaces
{
    public interface IAgentService
    {
        Task<bool> IsAgentExistByUserId(string userId);
    }
}
