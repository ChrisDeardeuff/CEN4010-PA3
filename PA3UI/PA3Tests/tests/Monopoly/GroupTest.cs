using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    
    public class GroupTest
    {
        [TestMethod]
        public void GetAmountTest()
        {
            var player = new Player();
            var group = new Group(3);

            //test get amount player owns with none owned or created
            Assert.ThrowsException<NullReferenceException>(()=>group.GetAmountPlayerOwns(0));
            
            //test get amount player owns with none owned and streets created
            var street1 = new Street(6,group, 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var street2 = new Street(8, group, 100, new int[] { 6, 30, 90, 270, 400, 550 }, "Vermont Ave");
            var street3 = new Street(9, group, 120, new int[] {8, 40, 100, 300, 450, 600}, "Connecticut Ave");
            Assert.AreEqual(0,group.GetAmountPlayerOwns(0));
            
            //test get amount player owns with 1
            street1.BoughtByPlayer(0);
            Assert.AreEqual(1,group.GetAmountPlayerOwns(0));
            //test get amount player owns with 2
            street2.BoughtByPlayer(0);
            Assert.AreEqual(2,group.GetAmountPlayerOwns(0));
            //test get amount with 3
            street3.BoughtByPlayer(0);
            Assert.AreEqual(3,group.GetAmountPlayerOwns(0));
        }

        [TestMethod]

        public void HasBuildingsTest()
        {
            var group = new Group(3);
            //test 'has any buildings' with no buildings
            Assert.AreEqual(false, group.HasAnyBuildings());
            //test with 1 building
            var street1 = new Street(6,group, 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var street2 = new Street(8, group, 100, new int[] { 6, 30, 90, 270, 400, 550 }, "Vermont Ave");
            var street3 = new Street(9, group, 120, new int[] {8, 40, 100, 300, 450, 600}, "Connecticut Ave");
            street1.DevelopProperty(1);
            Assert.AreEqual(true, group.HasAnyBuildings());
        }
    }
}