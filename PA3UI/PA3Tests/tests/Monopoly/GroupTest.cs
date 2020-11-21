using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    
    public class GroupTest
    {
        [TestMethod]
        public void GroupPropertyTest()
        {
            var player = new Player();
         //   var player2 = new Player();
            var group = new Group(3);
            //test get amount player owns with none
            Assert.ThrowsException<NullReferenceException>(()=>group.GetAmountPlayerOwns(0));
            var street1 = new Street(6,group, 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var street2 = new Street(8, group, 100, new int[] { 6, 30, 90, 270, 400, 550 }, "Vermont Ave");
            var street3 = new Street(9, group, 120, new int[] {8, 40, 100, 300, 450, 600}, "Connecticut Ave");
            
            
         //   group.AddProperty(street1);
            //test 'has any buildings' with no buildings
         //   Assert.ThrowsException<NullReferenceException>(()=>group.HasAnyBuildings());
            //test get amount player owns with 1 
            Assert.AreEqual(0,group.GetAmountPlayerOwns(0));
            street1.BoughtByPlayer(0);
          //  street1.DevelopProperty(0);
          //  player.addProperty(street1);
            Assert.AreEqual(1,group.GetAmountPlayerOwns(0));
            //test get amount player owns with 2
         //   group.AddProperty(street2);
           // street2.DevelopProperty(0);
            street2.BoughtByPlayer(0);
            Assert.AreEqual(2,group.GetAmountPlayerOwns(0));
            //test 'has any buildings' with a building
            street3.BoughtByPlayer(0);
            Assert.AreEqual(3,group.GetAmountPlayerOwns(0));
            
            street1.DevelopProperty(1);
            Assert.AreEqual(true,group.HasAnyBuildings());
        }
    }
}