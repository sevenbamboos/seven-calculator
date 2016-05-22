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

        private List<Exercise> _exercises;
        public void Add(Exercise exe)
        {
            if (_exercises == null)
            {
                _exercises = new List<Exercise>();
            }
            _exercises.Add(exe);
        }

        public void Generate(bool shuffle)
        {

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
