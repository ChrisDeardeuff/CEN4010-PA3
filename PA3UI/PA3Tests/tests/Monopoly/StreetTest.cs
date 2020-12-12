using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class StreetTest
    {
        /// <summary>
        /// Tests if CanBeMortaged Returns true when the property canbeMortaged (no houses on any property in the group, not mortaged) 
        /// otherwise should return false
        /// </summary>
        [TestMethod]
        public void CanBeMortgagedOneStreet()
        {
            //Test one Street
            var player = 1;
            var group = new Group(1, 50);
            var street = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");

            //Player owns the street
            street.BoughtByPlayer(player);
            group.GetAmountPlayerOwns(player);
           
            Assert.IsTrue(street.CanBeMortaged());
            
            street.DevelopProperty(-1);     //Street already mortgaged

            Assert.IsFalse(street.CanBeMortaged());
        }

        /// <summary>
        /// Tests if CanBeMortaged Returns true when the property canbeMortaged (no houses on any property in the group, not mortaged) 
        /// otherwise should return false
        /// </summary>
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
            
            //Street already mortgaged
            street.DevelopProperty(-1);
            street1.DevelopProperty(-1);
            street2.DevelopProperty(-1);
            Assert.IsFalse(street.CanBeMortaged());
            Assert.IsFalse(street1.CanBeMortaged());
            Assert.IsFalse(street2.CanBeMortaged());

        }

        /// <summary>
        /// test if CanPlayerBuild returns true if all properties in the group are owned by the same player,
        /// else false
        /// </summary>
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

        /// <summary>
        /// test if CanPlayerBuild returns true if all properties in the group are owned by the same player,
        /// else false
        /// </summary>
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
            Assert.IsFalse(street2.CanPlayerBuild(player2));
            
            //Monopoly belongs to another player
            Assert.IsFalse(street3.CanPlayerBuild(player2));

            //street3.DevelopProperty(-1);
            //Assert.IsFalse(street3.CanPlayerBuild(player1));
        }

        /// <summary>
        /// Tests if property returns the right value for getRent,
        /// value is based on development value of the property
        /// </summary>
        [TestMethod]
        public void GetStreetRent()
        {

            var group = new Group(1, 50);
            var street = new Street(21, group, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, "Kentucky Ave");

            Assert.AreEqual(18, street.GetRent());  //Undeveloped
            street.DevelopProperty(1);
            Assert.AreEqual(90, street.GetRent());  //1 house
            street.DevelopProperty(2);
            Assert.AreEqual(250, street.GetRent()); //2 houses
            street.DevelopProperty(3);
            Assert.AreEqual(700, street.GetRent()); //3 houses
            street.DevelopProperty(4);
            Assert.AreEqual(875, street.GetRent()); //4 houses
            street.DevelopProperty(5);
            Assert.AreEqual(1050, street.GetRent());  //Hotel
            street.DevelopProperty(-1);
            Assert.AreEqual(0, street.GetRent());   //Mortgaged street
        }

    }
}
