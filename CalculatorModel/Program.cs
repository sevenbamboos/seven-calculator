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
            var lst = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };

            Exam.doShuffle(lst);
            foreach (var item in lst)
            {
                Console.Write($"{item},");
            }

            Console.ReadKey();
        }
    }
}
