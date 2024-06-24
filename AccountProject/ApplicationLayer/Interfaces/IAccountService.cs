namespace AccountProject.ApplicationLayer.Interfaces
{
    public interface IAccountService
    {
        Task<Dictionary<string, decimal>> GetTopLevelAccountsTotalBalanceAsync();
        Task<List<string>> GetAccountDetailsAsync(string id);
    }
}
