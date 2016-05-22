using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samw.Calculator.Model
{

    public interface IEvaluable
    {
        decimal Value { get; }

        bool Correct { get; }

        decimal Answer { get; set; }
    }

    public sealed class SingleValueNode : IEvaluable
    {
        private readonly decimal _value;

        public SingleValueNode(decimal value)
        {
            _value = value;
        }

        decimal IEvaluable.Answer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        bool IEvaluable.Correct
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        decimal IEvaluable.Value
        {
            get
            {
                return _value;
            }
        }

        public override string ToString() => _value.ToString();
    }

    public class Expression : IEvaluable
    {
        public IEvaluable LeftNode { get; set; } 
        public IEvaluable RightNode { get; set; }
        Func<decimal, decimal, decimal> _operator { get; set; }

        private decimal _answer;

        public string Operator => OperatorStringMap[_operator];

        public override string ToString() => 
            $"{LeftNode} {Operator} {RightNode} = {((IEvaluable) this).Value}";

        decimal IEvaluable.Value
        {
            get
            {
                //TODO precondition
                return _operator(LeftNode.Value, RightNode.Value);
            }
        }

        bool IEvaluable.Correct
        {
            get
            {
                return ((IEvaluable)this).Value == _answer;
            }
        }

        decimal IEvaluable.Answer
        {
            get
            {
                return _answer;
            }

            set
            {
                _answer = value;
            }
        }

        static Func<decimal, decimal, decimal> Add = (left, right) => left + right;
        static Func<decimal, decimal, decimal> Subtract = (left, right) => left - right;
        static Func<decimal, decimal, decimal> Multiply = (left, right) => left * right;
        static Func<decimal, decimal, decimal> Divide = (left, right) => left / right;

        public const string ADD_VALUE = "+";
        public const string SUBTRACT_VALUE = "-";
        public const string MULTIPLY_VALUE = "x";
        public const string DIVIDE_VALUE = "/";

        //TODO how to avoid the repeative type declaration?
        static Dictionary<Func<decimal, decimal, decimal>, string> OperatorStringMap 
            = new Dictionary<Func<decimal, decimal, decimal>, string>()
        {
            { Add, ADD_VALUE },
            { Subtract, SUBTRACT_VALUE },
            { Multiply, MULTIPLY_VALUE },
            { Divide, DIVIDE_VALUE }
        };

        public static Expression InitAdd(int num1Max, int num2Max)
        {
            int num1, num2;
            random(num1Max, num2Max, out num1, out num2);
            return init(num1, num2, Add);
        }

        public static Expression InitMultiply(int num1Max, int num2Max)
        {
            int num1, num2;
            random(num1Max, num2Max, out num1, out num2);
            return init(num1, num2, Multiply);
        }

        public static Expression InitSubtract(int num1Max, int num2Max)
        {
            if (num1Max < num2Max)
            {
                exchange(ref num1Max, ref num2Max);
            }

            int num1, num2;
            if (num1Max == num2Max)
            {
                num1 = randomRange(num1Max, 1);
            }
            else
            {
                num1 = randomRange(num1Max, num2Max);
            }

            num2 = randomRange(num2Max, 1);

            if (num1 < num2)
            {
                exchange(ref num1, ref num2);
            }
            return init(num1, num2, Subtract);
        }

        public static Expression InitDivide(int num1Max, int num2Max)
        {
            if (num1Max < num2Max)
            {
                exchange(ref num1Max, ref num2Max);
            }

            int num2 = randomRange(num2Max, 1);
            int resultMax = num1Max / num2;
            int result = randomRange(resultMax, 1);
            return init(num2 * result, num2, Divide);
        }

        #region helper methods

        static void exchange(ref int i1, ref int i2)
        {
            int tmp = i1;
            i1 = i2;
            i2 = tmp;
        }

        static bool randomBool()
        {
            return getRandom().Next() % 2 == 0;
        }

        static void random(int num1Max, int num2Max, out int num1, out int num2)
        {
            num1 = randomRange(num1Max, 1);
            num2 = randomRange(num2Max, 1);
            if (randomBool())
            {
                exchange(ref num1, ref num2);
            }
        }

        delegate int RandomRangeDelegate(int max, int min);

        static RandomRangeDelegate randomRange = createRandomRangeDelegate();

        static RandomRangeDelegate createRandomRangeDelegate()
        {

            return (max, min) =>
            {
                //TODO precondition
                if (max < min)
                {
                    throw new ArgumentException(
                        $"max param {max} failed to be greater than min {min}");
                }

                int result = randomInt(max - min);
                return result + min;
            };
        }

        static int randomInt(int max)
        {
            return getRandom().Next(max);
        }

        static Random rnd = null;

        static Random getRandom()
        {
            rnd = rnd ?? new Random((int)DateTime.Now.Ticks & 0xFFFF);
            return rnd;
        }

        static Expression init(decimal num1, decimal num2, Func<decimal, decimal, decimal> op)
        {
            return init(new SingleValueNode(num1), new SingleValueNode(num2), op);
        }

        static Expression init(IEvaluable node1, IEvaluable node2, 
            Func<decimal, decimal, decimal> op)
        {
            return new Expression
            {
                LeftNode = node1,
                RightNode = node2,
                _operator = op
            };
        }

        #endregion
    }

}