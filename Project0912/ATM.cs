namespace Project0912;

internal interface IATM<Cash> where Cash : ICash
{
    IList<Cash100> Withdraw(long accountID, int amount);

    void Deposit(long accountID, IEnumerable<Cash100> cashes);

    int GetBalance(long accountID);

    event EventHandler<Account> BigMoneyFetched;
}

internal class ATM : IATM<Cash100>
{
    private readonly ICashBox<Cash100> cashBox = new DefaultCashBox();

    private readonly IBank<Cash100> parentBank;

    public ATM(IBank<Cash100> parentBank, IEnumerable<Cash100> initialCashes)
    {
        cashBox.Add(initialCashes);
        this.parentBank = parentBank;
    }

    public IList<Cash100> Withdraw(long accountID, int amount)
    {
        var account = GetAccount(accountID);
        var cashNum = amount / 100;
        if (amount % 100 != 0)
            throw new InvalidOperationException($"This ATM doesn't support withdrawing {amount} currencies.");
        if (cashNum > cashBox.Count) throw new InvalidOperationException("This ATM doesn't have enough money.");
        if (account.Balance < amount)
            throw new InvalidOperationException($"This account id {accountID} has no enough money.");
        parentBank.SetAccount(accountID, account with { Balance = account.Balance - amount });
        var result = cashBox.Get(cashNum);
        if (amount >= 10000) BigMoneyFetched?.Invoke(this, account);
        return result;
    }

    public void Deposit(long accountID, IEnumerable<Cash100> cashes)
    {
        var account = GetAccount(accountID);
        cashBox.Add(cashes);
        parentBank.SetAccount(accountID, account with { Balance = account.Balance + cashes.Count() * 100 });
    }

    public int GetBalance(long accountID)
    {
        return GetAccount(accountID).Balance;
    }

    public event EventHandler<Account> BigMoneyFetched;

    private Account GetAccount(long accountID)
    {
        var got = parentBank.GetAccount(accountID, out var account);
        if (!got) throw new InvalidOperationException($"Account id {accountID} not found.");
        return account;
    }
}