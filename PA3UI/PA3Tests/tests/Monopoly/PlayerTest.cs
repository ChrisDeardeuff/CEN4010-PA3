using System;
using System.Collections.Generic;
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
            //test adding balance
            player.addBalance(40);
            Assert.AreEqual(40, player.balance);
            //test going to jail
            player.goToJail();
            Assert.IsTrue(player.inPrison);
            Assert.AreEqual(10, player.position);
            //test prison counter
            player.UpdateInPrisonCounter();
            Assert.AreEqual(1, player.inPrisonCounter);
            //test get out of jail
            Assert.Equals(false, player.inPrison);
            Assert.AreEqual(0,player.inPrisonCounter);
            //test subtract balance
            player.subtractBalance(40);
            Assert.AreEqual(0, player.balance);
            //test has enough money
                //current balance should be 0
            Assert.Equals(false, player.HasEnoughMoney(70));
            player.addBalance(1000);
            Assert.Equals(true, player.HasEnoughMoney(160));
        }
        [TestMethod]
        public void GoTest()
        {
            var player = new Player();
            //test passing go
            player.movePlayerForward(40);
            Assert.AreEqual(200, player.balance);
        }

        [TestMethod]

        public void PropertyActionsTest()
        {
            var player = new Player();
            //test getting properties owned list
                // Assert.That.GetType(List<>, player.getPropertiesOwned());
            player.getPropertiesOwned();
                //  player.addProperty(property: Property);
                // Assert.Equals(true, player.getPropertiesOwned().Contains());
                // player.removeProperty(property:);
            //make sure list does not have property
                //Assert.Equals(false, player.getPropertiesOwned().Contains());
        }
    }
}