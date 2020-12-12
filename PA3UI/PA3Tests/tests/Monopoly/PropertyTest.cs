using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;


namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class PropertyTest
    {
        /// <summary>
        /// Tests if CanBeMortaged Returns true when the property canbeMortaged (no houses on any property in the group, not mortaged)
        /// otherwise should return false
        /// </summary>
        [TestMethod]
        public void CanBeMortgagedTest()
        {
            var group = new Group(2, 200);
            Property property = new Street(37, group, 350, new int[] { 35, 175, 500, 1100, 1300, 1500 }, "Park Place");
            Property property1 = new Street(37, group, 350, new int[] { 35, 175, 500, 1100, 1300, 1500 }, "Park Place");
            Assert.IsTrue(property.CanBeMortaged()); //Undeveloped property
            property.DevelopProperty(-1);
            Assert.IsFalse(property.CanBeMortaged()); //Already mortgaged
            property.DevelopProperty(1);
            Assert.IsFalse(property.CanBeMortaged()); //1 house on property
            property.DevelopProperty(0);
            property1.DevelopProperty(1);
            Assert.IsFalse(property.CanBeMortaged()); //1 house on property
        }

        /// <summary>
        /// Test that works just like CanBeMortgaged but with multiple properties
        /// </summary>
        [TestMethod]
        public void MultipleMortgageTest()
        {
            var group = new Group(3, 300);
            Property property = new Street(31, group, 300, new int[] { 26, 130, 390, 900, 1100, 1275 }, "Pacific Ave");
            Property property1 = new Street(32, group, 300, new int[] { 26, 130, 390, 900, 1100, 1275 }, "North Carolina Ave");
            Property property2 = new Street(34, group, 320, new int[] { 28, 150, 390, 900, 1200, 1400 }, "Pennsylvania Ave");
            Assert.IsTrue(property1.CanBeMortaged()); //Undeveloped property
            property2.DevelopProperty(2);
            property.DevelopProperty(-1);
            Assert.IsFalse(property2.CanBeMortaged()); //2 houses on property
            Assert.IsFalse(property.CanBeMortaged()); //Already mortgaged
        }

        /// <summary>
        /// Tests if getAction returns the right value after the property has been bought (should be Action.payRent)
        /// </summary>
        [TestMethod]
        public void GetActionTest()
        {
            var unowned = -1;
            var owner = 2;
            var group = new Group(1, 50);
            Property property = new Street(1, group, 60, new int[] { 2, 10, 30, 90, 160, 250 }, "Mediterranean Ave");
            
            //Property is not owned, can be bought
            property.BoughtByPlayer(unowned);
            Assert.AreEqual(Actions.canBuy, property.GetAction());

            //Property is owned, must pay rent
            property.BoughtByPlayer(owner);
            Assert.AreEqual(Actions.payRent, property.GetAction());
        }

        /// <summary>
        /// Tests if Bought by player sets the owner of the property, and returns the price of the property
        /// </summary>
        [TestMethod]
        public void BoughtByPlayerTest()
        {
            var player = 1;
            var group = new Group(1, 50);
            Property property = new Street(1, group, 60, new int[] { 2, 10, 30, 90, 160, 250 }, "Mediterranean Ave");
            Assert.AreEqual(60, property.BoughtByPlayer(player)); //player purchased property
            Assert.AreEqual(player, property.owner);     //Player owns the property
        }

        /// <summary>
        /// Tests if Develop Property develops the property to the right value
        /// </summary>
        [TestMethod]
        public void DevelopPropertyTest()
        {
            var group = new Group(1, 50);
            Property property = new Street(1, group, 60, new int[] { 2, 10, 30, 90, 160, 250 }, "Mediterranean Ave");

            property.DevelopProperty(2);
            Assert.AreEqual(2, property.developmentValue);
        }

        /// <summary>
        /// Tests EnoughHousesAndHotelsAvailable by first inserting values and houses available (should return true),
        /// and then by inserting values that are not available (should return false)
        /// </summary>
        [TestMethod]
        public void TestEnoughHousesAndHotelsAvailable() 
        {
            Assert.IsTrue(Street.EnoughHousesAndHotelsAvailable(1, 2));
            Assert.IsTrue(Street.EnoughHousesAndHotelsAvailable(3, 0));
            Assert.IsFalse(Street.EnoughHousesAndHotelsAvailable(35, 0));
            Assert.IsFalse(Street.EnoughHousesAndHotelsAvailable(0, 35));
        }
    }
}
