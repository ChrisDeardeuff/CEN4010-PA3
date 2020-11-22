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

            //Cannot build on a utility
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
            
            Assert.IsFalse(ec.CanBeMortaged()); //Utility is already mortgaged
            Assert.IsTrue(ww.CanBeMortaged()); //Unmortgaged utility
        }

        [TestMethod]
        public void GetRentTest()
        {
            var player = 1;
            var player1 = 2;
            var group = new Group(2);
            var ec = new Utility(12, group, 150, "Electric Company");
            var ww = new Utility(28, group, 150, "Water Works");
            

            //Each player owns only one utility (rent*4)
            ec.BoughtByPlayer(player1);
            ww.BoughtByPlayer(player);
            ec.DevelopProperty(-1);     //Utility has been mortgaged

            Assert.AreEqual(16, ww.GetRent(4));
            Assert.AreEqual(48, ww.GetRent(12));
            Assert.AreEqual(0, ec.GetRent(6));

            //A player owns both utilities (rent*10)
            ec.BoughtByPlayer(player);
            ww.BoughtByPlayer(player);
            ec.DevelopProperty(0);      //Utility has been unmortgaged

            Assert.AreEqual(50, ww.GetRent(5));
            Assert.AreEqual(100, ec.GetRent(10));

            ec.DevelopProperty(-1);     //Utility has been mortgaged

            Assert.AreEqual(0, ec.GetRent(7));
            Assert.AreEqual(70, ww.GetRent(7));
            
        }
    }
}
