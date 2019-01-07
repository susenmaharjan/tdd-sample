using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankApplication.Savings.Tests
{
  [TestClass]
  public class AccountTest
  {
    #region Properties

    private IAccount _account;
    private string _accountNumber;
    private double _accountBalance;

    #endregion

    #region Intialize

    [TestInitialize]
    public void Initialize()
    {
      _account = new Account();
      _accountNumber = "123456789";
      _accountBalance = 200_000;
    }


    #endregion

    #region TestAccountStatus

    [TestMethod]
    public void TestAccountStatus_Active_Success()
    {
      Assert.IsTrue(_account.IsAccountActive(_accountNumber), "Failed. Account is not active.");
    }

    [TestMethod]
    public void TestAccountStatus_ArgumentException_Success()
    {
      Assert.ThrowsException<ArgumentException>(() => _account.IsAccountActive(null));
    }

    [TestMethod]
    public void TestAccountStatus_AccountNumberWhiteSpace_ArgumentException_Success()
    {
      Assert.ThrowsException<ArgumentException>(() => _account.IsAccountActive(" "));
    }

    #endregion

    #region TestDeposit

    //scenarios
    //_account number should not be empty or null
    //amount should not be negative and there should price limit

    [TestMethod]
    public void TestDeposit_NullOrWhiteSpace_ArgumentException_Success()
    {
      //when accountNumber is null
      Assert.ThrowsException<ArgumentException>(() => _account.Deposit(null, _accountBalance));

      //when accountNumber is whitespace
      Assert.ThrowsException<ArgumentException>(() => _account.Deposit(" ", _accountBalance));

      //when accountNumber is empty
      Assert.ThrowsException<ArgumentException>(
        () => _account.Deposit(string.Empty, _accountBalance));
    }

    [TestMethod]
    public void TestDeposit_NegativeAccountBalance()
    {
      double negativeAmount = -1_000;

      Assert.ThrowsException<ArgumentException>(() => _account.Deposit(_accountNumber, negativeAmount));
    }

    [TestMethod]
    public void TestDeposit_AmountLimit()
    {
      double depositAmount = 700_001;

      Assert.ThrowsException<ArgumentException>(() => _account.Deposit(_accountNumber, depositAmount));
    }

    #endregion

    #region TestWithdrawal

    //scenarios
    //cash witdrawal minimum 500, maximum 25000
    //withdrawal cash must be less than actual balance

    [TestMethod]
    public void TestWithdrawal_NullOrWhiteSpace_ArgumentException_Success()
    {
      //when accountNumber is null
      Assert.ThrowsException<ArgumentException>(() => _account.Withdrawal(null, _accountBalance));

      //when accountNumber is whitespace
      Assert.ThrowsException<ArgumentException>(() => _account.Withdrawal(" ", _accountBalance));

      //when accountNumber is empty
      Assert.ThrowsException<ArgumentException>(() =>
        _account.Withdrawal(string.Empty, _accountBalance));
    }

    [TestMethod]
    public void TestWithdrawal_Minimum_Withdrawal()
    {
      double belowLimitAmount = 400;

      Assert.ThrowsException<ArgumentException>(() => _account.Withdrawal(_accountNumber, belowLimitAmount));
    }

    [TestMethod]
    public void TestWithdrawal_Maximum_Withdrawal()
    {
      double aboveLimitAmount = 25100;

      Assert.ThrowsException<ArgumentException>(() => _account.Withdrawal(_accountNumber, aboveLimitAmount));
    }

    #endregion

    #region TestActivateAccount
    //scenarios
    //only manager can activate account

    [TestMethod]
    public void TestActivateAccount_NullOrWhiteSpace_ArgumentException()
    {
      Assert.ThrowsException<ArgumentException>(() => _account.ActivateAccount(null, Roles.Administrator));
      Assert.ThrowsException<ArgumentException>(() => _account.ActivateAccount(" ", Roles.Administrator));
      Assert.ThrowsException<ArgumentException>(() => _account.ActivateAccount(string.Empty, Roles.Administrator));
    }

    [TestMethod]
    public void TestActivateAccount_CustomerActivation()
    {
      Assert.ThrowsException<UnauthorizedAccessException>(() => _account.ActivateAccount(_accountNumber, Roles.Customer));
    }

    [TestMethod]
    public void TestActivateAccount_AdministratorActivation()
    {
      //arrange
      var role = Roles.Administrator;

      //act
      var activation = _account.ActivateAccount(_accountNumber, role);

      //assert
      Assert.AreEqual(true, activation);
    }

    #endregion
  }
}