using System;
using System.Collections.Generic;
using System.Text;
/**
 * @author Ben Lucas
 *@File: MainClass.cs 
 */
namespace Lucas_Program_0
{
    /// <summary>  
    ///  Dispalys a menu of choices for user then allows user to either deposit, withdraw, add interest, show balance, or quit.  
    /// </summary>  
    class MainClass
    {
        private Account account = new Account();
        /// <summary>  
        ///  Prints user instructions
        /// </summary>  
        public static void DisplayMenu()
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("(D)eposit");
            Console.WriteLine("(W)ithdraw");
            Console.WriteLine("(C)alculateInterest");
            Console.WriteLine("(S)howBalance");
            Console.WriteLine("(Q)uit");
            Console.WriteLine("************************************");
            Console.WriteLine("Make choice by entering first letter of choice");
        }
        static void Main(string[] args) {

            Account account = new Account();//single account object
            double amount = 0.0;//amount of money to manipulate account balance
            ConsoleKeyInfo choice;//stores user menu decision

            Console.WriteLine("************************************");
            Console.WriteLine("Welcome to Bernard's Bodacious Bank!");
            Console.WriteLine("************************************");
            Console.WriteLine("We have opened your account");
            Console.WriteLine("Current Balance: {0:C2}", account.GetBalance());//display balance
            DisplayMenu();
            choice = Console.ReadKey();
            while (choice.Key != ConsoleKey.Q) {//while loop for quit option
                switch (choice.Key) {//switch on choice
                    case ConsoleKey.D:
                        Console.WriteLine("\n\nPlease enter amount to deposit:");//prompt user
                        while (!double.TryParse(Console.ReadLine(), out amount))//while ReadLine() isn't a double print error
                        {
                            Console.WriteLine("XXXXXXXXXXXXXXXXXErrorXXXXXXXXXXXXXXXXX");
                            Console.WriteLine("            Improper Input!            ");
                            Console.WriteLine(" Please enter a number with a decimal: ");
                            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                        }
                        account.Deposit(amount);//deposit amount entered by user
                        Console.WriteLine("************************************");
                        Console.WriteLine("Deposited {0:C2} Succesfully", amount);//print success message
                        Console.WriteLine("Current Balance: {0:C2}", account.GetBalance());//print new balance
                        Console.WriteLine("************************************");
                        DisplayMenu();
                        choice = Console.ReadKey();//new user choice for next iteration
                        break;
                    case ConsoleKey.W:
                        Console.WriteLine("\n\nPlease enter amount to withdraw:");
                        while (!double.TryParse(Console.ReadLine(), out amount))//while ReadLine() isn't a double print error
                        {
                            Console.WriteLine("XXXXXXXXXXXXXXXXXErrorXXXXXXXXXXXXXXXXX");
                            Console.WriteLine("            Improper Input!            ");
                            Console.WriteLine(" Please enter a number with a decimal: ");
                            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                        }
                        if (account.GetBalance() - amount >= 0)//check for over draft
                        {
                            account.Withdraw(amount);//withdraw amount entered by user
                            Console.WriteLine("************************************");
                            Console.WriteLine("Withdrew {0:C2} Succesfully", amount);//success message
                            Console.WriteLine("Current Balance: {0:C2}", account.GetBalance());//new balance message
                            Console.WriteLine("************************************");
                        }
                        else {
                            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXErrorXXXXXXXXXXXXXXXXXXXX");
                            Console.WriteLine("Insufficient Funds! Current Balance: {0:C2}", account.GetBalance());//insufficient funds message
                            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                        }
                        DisplayMenu();
                        choice = Console.ReadKey();//new user choice for next iteration
                        break;
                    case ConsoleKey.C:
                        Console.WriteLine("\n************************************");
                        Console.WriteLine("Current Balance: {0:C2}", account.GetBalance());//old balance
                        account.AddInterest(account.GetBalance());//add calculated interest
                        Console.WriteLine("New Balance: {0:C2}", account.GetBalance());//new balance
                        Console.WriteLine("************************************");
                        DisplayMenu();
                        choice = Console.ReadKey();//new user choice for next iteration
                        break;
                    case ConsoleKey.S:
                        Console.WriteLine("\n************************************");
                        Console.WriteLine("Current Balance: {0:C2}", account.GetBalance());
                        Console.WriteLine("************************************");
                        DisplayMenu();
                        choice = Console.ReadKey();//new user choice for next iteration
                        break;
                    default:
                        Console.WriteLine("\n\nXXXXXXXXErrorXXXXXXXX");
                        Console.WriteLine("Menu Selection Error!");//error for incorrect menu choice
                        Console.WriteLine("XXXXXXXXXXXXXXXXXXXXX");
                        DisplayMenu();
                        choice = Console.ReadKey();//new user choice for next iteration
                        break;
                }
            }
            account.ShowTransactions();
            Console.ReadKey();
        }
    }
}
