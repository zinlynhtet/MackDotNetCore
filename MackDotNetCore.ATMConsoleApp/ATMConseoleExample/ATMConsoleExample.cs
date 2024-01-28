using MackDotNetCore.ATMConsoleApp.Model;
using MackDotNetCore.ConsoleApp.EFCoreExamples;
using System;
using System.Linq;

namespace MackDotNetCore.ATMConsoleApp.ATMConseoleExample
{
    public class ATMConsoleExample
    {
        private readonly AppDbContext _dbContext;

        public ATMConsoleExample()
        {
            _dbContext = new AppDbContext();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the ATM Console App!");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Delete");
            Console.Write("Enter your choice (1-4): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Write("Enter Card Number: ");
                if (double.TryParse(Console.ReadLine(), out double cardNumber))
                {
                    Console.Write("Enter PIN: ");
                    if (int.TryParse(Console.ReadLine(), out int pin))
                    {
                        // Verify credentials before proceeding
                        if (VerifyCredentials(cardNumber, pin))
                        {
                            switch (choice)
                            {
                                case 1:
                                    Create(cardNumber, pin);
                                    break;
                                case 2:
                                    Deposit(cardNumber, pin);
                                    break;
                                case 3:
                                    Withdraw(cardNumber, pin);
                                    break;
                                case 4:
                                    Delete(cardNumber, pin);
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Card Number or PIN. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid PIN. Please enter a valid integer PIN.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Card Number. Please enter a valid numeric card number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
            }
        }

        private bool VerifyCredentials(double cardNumber, int pin, bool skipVerification = false)
        {
            if (skipVerification)
            {
                return true;
            }

            return _dbContext.Blogs.Any(x => x.CardNum == cardNumber && x.Pin == pin);
        }

        private void Create(double cardNumber, int pin)
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            if (double.TryParse(Console.ReadLine(), out double balance))
            {
                // Skip PIN/card verification for registration
                if (VerifyCredentials(cardNumber, pin, true))
                {
                    BlogDataModel blog = new BlogDataModel
                    {
                        CardNum = cardNumber,
                        Pin = pin,
                        FirstName = firstName,
                        LastName = lastName,
                        Balance = balance
                    };

                    _dbContext.Blogs.Add(blog);
                    int result = _dbContext.SaveChanges();

                    string message = result > 0 ? "Registration Successful" : "Registration Failed.";
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("Invalid PIN or Card Number for registration.");
                }
            }
            else
            {
                Console.WriteLine("Invalid balance entered.");
            }
        }

        private void Deposit(double cardNumber, int pin)
        {
            Console.Write("Enter the amount to deposit: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                BlogDataModel account = _dbContext.Blogs.First(x => x.CardNum == cardNumber && x.Pin == pin);
                account.Balance += amount;

                int result = _dbContext.SaveChanges();
                string message = result > 0 ? $"Deposit Successful. Your new balance is: {account.Balance}" : "Deposit Failed.";
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Invalid amount entered.");
            }
        }

        private void Withdraw(double cardNumber, int pin)
        {
            Console.Write("Enter the amount to withdraw: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                BlogDataModel account = _dbContext.Blogs.First(x => x.CardNum == cardNumber && x.Pin == pin);

                if (account.Balance >= amount)
                {
                    account.Balance -= amount;
                    int result = _dbContext.SaveChanges();
                    string message = result > 0 ? $"Withdrawal Successful. Your remaining balance is: {account.Balance}" : "Withdrawal Failed.";
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("Insufficient balance for withdrawal.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount entered.");
            }
        }

        private void Delete(double cardNumber, int pin)
        {
            BlogDataModel blog = _dbContext.Blogs.FirstOrDefault(x => x.CardNum == cardNumber && x.Pin == pin);
            if (blog != null)
            {
                _dbContext.Blogs.Remove(blog);
                int result = _dbContext.SaveChanges();
                string message = result > 0 ? "Account Deletion Successful." : "Account Deletion Failed.";
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("No account found for deletion.");
            }
        }
    }
}
