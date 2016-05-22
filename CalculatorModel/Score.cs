using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samw.Calculator.Model
{

    public class Exercise
    {
        public delegate IEvaluable ExerciseFunc(int num1Max, int num2Max);

        delegate IEvaluable ExerciseFuncWithRange();

        private readonly ExerciseFuncWithRange _func;

        private List<IEvaluable> _expressions;

        public string Name { get; }

        public Exercise(string name, ExerciseFunc func, int num1Max, int num2Max)
        {
            Name = name;
            _func = () =>
            {
                return func(num1Max, num2Max);
            };
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
