using System;
using System.Collections.Generic;
using System.Text;
/**
 * @author Ben Lucas
 *@File: Transactions.cs
 */
namespace Lucas_Program_0
{
    /// <summary>  
    ///  Creates Transaction objects and allows amount and type to be retrieved
    /// </summary>  
    public class Transaction
    {
        private double amount;
        private string tranType;

        public Transaction(double amount, string type) {
            this.amount = amount;
            this.tranType = type;
        }

        public double GetAmount() {
            return amount;
        }
        public string GetTranType()
        {
            return tranType;
        }
    }
}
