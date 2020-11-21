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
            var player2 = new Player();
            var group = new Group(3);
            //test get amount owns with none
            Assert.AreEqual(0,group.GetAmountPlayerOwns(0));
            //test 'has any buildings' with no buildings
            Assert.Equals(false, group.HasAnyBuildings());
            var street1 = new Street(6, new Group(2, 50), 100, new int[] {6, 30, 90, 270, 400, 550}, "Oriental Ave");
            var street2 = new Street(8, new Group(2,50), 100, new int[] { 6, 30, 90, 270, 400, 550 }, "Vermont Ave");
            var street3 = new Street(9, new Group(2, 50), 120, new int[] {8, 40, 100, 300, 450, 600},
                "Connecticut Ave");
            //test add property
            group.AddProperty(street1);
            //test get amount owns with 1 
            Assert.AreEqual(1,group.GetAmountPlayerOwns(0));
            group.AddProperty(street2);
            Assert.AreEqual(2,group.GetAmountPlayerOwns(0));
            street1.DevelopProperty(1);
            //test 'has any buildings' with a building
            Assert.AreEqual(true,group.HasAnyBuildings());
        }
    }
}