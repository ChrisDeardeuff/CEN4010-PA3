using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class PlayerTest
    {
        /// <summary>
        /// This test tests the moveForward method by moving a player 
        /// forward and checking if he is standing at the right location
        /// </summary>
        [TestMethod]
        public void MoveTest()
        {
            var player = new Player();
            //test moving player forward
            player.movePlayerForward(2);
            Assert.AreEqual(2, player.position);
        }
        /// <summary>
        /// This test tests the hasEnoughMoney method, by using one valuethat is smaller than the balance of the player,
        /// and one that is bigger than the balance of the player
        /// </summary>
        [TestMethod]
        public void HasEnoughMoneyTest()
        {
            var player = new Player();
            //test has enough money (balance is 1500)
            Assert.AreEqual(false, player.HasEnoughMoney(2000));
            Assert.AreEqual(true, player.HasEnoughMoney(160));
        }

        /// <summary>
        /// money is added to a player to test AddBalance
        /// </summary>
        [TestMethod]
        public void AddBalanceTest()
        {
            var player = new Player();
            Assert.AreEqual(1500, player.balance);
            //test adding balance
            player.addBalance(40);
            Assert.AreEqual(1540, player.balance);
        }

        /// <summary>
        /// money is subtracted from a player to test removeBalance balance 
        /// is checked after subtract balance
        /// </summary>
        [TestMethod]
        public void SubtractTest()
        {
            var player = new Player();
            Assert.AreEqual(1500, player.balance);
            //test subtracting balance
            player.subtractBalance(100);
            Assert.AreEqual(1400, player.balance);
        }

        /// <summary>
        /// This test tests if the goToJail method works, by sending a player to jail,
        /// and then checking if he is in jail
        /// </summary>
        [TestMethod]
        public void GoToJailTest()
        {
            var player = new Player();
            player.goToJail();
            Assert.IsTrue(player.inPrison);
            Assert.AreEqual(10, player.position);
            player.UpdateInPrisonCounter();
            Assert.AreEqual(1, player.inPrisonCounter);
        }

        /// <summary>
        /// Tests the GetOutOfJailMethod, by putting a player
        /// in prison, and then calling GetOutOfJail
        /// </summary>
        [TestMethod]
        public void GetOutOfJailTest()
        {
            var player = new Player();
            player.goToJail();
            player.UpdateInPrisonCounter();
            player.GetOutOfJail();
            Assert.AreEqual(false, player.inPrison);
            Assert.AreEqual(0,player.inPrisonCounter);
        }

        /// <summary>
        /// This test checks if the prisonCounter is updated 
        /// whenever UpdateInPrisonCounter is called
        /// </summary>
        [TestMethod]
        public void PrisonCounterTest()
        {
            var player = new Player();
            //test prison counter with 0 prisoners
            Assert.AreEqual(0, player.inPrisonCounter);
            //test prison counter with 1 prisoner
            player.goToJail();
            player.UpdateInPrisonCounter();
            Assert.AreEqual(1, player.inPrisonCounter);
        }

        /// <summary>
        /// This test tests if the player gets 200$ when passing go,
        /// by letting a player move 45 places forward
        /// </summary>
        [TestMethod]
        public void GoTest()
        {
            var player = new Player();
            //test passing go
            player.movePlayerForward(45);
            Assert.AreEqual(1700, player.balance);
        }

        /// <summary>
        /// 3 properties are created and added to a player,
        /// then Getproperties is tested by checking if its output contains
        /// the 3 properties
        /// </summary>
        [TestMethod]

        public void GetPropertiesTest()
        {
            var player = new Player();
            var street = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var railroad = new Railroads(15, new Group(4), 200, "Pennsylvania Railroad");
            var utility = new Utility(28, new Group(2), 150, "Water Works");
            //test list from get properties owned
            player.addProperty(street);
            player.addProperty(railroad);
            player.addProperty(utility);
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(street));
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(railroad));
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(utility));
            player.removeProperty(railroad);
            player.removeProperty(street);
            player.removeProperty(utility);
            Assert.AreEqual(false, player.getPropertiesOwned().Contains(street));
            Assert.AreEqual(false, player.getPropertiesOwned().Contains(railroad));
            Assert.AreEqual(false, player.getPropertiesOwned().Contains(utility));
        }

        /// <summary>
        /// 4 properties are added to one player inorder to test AddProperty
        /// </summary>
        [TestMethod]
        public void AddPropertyTest()
        {
            var player = new Player();
            var street = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var railroad = new Railroads(15, new Group(4), 200, "Pennsylvania Railroad");
            var utility = new Utility(28, new Group(2), 150, "Water Works");
            var utility1 = new Utility(12, new Group(2), 150, "Water Works");
            player.addProperty(street);
            player.addProperty(railroad);
            player.addProperty(utility);
            player.addProperty(utility1);
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(street));
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(railroad));
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(utility));
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(utility1));
        }

        /// <summary>
        /// 4 properties are added to one player and than removed from the 
        /// player to check the removeProperty method
        /// </summary>
        [TestMethod]
        public void RemovePropertyTest()
        {
            //test remove property
            var player = new Player();
            var street = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var railroad = new Railroads(15, new Group(4), 200, "Pennsylvania Railroad");
            var utility = new Utility(28, new Group(2), 150, "Water Works");
            player.addProperty(street);
            player.addProperty(railroad);
            player.addProperty(utility);
            player.removeProperty(railroad);
            player.removeProperty(street);
            player.removeProperty(utility);
            Assert.AreEqual(false, player.getPropertiesOwned().Contains(street));
            Assert.AreEqual(false, player.getPropertiesOwned().Contains(railroad));
            Assert.AreEqual(false, player.getPropertiesOwned().Contains(utility));
        }

        /// <summary>
        /// two players and 3 streets are created and added to the players,
        /// than the score of each player is Calculated
        /// </summary>
        [TestMethod]
        public void CalculateScoreTest()
        {
            var player = new Player();
            var player2 = new Player();
            
            var street1 = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var street2 = new Street(8, new Group(2,50), 100, new int[] { 6, 30, 90, 270, 400, 550 }, "Vermont Ave");
            var street3 = new Street(9, new Group(2,50), 120, new int[] { 8, 40, 100, 300, 450, 600 }, "Connecticut Ave");

            street2.DevelopProperty(3);
            street3.DevelopProperty(-1);

            player.addProperty(street1);
            player.addProperty(street3);
            player2.addProperty(street2);
            Assert.AreEqual(1490, player.CalculateScore());
            Assert.AreEqual(1700, player2.CalculateScore());
        }
    }
}