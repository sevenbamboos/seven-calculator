using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using samw.Calculator.Model;

namespace samw.Calculator.Test
{
    [TestClass]
    public class ModelUnitTest
    {
        [TestMethod]
        public void TestNumMax()
        {
            testWithAdd(1000, 100, 200);
            testWithSubtract(1000, 100, 100);
            testWithMultiply(1000, 100, 10);
            testWithDivide(1000, 100, 10);
        }

        private void testWithAdd(int count, int num1Max, int num2Max)
        {
            for (int i = 0; i < count; i++)
            {
                Expression expression = Expression.InitAdd(num1Max, num2Max);
                Assert.AreEqual(Expression.ADD_VALUE, expression.Operator);
                checkMax(expression.LeftNode.Value, expression.RightNode.Value, num1Max, num2Max);
            }
        }

        private void checkMax(decimal value1, decimal value2, int num1Max, int num2Max)
        {
            Assert.IsTrue((value1 < num1Max && value2 < num2Max)
                || (value1 < num2Max && value2 < num1Max));
        }

        private void checkMaxWithOrder(decimal value1, decimal value2, int num1Max, int num2Max)
        {
            Assert.IsTrue((value1 < num1Max && value2 < num2Max));
        }

        private void testWithSubtract(int count, int num1Max, int num2Max)
        {
            for (int i = 0; i < count; i++)
            {
                Expression expression = Expression.InitSubtract(num1Max, num2Max);
                Assert.AreEqual(Expression.SUBTRACT_VALUE, expression.Operator);
                checkMaxWithOrder(expression.LeftNode.Value, expression.RightNode.Value, num1Max, num2Max);
            }
        }

        private void testWithMultiply(int count, int num1Max, int num2Max)
        {
            for (int i = 0; i < count; i++)
            {
                Expression expression = Expression.InitMultiply(num1Max, num2Max);
                Assert.AreEqual(Expression.MULTIPLY_VALUE, expression.Operator);
                checkMax(expression.LeftNode.Value, expression.RightNode.Value, num1Max, num2Max);
            }
        }

        private void testWithDivide(int count, int num1Max, int num2Max)
        {
            for (int i = 0; i < count; i++)
            {
                Expression expression = Expression.InitDivide(num1Max, num2Max);
                Assert.AreEqual(Expression.DIVIDE_VALUE, expression.Operator);
                checkMaxWithOrder(expression.LeftNode.Value, expression.RightNode.Value, num1Max, num2Max);
            }
        }
    }
}
