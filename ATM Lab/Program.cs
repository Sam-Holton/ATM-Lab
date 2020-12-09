using System;
using System.IO;

namespace ATM_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            ATM atm = new ATM();
            var currentAccount = new Account(null, null, 0);
            int userInputInt = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to Grand Circus Bank!");
                Console.WriteLine();
                Console.WriteLine("[1] Log In");
                Console.WriteLine("[2] Create New Account");
                Console.WriteLine();
                Console.Write("Please Select One of the Above to Continue: ");
                string userInputString = Console.ReadLine();

                if (userInputString == "1") atm.Login(currentAccount);
                else if (userInputString == "2") atm.Register();
                else
                {
                    Console.WriteLine("Entry Not Recognized. Hit Enter to Continue.");
                    Console.ReadLine();
                    continue;
                }

                do
                {
                    Console.Clear();
                    Console.WriteLine("[1] Deposit Funds");
                    Console.WriteLine("[2] Withdraw Funds");
                    Console.WriteLine("[3] Check Balance");
                    Console.WriteLine("[4] Log Off");
                    Console.WriteLine();
                    Console.Write("Please Select One of the Above Options: ");
                    if (int.TryParse(Console.ReadLine(), out userInputInt)
                        && userInputInt > 0
                        && userInputInt < 5)
                    {
                        switch (userInputInt)
                        {
                            case 1:
                                {
                                    atm.Deposit(currentAccount.Balance);
                                    break;
                                }
                            case 2:
                                {
                                    atm.Withdraw(currentAccount.Balance);
                                    break;
                                }
                            case 3:
                                {
                                    atm.CheckBalance(currentAccount.Balance);
                                    break;
                                }
                            case 4:
                                {
                                    atm.Logout(currentAccount);
                                    break;
                                }
                            default: break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input. Please Try Again. (Hit Enter to Continue)");
                        Console.ReadLine();
                    }
                } while (userInputInt != 4);
            } while (true);
        }
    }

    public class ATM
    {
        public void Register ()
        {
            Console.Clear();
            Console.WriteLine("We're ecstatic that you've decided to join Grand Circus Bank!");
            Console.WriteLine();
            Console.Write("Please Enter a User Name: ");
            string userName = Console.ReadLine();
            Console.Write("Please Enter a Password: ");
            string passWord = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Fantastic! Welcome to Grand Circus Bank! (Hit Enter to Continue)");
            Console.ReadLine();
            // Validate Input, Set/Check Password Criteria, Check userName Against Existing Database, Save Info to Disk, Hash Password
        }

        public Account Login(Account currentAccount)
        {
            Console.Clear();
            Console.Write("Please Enter Your User Name: ");
            string userName = Console.ReadLine();
            Console.Write("Please Enter Your Password: ");
            string passWord = Console.ReadLine();

            
            
            using (var accountReader = new StreamReader("../../../BlackHatWetDream.txt"))
            {
                string temp;
                while ((temp = accountReader.ReadLine()) != null)
                {
                    string[] accountLine = temp.Split('%');
                    if (accountLine[0] == userName 
                        && accountLine[1] == passWord)
                    {
                        currentAccount.Balance = Decimal.Parse(accountLine[2]);
                        currentAccount.UserName = userName;
                        break;
                    }
                }
            }
            
            if (currentAccount.UserName == null)
            {
                Console.WriteLine("I'm sorry, but that User Name and Password combination do not match our records (Hit Enter to Continue)");
                Console.ReadLine();
            }
            return currentAccount;
        }

        public void Logout(Account currentAccount)
        {
            currentAccount.UserName = null;
            currentAccount.PassWord = null;
            currentAccount.Balance = 0.0M;

            Console.WriteLine("Thank You for Banking with Grand Circus! (Hit Enter Complete Log Off)");
            Console.ReadLine();
        }

        public void CheckBalance(decimal balance)
        {
            Console.Clear();
            Console.WriteLine($"Your Current Balance Is: {balance}");
            Console.WriteLine();
            Console.WriteLine("Please Hit Enter to Continue");
            Console.ReadLine();
        }

        public decimal Deposit(decimal balance)
        {
            decimal originalBalance = balance;
            Console.Clear();
            Console.Write("Please Enter the Amount You Would Like to Deposit: ");
            decimal.TryParse(Console.ReadLine(), out decimal userInput);
            balance += userInput;
            Console.WriteLine();
            Console.WriteLine($"Original Balance: {originalBalance}");
            Console.WriteLine($"Withdrawal Amount: {userInput}");
            Console.WriteLine($"New Balance: {balance}");
            Console.WriteLine();
            Console.WriteLine("Please Hit Enter to Continue");
            Console.ReadLine();
            return balance;
        }

        public decimal Withdraw(decimal balance)
        {
            decimal originalBalance = balance;
            Console.Clear();
            Console.Write("Please Enter the Amount You Would Like to Withdraw: ");
            decimal.TryParse(Console.ReadLine(), out decimal userInput);
            balance -= userInput;
            Console.WriteLine();
            Console.WriteLine($"Original Balance: {originalBalance}");
            Console.WriteLine($"Withdrawal Amount: {userInput}");
            Console.WriteLine($"New Balance: {balance}");
            Console.WriteLine();
            Console.WriteLine("Please Hit Enter to Continue");
            Console.ReadLine();
            return balance;
        }
    }

    public class Account
    {
        public Account(string userName, string passWord, decimal balance)
        {
            string UserName = userName;
            string PassWord = passWord;
            decimal Balance = balance;
        }

        public string UserName { get; set; }
        public string PassWord { get; set; }
        public decimal Balance { get; set; }

    }
}
