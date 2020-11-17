using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestFieldsLocations()
        {
            //testing get field at
            var fields = new Fields();
            
            for (int i = 0; i < 40; i++)
            {
                Assert.AreEqual(fields.GetFieldAt(i).GetLocation(), i);
            }
        }
    }
}