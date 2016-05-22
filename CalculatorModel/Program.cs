using samw.Calculator.Model;
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
            Expression exp = Expression.InitAdd(100, 100);
            decimal result = exp.LeftNode.Value + exp.RightNode.Value;
            Console.WriteLine(result == ((IEvaluable)exp).Value);
            Console.ReadKey();
        }
    }
}
