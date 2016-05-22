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
            Exercise exe = new Exercise("add less than 100", Expression.InitAdd, 100, 100);
            foreach (IEvaluable eva in exe.Generate(100))
            {
                eva.Answer = 100;
                Console.WriteLine($"{eva} and your answer is {eva.Answer}");
            }

            Console.WriteLine($"You made {exe.CorrectCount} out of {exe.TotalCount}. Exercise {exe.Name}'s score is {100 * exe.CorrectCount / exe.TotalCount}");

            Console.ReadKey();
        }
    }
}
