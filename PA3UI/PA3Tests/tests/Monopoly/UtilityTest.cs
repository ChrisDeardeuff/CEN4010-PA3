using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;


namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void CanPlayerBuildTest()
        {
            var group = new Group(2);
            var ec = new Utility(12, group, 150, "Electric Company");
            var ww = new Utility(28, group, 150, "Water Works");
            var player1 = 1;
            Assert.IsFalse(ec.CanPlayerBuild(player1));
            Assert.IsFalse(ww.CanPlayerBuild(player1));
        }

        [TestMethod]
        public void CanMortgageTest()
        {
            var group = new Group(2);
            var ec = new Utility(12, group, 150, "Electric Company");
            var ww = new Utility(28, group, 150, "Water Works");
            ec.DevelopProperty(-1);
            ww.DevelopProperty(0);
            Assert.IsFalse(ec.CanBeMortaged());
            Assert.IsTrue(ww.CanBeMortaged());
        }
    }
}
