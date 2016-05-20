using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samw.Calculator.model
{

    public interface IEvaluable
    {
        decimal Value { get; }
    }

    public sealed class SingleValueNode : IEvaluable
    {
        private readonly decimal _value;

        public SingleValueNode(decimal value)
        {
            _value = value;
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
        IEvaluable LeftNode { get; set; }
        IEvaluable RightNode { get; set; }
        Func<decimal, decimal, decimal> Operator { get; set; }

        public override string ToString() => 
            $"{LeftNode} {OperatorStringMap[Operator]} {RightNode} = {((IEvaluable) this).Value}";

        decimal IEvaluable.Value
        {
            get
            {
                //TODO precondition
                return Operator(LeftNode.Value, RightNode.Value);
            }
        }

        static Func<decimal, decimal, decimal> Add = (left, right) => left + right;
        static Func<decimal, decimal, decimal> Subtract = (left, right) => left - right;
        static Func<decimal, decimal, decimal> Multiply = (left, right) => left * right;
        static Func<decimal, decimal, decimal> Divide = (left, right) => left / right;

        //TODO how to avoid the repeative type declaration?
        static Dictionary<Func<decimal, decimal, decimal>, string> OperatorStringMap 
            = new Dictionary<Func<decimal, decimal, decimal>, string>()
        {
            { Add, "+" },
            { Subtract, "-" },
            { Multiply, "x" },
            { Divide, "/" }
        };

        public static IEvaluable InitAdd(int num1Max, int num2Max)
        {
            int num1, num2;
            random(num1Max, num2Max, out num1, out num2);
            return Init(num1, num2, Add);
        }

        public static IEvaluable InitMultiply(int num1Max, int num2Max)
        {
            int num1, num2;
            random(num1Max, num2Max, out num1, out num2);
            return Init(num1, num2, Multiply);
        }

        public static IEvaluable InitSubtract(int num1Max, int num2Max)
        {
            if (num1Max < num2Max)
            {
                exchange(ref num1Max, ref num2Max);
            }

            int num1, num2;
            randomWithOrder(num1Max, num2Max, out num1, out num2);
            return Init(num1, num2, Subtract);
        }

        #region helper methods

        static void exchange(ref int i1, ref int i2)
        {
            int tmp = i1;
            i1 = i2;
            i2 = tmp;
        }

        static bool random()
        {
            return getRandom().Next() % 2 == 0;
        }

        static void randomWithOrder(int num1Max, int num2Max, out int num1, out int num2)
        {
            if (num1Max < num2Max)
            {
                throw new ArgumentException($"num1Max param {num1Max} failed to be greater than num2Max {num2Max}");
            }

            if (num1Max == num2Max)
            {
                num1 = random(num1Max, 1);
            }
            else
            {
                num1 = random(num1Max, num2Max);
            }
            
            num2 = random(num2Max, 1);

            if (num1 < num2)
            {
                exchange(ref num1, ref num2);
            }
        }

        static void random(int num1Max, int num2Max, out int num1, out int num2)
        {
            num1 = random(num1Max, 1);
            num2 = random(num2Max, 1);
            if (random())
            {
                exchange(ref num1, ref num2);
            }
        }

        static int random(int max, int min)
        {
            return randomFuncWithMin(min)(max);
        }

        delegate int RandomFunc(int max);

        static RandomFunc randomFuncWithMin(int min)
        {
            return max =>
            {
                //TODO precondition
                if (max < min)
                {
                    throw new ArgumentException($"max param {max} failed to be greater than min {min}");
                }

                int result = random(max - min);
                return result + min;
            };
        }

        static int random(int max)
        {
            return getRandom().Next(max);
        }

        static Random rnd = null;

        static Random getRandom()
        {
            rnd = rnd ?? new Random((int)DateTime.Now.Ticks & 0xFFFF);
            return rnd;
        }

        static IEvaluable Init(decimal num1, decimal num2, Func<decimal, decimal, decimal> op)
        {
            return Init(new SingleValueNode(num1), new SingleValueNode(num2), op);
        }

        static IEvaluable Init(IEvaluable node1, IEvaluable node2, 
            Func<decimal, decimal, decimal> op)
        {
            return new Expression
            {
                LeftNode = node1,
                RightNode = node2,
                Operator = op
            };
        }

        #endregion
    }

}