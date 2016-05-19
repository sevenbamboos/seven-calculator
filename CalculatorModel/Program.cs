using samw.Calculator.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samw.Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                IEvaluable exp1 = Expression.InitAdd(100, 100);
                Console.WriteLine(exp1 + " = " + exp1.Value);
            }

            Console.ReadKey();
        }
    }
}
