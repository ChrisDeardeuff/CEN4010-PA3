using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class StreetTest
    {
        [TestMethod]
        public void CanBeMortgagedOneStreet()
        {
            //Test one Street
            var group = new Group(1, 50);
            var street = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            Assert.IsTrue(street.CanBeMortaged());
            street.DevelopProperty(-1);
            Assert.IsFalse(street.CanBeMortaged());
        }

        [TestMethod]
        public void CanBeMortgagedMultipleStreets()
        {
            var group = new Group(3, 50);
            var street = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street1 = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street2 = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            Assert.IsTrue(street.CanBeMortaged());
            Assert.IsTrue(street1.CanBeMortaged());
            Assert.IsTrue(street2.CanBeMortaged());
            street.DevelopProperty(-1);
            street1.DevelopProperty(-1);
            street2.DevelopProperty(-1);
            Assert.IsFalse(street.CanBeMortaged());
            Assert.IsFalse(street1.CanBeMortaged());
            Assert.IsFalse(street2.CanBeMortaged());



        }
    }
}
