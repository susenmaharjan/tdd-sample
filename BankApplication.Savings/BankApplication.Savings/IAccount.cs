namespace BankApplication.Savings
{
  public interface IAccount
  {
    double AccountBalance { get; set; }
    bool AccountStatus { get; set; }
    bool IsAccountActive(string accountNumber);
    double Deposit(string accountNumber, double amount);
    double Withdrawal(string accountNumber, double amount);
    bool ActivateAccount(string accountNumber, Roles userRole);
  }
}
