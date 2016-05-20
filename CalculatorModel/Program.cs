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
        delegate IEvaluable Evaluate(int num1Max, int num2Max); 

        static void Main(string[] args)
        {
            testWith(10, Expression.InitAdd, 10, 10);

            testWith(10, Expression.InitAdd, 100, 100);

            testWith(10, Expression.InitSubtract, 100, 10);

            testWith(10, Expression.InitSubtract, 100, 100);

            testWith(10, Expression.InitMultiply, 10, 10);

            testWith(10, Expression.InitMultiply, 100, 100);

            Console.ReadKey();
        }

        static void testWith(int count, Evaluate evaluate, int num1Max, int num2Max)
        {
            for (int i = 0; i < count; i++)
            {
                IEvaluable result = evaluate(num1Max, num2Max);
                Console.WriteLine(result);
            }
            Console.WriteLine();
        }
    }
}
