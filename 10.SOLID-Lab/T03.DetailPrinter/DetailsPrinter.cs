using System;
using System.Collections.Generic;
using T03.DetailPrinter.Contracts;

namespace T03.DetailPrinter
{
    public class DetailsPrinter
    {
        private readonly IList<IEmployee> employees;

        public DetailsPrinter(IList<IEmployee> employees)
        {
            this.employees = employees;
        }

        public void PrintDetails()
        {
            foreach (IEmployee employee in employees)
            {
                Console.WriteLine(employee);
            }
        }
    }
}
