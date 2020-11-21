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
            Assert.AreEqual(1, player.position);
            //test adding balance
            player.addBalance(40);
            Assert.AreEqual(1540, player.balance);
            //test going to jail
            player.goToJail();
            Assert.IsTrue(player.inPrison);
            Assert.AreEqual(10, player.position);
            //test prison counter
            player.UpdateInPrisonCounter();
            Assert.AreEqual(1, player.inPrisonCounter);
            
            player.GetOutOfJail();
            //test get out of jail
            Assert.Equals(false, player.inPrison);
            Assert.AreEqual(0,player.inPrisonCounter);
            //test subtract balance
            player.subtractBalance(40);
            Assert.AreEqual(1500, player.balance);
            //test has enough money
                //current balance should be 0
            Assert.Equals(false, player.HasEnoughMoney(2000));
            Assert.Equals(true, player.HasEnoughMoney(160));
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

        public void PropertyActionsTest()
        {
            var player = new Player();
            var street = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var railroad = new Railroads(15, new Group(4), 200, "Pennsylvania Railroad");
            var utility = new Utility(28, new Group(2), 150, "Water Works");
            //test getting properties owned list, addProperty, and remove property
            player.addProperty(street);
            player.addProperty(railroad);
            player.addProperty(utility);
            Assert.Equals(true, player.getPropertiesOwned().Contains(street));
            Assert.Equals(true, player.getPropertiesOwned().Contains(railroad));
            Assert.Equals(true, player.getPropertiesOwned().Contains(utility));
            player.removeProperty(railroad);
            player.removeProperty(street);
            player.removeProperty(utility);
            Assert.Equals(false, player.getPropertiesOwned().Contains(street));
            Assert.Equals(false, player.getPropertiesOwned().Contains(railroad));
            Assert.Equals(false, player.getPropertiesOwned().Contains(utility));
        }
        [TestMethod]
        public void CalculateScoreTest()
        {
            var player = new Player();
            var player2 = new Player();
            
            var street1 = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var street2 = new Street(8, new Group(2,50), 100, new int[] { 6, 30, 90, 270, 400, 550 }, "Vermont Ave");
            var street3 = new Street(9, new Group(2,50), 120, new int[] { 8, 40, 100, 300, 450, 600 }, "Connecticut Ave");
            
            street1.DevelopProperty(1);
            Assert.AreEqual(50, player.CalculateScore());
            Assert.AreEqual(0, player2.CalculateScore());
        }
    }
}