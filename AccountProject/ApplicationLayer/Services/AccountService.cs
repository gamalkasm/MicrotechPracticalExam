using AccountProject.ApplicationLayer.Interfaces;
using AccountProject.DomainLayer.Entities;
using AccountProject.InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AccountProject.ApplicationLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly TestDevContext _context;

        public AccountService(TestDevContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, decimal>> GetTopLevelAccountsTotalBalanceAsync()
        {
            var accounts = await _context.Accounts.ToListAsync();
            var topLevelAccounts = accounts.Where(a => a.ACC_Parent == null).ToList();
            var result = new Dictionary<string, decimal>();

            foreach (var topLevel in topLevelAccounts)
            {
                var totalBalance = GetTotalBalance(topLevel.ACC_Number, accounts);
                result.Add(topLevel.ACC_Number, totalBalance);
            }

            return result;
        }
        public async Task<List<string>> GetAccountDetailsAsync(string id)
        {
            var details = new List<string>();

            // Find the root parent account
            var rootParent = await _context.Accounts.FirstOrDefaultAsync(a => a.ACC_Number == id);
            if (rootParent != null)
            {
                // Get child accounts with balance > 0
                GetChildAccounts(rootParent, details);
            }

            return details;
        }
        private decimal GetTotalBalance(string accountId, List<Account> allAccounts)
        {
            var children = allAccounts.Where(a => a.ACC_Parent == accountId).ToList();
            if (!children.Any())
            {
                return allAccounts.FirstOrDefault(a => a.ACC_Number == accountId).Balance ?? 0;
            }

            return children.Sum(child => GetTotalBalance(child.ACC_Number, allAccounts));
        }
        private void GetChildAccounts(Account parentAccount, List<string> details)
        {
            // Find child accounts with balance > 0
            var childAccounts = _context.Accounts
                .Where(a => a.ACC_Parent == parentAccount.ACC_Number)
                .ToList();

            foreach (var child in childAccounts)
            {
                if (child.Balance != null && child.Balance > 0)
                {
                    var serialId = GetSerialId(child.ACC_Number);
                    details.Add($"Account {serialId} = {child.Balance}");
                }
                GetChildAccounts(child, details);
            }
        }
        private string GetSerialId(string accNumber)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.ACC_Number == accNumber);
            if (account == null)
                return string.Empty;

            var serialId = $"{accNumber}";

            var parent = account.ACC_Parent;
            while (!string.IsNullOrEmpty(parent))
            {
                var parentAccount = _context.Accounts.FirstOrDefault(a => a.ACC_Number == parent);
                if (parentAccount == null)
                    break;

                serialId = $"{parent} - {serialId}";
                parent = parentAccount.ACC_Parent;
            }

            return serialId;
        }

       
    }
}
