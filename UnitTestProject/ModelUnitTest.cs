using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using samw.Calculator.Model;

namespace samw.Calculator.Test
{
    [TestClass]
    public class ModelUnitTest
    {
        [TestMethod]
        public void TestAdd()
        {
            Expression e = (Expression) Expression.InitAdd(10, 10);
            Console.WriteLine(e);
        }
    }
}
