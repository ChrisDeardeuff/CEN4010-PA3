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

            //Undeveloped property
            Assert.IsTrue(street.CanBeMortaged());
            Assert.IsTrue(street1.CanBeMortaged());
            Assert.IsTrue(street2.CanBeMortaged());
            
            //1 house on property
            street.DevelopProperty(-1);
            street1.DevelopProperty(-1);
            street2.DevelopProperty(-1);
            Assert.IsFalse(street.CanBeMortaged());
            Assert.IsFalse(street1.CanBeMortaged());
            Assert.IsFalse(street2.CanBeMortaged());



        }

        [TestMethod]
        public void CanBuildTest()
        {
            var player1 = 1;
            var player2 = 2;
            var group = new Group(1, 50);
            var group1 = new Group(3, 50);
            var street = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street1 = new Street(21, group1, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street2 = new Street(21, group1, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street3 = new Street(21, group1, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
           
            //Player owns a monopoly
            street.BoughtByPlayer(player1);
            Assert.IsTrue(street.CanPlayerBuild(player1));
            street1.BoughtByPlayer(player2);
            street2.BoughtByPlayer(player2);
            street3.BoughtByPlayer(player2);
            Assert.IsTrue(street1.CanPlayerBuild(player2));
            Assert.IsTrue(street2.CanPlayerBuild(player2));
            Assert.IsTrue(street3.CanPlayerBuild(player2));

        }

        [TestMethod]
        public void CannotBuildTest()
        {
            var player1 = 1;
            var player2 = 2;
            var group = new Group(3, 50);
            var group1 = new Group(1, 50);
            var street = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street1 = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street2 = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            var street3 = new Street(21, group1, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");
            
            //Player does not have a monopoly
            street.BoughtByPlayer(player1);
            street1.BoughtByPlayer(player1);
            street2.BoughtByPlayer(player2);
            street3.BoughtByPlayer(player1);
            Assert.IsFalse(street.CanPlayerBuild(player1));
            Assert.IsFalse(street.CanPlayerBuild(player2));
            
            //Monopoly belongs to another player
            Assert.IsFalse(street3.CanPlayerBuild(player2));
        }
    }
}
