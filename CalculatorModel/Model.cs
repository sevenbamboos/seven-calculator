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

    public class Expression : IEvaluable
    {
        IEvaluable LeftNode { get; set; }
        IEvaluable RightNode { get; set; }
        Func<decimal, decimal, decimal> Operator { get; set; }

        public override string ToString() => $"{LeftNode} {OperatorStringMap[Operator]} {RightNode}";

        decimal IEvaluable.Value
        {
            get
            {
                //TODO precondition for left/right node and operator
                return Operator(LeftNode.Value, RightNode.Value);
            }
        }

        static Func<decimal, decimal, decimal> Add = (left, right) => left + right;
        static Func<decimal, decimal, decimal> Subtract = (left, right) => left - right;
        static Func<decimal, decimal, decimal> Multiply = (left, right) => left * right;
        static Func<decimal, decimal, decimal> Divide = (left, right) => left / right;

        //TODO how to avoid the repeative type declaration?
        static Dictionary<Func<decimal, decimal, decimal>, string> OperatorStringMap = new Dictionary<Func<decimal, decimal, decimal>, string>()
        {
            { Add, "+" },
            { Subtract, "-" },
            { Multiply, "x" },
            { Divide, "/" }
        };

        public static IEvaluable InitAdd(int num1Max, int num2Max)
        {
            int num1 = random(num1Max), num2 = random(num2Max);
            if (random())
            {
                exchange(ref num1, ref num2);
            }
            return Init(num1, num2, Add);
        }

        public static IEvaluable InitMultiply(int num1Max, int num2Max)
        {
            int num1 = random(num1Max), num2 = random(num2Max);
            if (random())
            {
                exchange(ref num1, ref num2);
            }
            return Init(num1, num2, Multiply);
        }

        static void exchange(ref int i1, ref int i2)
        {
            int tmp = i1;
            i1 = i2;
            i2 = tmp;
        }

        static bool random()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0xFFFF);
            return rnd.Next() % 2 == 0;
        }

        static int random(int max)
        {
            Random rnd = new Random((int) DateTime.Now.Ticks & 0xFFFF);
            return rnd.Next(max);
        }

        static IEvaluable Init(decimal num1, decimal num2, Func<decimal, decimal, decimal> op)
        {
            return Init(new SingleValueNode(num1), new SingleValueNode(num2), op);
        }

        static IEvaluable Init(IEvaluable node1, IEvaluable node2, Func<decimal, decimal, decimal> op)
        {
            return new Expression
            {
                LeftNode = node1,
                RightNode = node2,
                Operator = op
            };
        }
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
}
