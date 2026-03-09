namespace LR2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount(1000);
            AccountMonitor monitor = new AccountMonitor(account);

            Console.WriteLine("Створено банківський рахунок з початковим балансом 1000.");

            account.Deposit(500);
            account.Withdraw(200);
            account.Withdraw(1500);
        }
    }

    public class BankAccount
    {
        private int _balance;

        public delegate void BalanceChangedHandler(string message);
        public event BalanceChangedHandler? BalanceChangedDeposit;
        public event BalanceChangedHandler? BalanceChangedWithdraw;

        public BankAccount(int initialBalance)
        {
            _balance = initialBalance;
        }

        public void Deposit(int amount)
        {
            _balance += amount;
            BalanceChangedDeposit?.Invoke($"Депозит на суму {amount}. Поточний баланс: {_balance}");
        }

        public void Withdraw(int amount)
        {
            if (amount > _balance)
            {
                Console.WriteLine("Недостатньо коштів!");
                return;
            }
            _balance -= amount;
            BalanceChangedWithdraw?.Invoke($"Зняття на сумму {amount}. Поточний баланс: {_balance}");
        }
    }

    public class  AccountMonitor
    {
        public AccountMonitor(BankAccount account)
        {
            account.BalanceChangedDeposit += OnBalanceChanged;
            account.BalanceChangedWithdraw += OnBalanceChanged;
        }

        public void OnBalanceChanged(string message)
        {
            Console.WriteLine($"{message}");
        }
    }
}
