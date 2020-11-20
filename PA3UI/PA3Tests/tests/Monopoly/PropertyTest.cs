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

        [TestMethod]
        public void MultipleMortgageTest()
        {
            var group = new Group(3, 300);
            Property property = new Street(31, group, 300, new int[] { 26, 130, 390, 900, 1100, 1275 }, "Pacific Ave");
            Property property1 = new Street(32, group, 300, new int[] { 26, 130, 390, 900, 1100, 1275 }, "North Carolina Ave");
            Property property2 = new Street(34, group, 320, new int[] { 28, 150, 390, 900, 1200, 1400 }, "Pennsylvania Ave");
            Assert.IsTrue(property1.CanBeMortaged());
            property2.DevelopProperty(2);
            property.DevelopProperty(-1);
            Assert.IsFalse(property2.CanBeMortaged());
            Assert.IsFalse(property.CanBeMortaged());
        }
    }
}
