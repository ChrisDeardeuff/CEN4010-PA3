using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;


namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class PropertyTest
    {
        [TestMethod]
        public void CanBeMortgagedTest()
        {
            var group = new Group(1, 200);
            Property property = new Street(37, group, 350, new int[] { 35, 175, 500, 1100, 1300, 1500 }, "Park Place");
            Assert.IsTrue(property.CanBeMortaged());
            property.DevelopProperty(-1);
            Assert.IsFalse(property.CanBeMortaged());
            property.DevelopProperty(1);
            Assert.IsFalse(property.CanBeMortaged());
        }
    }
}
