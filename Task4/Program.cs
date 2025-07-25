

namespace Task4
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            
            
                Balance += amount;
                return true;
            
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            
            
                return false;
            
        }
    }
    public class SavingsAccount : Account
    {

        public SavingsAccount(string name = "Unnamed Account", double balance = 0.0, double interestRate = 3.0) : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public double InterestRate { get; set; }

        public override bool Deposit(double amount)
        {
            if (amount < 0)
            {
                return false;
            }


            double interest = amount * (InterestRate / 100);
            return base.Deposit(amount + interest);
        }




    }
    public class CheckingAccount : Account
    {
        const double WithdrawalFee = 1.5;
        public CheckingAccount(string name = "Unnamed Account", double balance = 0.0) : base(name, balance)
        {
        }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + WithdrawalFee);
        }

    }
    public class TrustAccount : SavingsAccount
    {
         int withdrawcount = 0;
        const int bonusdeposited = 50;
        const double Maxdeposits = 5000;
        const int Maxwithdrawal = 3;
        const double MaxWithdrawPercent = 0.2;

        public TrustAccount(string name = "Unnamed Account", double balance = 0.0, double interestRate = 3.0) : base(name, balance, interestRate)
        {
        }
        public override bool Deposit(double amount)
        {
            if (amount >= Maxdeposits)
                amount += bonusdeposited;

            return base.Deposit(amount);
        }
        public override bool Withdraw(double amount)
        {
            if (withdrawcount> Maxwithdrawal)

            {
                return false;
            }
            withdrawcount++;
            return base.Withdraw(amount);
        }
    }
    public static class AccountUtil
    {
        

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing =================================");
            foreach (var account in accounts)
            {
                if (account.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {account.Name}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {account.Name}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing ================================");
            foreach (var account in accounts)
            {
                if (account.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {account.Name}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {account.Name}");
            }
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
           
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

             
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            
            AccountUtil.Withdraw(new List<Account>(savAccounts), 2000);
            AccountUtil.Deposit(new List<Account>(savAccounts), 2000);

           
             
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            
            AccountUtil.Deposit(new List<Account>(checAccounts), 1000);
            AccountUtil.Withdraw(new List<Account>(checAccounts), 2000);
            AccountUtil.Withdraw(new List<Account>(checAccounts), 2000);
           
            
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Deposit(new List<Account>(trustAccounts), 1000);
            AccountUtil.Deposit(new List<Account>(trustAccounts), 6000);
            AccountUtil.Withdraw(new List<Account>(trustAccounts), 2000); ;
            AccountUtil.Withdraw(new List<Account>(trustAccounts), 3000);
            AccountUtil.Withdraw(new List<Account>(trustAccounts), 5000);
            Console.WriteLine();
        }

    }
}

