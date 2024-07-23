using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AccountAppWithList.Models
{   
    internal class Account
    {
        const double MIN_BALANCE = 500;
        public int AccountNumber { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }


        public Account(int accountNumber, string name, double balance)
        {
            AccountNumber = accountNumber;
            Name = name;
            if(balance < MIN_BALANCE)
                Balance = MIN_BALANCE;
            else Balance = balance;
        }

        public Account(int accountNumber, string name)
        {
            AccountNumber = accountNumber;
            Name = name;
            Balance = MIN_BALANCE;
        }

        public Account() { }
        //public bool Deposit(double amount)
        //{
        //    Balance += amount;
        //    return true;

        //}

        //public bool Withdraw(double amount)
        //{
        //    if ((Balance - amount) < MIN_BALANCE)
        //        return false;
        //    Balance -= amount;
        //    return true;


        //}

        public static Account AccountWithMaxBalance(Account[] accounts)
        {
            double maxBalance = 0;
            Account accountWithMaxBalance = null;
            foreach (Account account in accounts)
            {   

                if (account.Balance > maxBalance)
                {   
                   maxBalance = account.Balance;
                    accountWithMaxBalance = account;
                }

                
            }
            return accountWithMaxBalance;
        }

        public static Account CreateAccount(int id , string name , double balance = MIN_BALANCE)
        {
            return new Account(id, name, balance);
        } 

        public override string ToString()
        {
            return $"=================={Name}==========================" +
                    $"\nAccount No : {AccountNumber}" +
            $"\nName : {Name}" +
                $"\nBalance : {Balance} ";
        }
    }
}
