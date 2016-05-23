using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samw.Calculator.Model
{

    public class Exam
    {
        public string User { get; set; }
        public DateTime StartTime { get; set; }

        List<Exercise> _exercises;
        public void Add(Exercise exe)
        {
            if (_exercises == null)
            {
                _exercises = new List<Exercise>();
            }
            _exercises.Add(exe);
        }

        public List<IEvaluable> Generate(bool shuffle)
        {
            var evaList = new List<IEvaluable>();
            foreach (Exercise exe in _exercises)
            {
                evaList.AddRange(exe.Generate());
            }
            
            if (shuffle)
            {
                doShuffle(evaList);
            }

            return evaList;
        }

        public static void doShuffle<T>(List<T> lst)
        {
            exchangeRange(lst, 0, lst.Count - 1);
            exchangeRange(lst, 0, (lst.Count - 1) / 2);
            exchangeRange(lst, (lst.Count - 1) / 2 + 1, lst.Count - 1);
        }

        static void exchangeRange<T>(List<T> lst, int i1, int i2)
        {
            reverse(lst, i1, i2);
            reverse(lst, i1, i1 + (i2 - i1) / 2);
            reverse(lst, i1 + (i2 - i1) / 2 + 1, i2);
        }

        static void reverse<T>(List<T> lst, int i1, int i2)
        {
            if (lst == null || lst.Count <= 1)
            {
                return;
            }

            for (int i = 0; i <= (i2-i1)/2; i++)
            {
                exchange(lst, i1 + i, i2 - i);
            }
        }

        static void exchange<T>(List<T> lst, int i, int j)
        {
            //TODO precondition
            if (i < 0 || i >= lst.Count || j < 0 || j >= lst.Count)
            {
                throw new ArgumentException($"Invalid i={i} or j={j}");
            }

            T tmp = lst[i];
            lst[i] = lst[j];
            lst[j] = tmp;
        }
    }

    public class Exercise
    {
        public delegate IEvaluable ExerciseFunc(int num1Max, int num2Max);

        delegate IEvaluable ExerciseFuncWithRange();

        private readonly ExerciseFuncWithRange _func;

        private List<IEvaluable> _expressions;

        private int? _count;

        public string Name { get; set; }

        public int? TotalCount
        {
            get
            {
                return _expressions?.Count();
            }
        }

        public int? CorrectCount
        {
            get
            {
                return (from expression in _expressions
                        where expression.Correct
                        select expression).Count();
            }
        }

        public Exercise(string name, ExerciseFunc func, int num1Max, int num2Max, int? count=null)
        {
            Name = name;
            _count = count;
            _func = () =>
            {
                return func(num1Max, num2Max);
            };
        }

        public List<IEvaluable> Generate()
        {
            if (_count != null)
            {
                return Generate(_count.Value);
            }

            throw new InvalidOperationException("Can't generate without setting count");
        }

        public List<IEvaluable> Generate(int count)
        {

            if (_expressions == null)
            {
                _expressions = new List<IEvaluable>(count);
            }
            else
            {
                _expressions.Clear();
            }

            for (int i = 0; i < count; i++)
            {
                _expressions.Add(_func());
            }

            return _expressions;
        }
    }
}
