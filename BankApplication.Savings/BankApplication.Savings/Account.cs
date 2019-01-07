using System;

namespace BankApplication.Savings
{
  public class Account : IAccount
  {
    public double AccountBalance { get; set; }
    public bool AccountStatus { get; set; }

    public bool IsAccountActive(string accountNumber)
    {
      if (string.IsNullOrWhiteSpace(accountNumber))
        throw new ArgumentException("Account number cannot be null or have White Spaces.");
      AccountStatus = true;
      return AccountStatus;
    }
    public double Deposit(string accountNumber, double amount)
    {
      if (string.IsNullOrWhiteSpace(accountNumber))
        throw new ArgumentException("Account number should be null or whitespace");
      if (amount < 0)
        throw new ArgumentException("Amount cannot be negative");
      if (amount > 700_000) //suppose 700,000 is limit for deposit
        throw new ArgumentException("You cannot deposit more than 700,000.");
      return AccountBalance = AccountBalance + amount;
    }
    public double Withdrawal(string accountNumber, double amount)
    {
      if (string.IsNullOrWhiteSpace(accountNumber))
        throw new ArgumentException("Account number should be null or whitespace");
      if (amount < 500)
        throw new ArgumentException("You cannot withdraw less than 500.");
      if (amount > 25000)
        throw new ArgumentException("You cannot withdraw more than 25,000");

      return AccountBalance = AccountBalance - amount;
    }
    public bool ActivateAccount(string accountNumber, Roles userRole)
    {
      if (string.IsNullOrWhiteSpace(accountNumber))
        throw new ArgumentException("Account number should be null or whitespace");
      if (userRole == Roles.Customer)
        throw new UnauthorizedAccessException("You are not authorized");
      AccountStatus = true;
      return AccountStatus;
    }

  }
}