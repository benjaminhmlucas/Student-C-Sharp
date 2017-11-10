using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucasProgram3_Multi_Length {
    class Bank {
        public static void Main(string[] args) {
            // shortest time to service a customer; must be greater than zero
            int MINIMUM_DURATION = 1;

            // longest time to service a customer; must be greater than the minimum duration
            int MAXIMUM_DURATION = 5;

            // average customers arriving per given minute.  This would mean three customers every four minutes.
            double CUST_PER_MINUTE = 5;

            // how long the simulation represents; 120 would equal two hours
            int SIMULATION_DURATION = 120;

            // The number of tellers for this run of the simulation
            int NUM_TELLERS = 10;

            int maxWaitTime = 0;
            int maxIdleTime = 0;

            double totalWaitTime = 0;
            double totalIdleTime = 0;
            double avgIdleTime = 0;
            double avgWaitTime = 0;

            int totalCustomersServed = 0;
            int totalDuration = 0;            
            
            List<Queue<Customer>> QList = new List<Queue<Customer>>(NUM_TELLERS);
            for (int i = 0;i < NUM_TELLERS;i++) {
                QList.Add(new Queue<Customer>());
            }
            
            //represents queue with least amount of customers
            Queue<Customer> shortQueue = QList[0];

            // Create the customer generator
            CustomerGenerator frontdoor = new CustomerGenerator(MINIMUM_DURATION, MAXIMUM_DURATION, CUST_PER_MINUTE, SIMULATION_DURATION, 97);
                        
            List<Teller> tellers = new List<Teller>(NUM_TELLERS);
            for (int i = 0; i < NUM_TELLERS; i++) {
                tellers.Add(new Teller(QList[i]));
            }
            
            for (int timeSlot = 0; timeSlot < SIMULATION_DURATION || Teller.TotalCustomersInQueue > 0; timeSlot++) {
                // get the queue of arriving customers from the frontdoor
                
                foreach (Queue<Customer> q in QList) {
                    if (q.Count < shortQueue.Count) {
                        shortQueue = q;
                    }
                }                
                Queue<Customer> arrivals = frontdoor.GetCustomers(timeSlot);
                while (arrivals.Count > 0) {
                    Customer cust = arrivals.Dequeue();
                    shortQueue.Enqueue(cust);
                    Teller.TotalCustomersInQueue++;
                }
                foreach (Teller teller in tellers) {
                    teller.ProcessNextCustomer(timeSlot);
                }
                totalDuration++;
            }
            
            Console.WriteLine("Minimum Duration: {0}", MINIMUM_DURATION);
            Console.WriteLine("Maximum Duration: {0}", MAXIMUM_DURATION);
            Console.WriteLine("Customer Per Minute: {0}", CUST_PER_MINUTE);
            Console.WriteLine("Simulation Duration: {0}", SIMULATION_DURATION);
            Console.WriteLine("Number of Tellers: {0}\n", NUM_TELLERS);

            foreach (Teller teller in tellers) {
                totalCustomersServed += teller.CustomersServed;
                if (maxWaitTime < teller.MaxWaitTime) {//set highest customer wait time
                    maxWaitTime = teller.MaxWaitTime;
                }
                if (maxIdleTime < teller.IdleTime) {//set higherst teller idle time
                    maxIdleTime = teller.IdleTime;
                }
                totalWaitTime += teller.TotalWaitTime;//add all wait times from line for all tellers for all customers
                totalIdleTime += teller.IdleTime;//add all time idle for all tellers
            }
            avgWaitTime = totalWaitTime / totalCustomersServed;//calculate average wait time for customers
            avgIdleTime = totalIdleTime / tellers.Count;//calculate average idle time for tellers
            Console.WriteLine("Maximum Wait Time: {0}", maxWaitTime);
            Console.WriteLine("Average Wait Time: {0:F}", avgWaitTime);
            Console.WriteLine("Maximum Idle Time: {0}", maxIdleTime);
            Console.WriteLine("Average Idle Time: {0:F}", avgIdleTime);
            Console.WriteLine("Customers Served: {0}", totalCustomersServed);
            Console.WriteLine("Total Duration: {0}", totalDuration);
       
            Console.ReadKey();
        }
    }
}
