using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSAQueue
{
    public class Teller
    {
        // our quueue of customers
        private Queue<Customer> custQ;
        
        // the tick of the clock when the teller becomes free
        private int nextFree = 0;

        // the amount of time the teller is idle
        public int IdleTime { get; private set; }

        // the number of customers served
        public int CustomersServed { get; private set; }

        // the longest time that one of this teller's customers has waited
        public int MaxWaitTime { get; private set; }

        // the total amount of time that all of this teller's customers have waited
        public int TotalWaitTime { get; private set; }
        
        public int tellerCustomersInQueue { get; private set; }

        // Constructor accepts the queue of customers from which to pull the next customer
        // There are other ways to do this, but this makes part B less painful
        public Teller(Queue<Customer> q) {
            tellerCustomersInQueue = 0;
            custQ = q;
        }

        /// <summary>
        /// Begins helping a customer if the teller is not busy AND a customer is waiting
        /// </summary>
        /// <param name="now">The time on the bank's clock, in minutes from the start, i.e. timeslot.</param>
        public void ProcessNextCustomer(int now) {
            // busy?            
            if (now >= nextFree) {
                
                if (!(custQ.Count <= 0)) {
                    Customer currentCustomer = custQ.Dequeue();
                    currentCustomer.ServiceTime = now - currentCustomer.ArrivalTime;
                    if (MaxWaitTime < currentCustomer.ServiceTime) {
                        MaxWaitTime = currentCustomer.ServiceTime;
                    }
                    nextFree = currentCustomer.TransactionDuration + now;
                    TotalWaitTime += currentCustomer.ServiceTime;
                    tellerCustomersInQueue--;
                    CustomersServed++;
                }
                else {
                    //increment the idle time
                    IdleTime++;
                }

            }
            
        }
    }
}

