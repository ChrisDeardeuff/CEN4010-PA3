﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class MonopolyGameTest
    {
        [TestMethod]
        public void GetDevelopmentValueTest()
        {
            MonopolyGame mg = new MonopolyGame(2);
            
            
            var value = mg.GetDevelopmentValue(11);
            Assert.AreEqual(0, value);
            var dneValue = mg.GetDevelopmentValue(45);
            Assert.AreEqual(-2, dneValue);

        }

        [TestMethod]
        public void NextPlayersTurnTest()
        {
            MonopolyGame mg = new MonopolyGame(2);

            var roll = mg.CanRoleDice;
            Assert.AreEqual(false, roll);
            
            //round 1, player turn 1
            mg.NextPlayersTurn();
            roll = mg.CanRoleDice;
            Assert.AreEqual(true,roll);
            var currentPlayer = mg.currentPlayerID;
            Assert.AreEqual(1, currentPlayer);
            var round = mg.Round;
            Assert.AreEqual(1, round);
            //player 2
            mg.NextPlayersTurn();
            currentPlayer = mg.currentPlayerID;
            Assert.AreEqual(2, currentPlayer);
            //round 2 player 1
            mg.NextPlayersTurn();
            currentPlayer = mg.currentPlayerID;
            Assert.AreEqual(1, currentPlayer);
            round = mg.Round;
            Assert.AreEqual(2, round);
        }

        [TestMethod]
        public void GetUserTokenNameTest()
        {
            
        }

        [TestMethod]
        public void ApplyDevelopPropertyTest()
        {
            
        }

        [TestMethod]
        public void CalculateMoneyAndHousesNeededTest()
        {
            
        }

        [TestMethod]
        public void DevelopPropertyTest()
        {
            
        }

        [TestMethod]
        public void BuyPropertyTest() 
        {
            
        }
        
        [TestMethod]
        public void GetPropertiesOwnedByPlayerTest() 
        {
            
        }
        
        [TestMethod]
        public void GetNamesForPropertyTest() 
        {
            
        }
        
        [TestMethod]
        public void HasAnyBuildingsOnItTest() 
        {
            
        }
        
        [TestMethod]
        public void CompleteTradeTest() 
        {
        
        }

    }
}