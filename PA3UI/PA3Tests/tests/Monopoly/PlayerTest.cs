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
        [TestMethod]
        public void MoveTest()
        {
            var player = new Player();
            //test moving player forward
            player.movePlayerForward(2);
            Assert.AreEqual(2, player.position);
        }

        [TestMethod]
        public void HasEnoughTest()
        {
            var player = new Player();
            //test has enough money (balance is 1500)
            Assert.AreEqual(false, player.HasEnoughMoney(2000));
            Assert.AreEqual(true, player.HasEnoughMoney(160));
        }

        [TestMethod]
        public void AddTest()
        {
            var player = new Player();
            Assert.AreEqual(1500, player.balance);
            //test adding balance
            player.addBalance(40);
            Assert.AreEqual(1540, player.balance);
        }
        [TestMethod]
        public void SubtractTest()
        {
            var player = new Player();
            Assert.AreEqual(1500, player.balance);
            //test subtracting balance
            player.subtractBalance(100);
            Assert.AreEqual(1400, player.balance);
        }
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

        [TestMethod]
        public void GetOutOfJailTest()
        {
            var player = new Player();
            player.goToJail();
            player.UpdateInPrisonCounter();
            player.GetOutOfJail();
            player.UpdateInPrisonCounter();
            Assert.AreEqual(false, player.inPrison);
            Assert.AreEqual(0,player.inPrisonCounter);
        }

        [TestMethod]
        public void PrisonCounterTest()
        {
            var player = new Player();
            //test prison counter with 0 prisoners
            player.UpdateInPrisonCounter();
            Assert.AreEqual(0, player.inPrisonCounter);
            //test prison counter with 1 prisoner
            player.goToJail();
            Assert.AreEqual(1, player.inPrisonCounter);
        }
        
        [TestMethod]
        public void GoTest()
        {
            var player = new Player();
            //test passing go
            player.movePlayerForward(45);
            Assert.AreEqual(1700, player.balance);
        }

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

        [TestMethod]
        public void AddPropertyTest()
        {
            var player = new Player();
            var street = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var railroad = new Railroads(15, new Group(4), 200, "Pennsylvania Railroad");
            var utility = new Utility(28, new Group(2), 150, "Water Works");
            player.addProperty(street);
            player.addProperty(railroad);
            player.addProperty(utility);
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(street));
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(railroad));
            Assert.AreEqual(true, player.getPropertiesOwned().Contains(utility));
        }

        [TestMethod]
        public void RemoveTest()
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
        [TestMethod]
        public void CalculateScoreTest()
        {
            var player = new Player();
            var player2 = new Player();
            
            var street1 = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var street2 = new Street(8, new Group(2,50), 100, new int[] { 6, 30, 90, 270, 400, 550 }, "Vermont Ave");
            var street3 = new Street(9, new Group(2,50), 120, new int[] { 8, 40, 100, 300, 450, 600 }, "Connecticut Ave");
            
            player.addProperty(street1);
            street1.DevelopProperty(1);
            Assert.AreEqual(1600, player.CalculateScore());
            Assert.AreEqual(1500, player2.CalculateScore());
        }
    }
}