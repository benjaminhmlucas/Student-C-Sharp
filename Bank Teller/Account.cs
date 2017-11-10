using System;
/**
 * @author Ben Lucas
 *@File: Account.cs
 *@Purpose: Creates Account objects, handles deposits/withdrawals, adds interest to current balance,
 * can show current balance and retireve balance by iterating through the transList.
 */
namespace Lucas_Program_0
{
    /// <summary>  
    ///  Creates Account objects, handles deposits/withdrawals, adds interest to currentt balance,
    ///  can show current balance and retireve balance by iterating through the transList.
    /// </summary>  
    public class Account
    {
        private double startBalance;//tracks starting balance
        private uint numTransactions;//tracks number of transactions
        private readonly double INTEREST_RATE = 0.0005;//interest rate applied line 39
        private Transaction[] transList { get; }

        public Account() : this(0.0) {
                     
        }
      
        public Account(double startBal) {
            this.startBalance = startBal;
            numTransactions = 0;
            transList = new Transaction[50];
        }
        /// <summary>  
        /// Creates a new deposit transaction and adds it to TransList.  Increments numTransactions to keep track of
        /// how much of array position.
        /// </summary>  
        public void Deposit(double amount) {
            Transaction newTrans = new Transaction(amount,"Deposit");
            transList[numTransactions] = newTrans;
            numTransactions++;
        }
        /// <summary>  
        /// Creates a new withdraw transaction and adds it to TransList.  Increments numTransactions to keep track of
        /// how much of array position.
        /// </summary>
        public void Withdraw(double amount)
        {            
            Transaction newTrans = new Transaction(amount, "Withdraw");
            transList[numTransactions] = newTrans;
            numTransactions++;            
        }
        /// <summary>  
        /// Calculates interest according to a passed in balance. Adds calculated interest transaction to TransList
        /// Increments numTransactions to keep track of array position. If there are more than 50 transactions
        /// errors ar
        /// </summary>
        public void AddInterest(double balance) {
            double amount = balance * INTEREST_RATE;
            Transaction newTrans = new Transaction(amount, "Interest");
            transList[numTransactions] = newTrans;
            numTransactions++;
        }
        /// <summary>  
        /// Takes startBalance and adds all transactions from transList to it.  Method uses tranType to determine
        /// addition or subtraction from the balance.
        /// </summary>
        public double GetBalance() {
            double currentBalance = startBalance;
            foreach (Transaction tran in transList) {
                if (tran != null)
                {
                    if (tran.GetTranType().Equals("Withdraw"))
                    {
                        currentBalance -= tran.GetAmount();
                    }

                    else
                    {
                        currentBalance += tran.GetAmount();
                    } 
                }
            }
            return currentBalance;
        }
        /// <summary>  
        /// Prints the contents of TransList to Console in an orderly fashion
        /// </summary>
        public void ShowTransactions()
        {
            Console.WriteLine("\nDeposit Amount    Transaction Type");
            foreach (Transaction transaction in transList) {
                if (transaction != null)
                {
                    Console.WriteLine("{0,14:C2} {1,19}", transaction.GetAmount(), transaction.GetTranType());
                }
            } 
        }
    }
}
