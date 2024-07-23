using System.Configuration;
using System.Text.Json;
using System.Threading.Channels;
using AccountAppWithList.Exceptions;
using AccountAppWithList.Models;

namespace AccountAppWithList
{
    internal class Program
    {
        static List<Account> accounts = new List<Account>();
        public static string path = ConfigurationManager.AppSettings["filePath"]!.ToString();
        static void Main(string[] args)
        {
            DisplayMenu();
        }

        static void DisplayMenu()

        {
            accounts = DeserializeAccounts();
            while (true)
            {

                Console.WriteLine("\nWelcome to Account Management App\n" +
                    "What do you want to do?\n" +
                    "1. Add Account\n" +
                    "2. Display all Accounts\n" +
                    "3. Find Account by Id\n" +
                    "4. Update Account\n" +
                    "5. Remove an account by Id\n" +
                    "6. Clear All Accounts\n" +
                    "7. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {

                    DoTask(choice);
                }
                catch (AccountListEmptyException ale)
                {
                    Console.WriteLine(ale.Message);
                }
                catch (AccountNotFoundException anf)
                {
                    Console.WriteLine(anf.Message);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void DoTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    AddNewAccount();
                    break;

                case 2:
                    accounts.ForEach(account => Console.WriteLine(account));
                    break;

                case 3:
                    Account findAccount = FindAccountById();
                    if (findAccount == null)
                        throw new AccountNotFoundException("Account not found");
                    else
                        Console.WriteLine(findAccount);
                    break;
                case 4:
                    UpdateAccountDetails();
                    break;
                case 5:
                    RemoveAccount();
                    break;
                case 6:
                    if (accounts.Count == 0)
                        throw new AccountListEmptyException("Account list empty");
                    else
                        accounts.Clear();
                    break;
                case 7:
                    SerializeAccounts();
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Please Enter valid input !");
                    break;


            }

        }

        static void SerializeAccounts()
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(JsonSerializer.Serialize(accounts));
            }
        }

        static List<Account> DeserializeAccounts()
        {
            if (!File.Exists(path))
            {
                return new List<Account>();
            }
            using (StreamReader sr = new StreamReader(path))
            {
                List<Account> accounts =  JsonSerializer.Deserialize<List<Account>>(sr.ReadToEnd())!;
                return accounts;
            }
        }

        static void RemoveAccount()
        {
            Account findAccount = FindAccountById();
            if (findAccount == null)
                throw new AccountNotFoundException("Account not found");
            else
            {
                accounts.Remove(findAccount);
                Console.WriteLine("Account Deleted successfully");
            }
        }
        static void UpdateAccountDetails()
        {
            Account findAccount = FindAccountById();
            if (findAccount == null)
                throw new AccountNotFoundException("Account not found");
            else
            {
                Console.WriteLine("Enter updated name");
                string name = Console.ReadLine();
                findAccount.Name = name;
                Console.WriteLine("Account updated successfully");
            }


        }


        static Account FindAccountById()
        {
            Account findAccount = null;
            Console.WriteLine("Enter Id : ");
            int id = Convert.ToInt32(Console.ReadLine());
            findAccount = accounts.Where(item => item.AccountNumber == id).FirstOrDefault();
            return findAccount;
        }
        static void AddNewAccount()
        {
            Console.WriteLine("Enter Id : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Name : ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Balance : ");
            double balance = Convert.ToDouble(Console.ReadLine());
            Account newAccount = Account.CreateAccount(id, name, balance);
            accounts.Add(newAccount);
            Console.WriteLine("New Account Added Successfully");
        }
    }


}
