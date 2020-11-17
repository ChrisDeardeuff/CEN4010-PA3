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
            var group = new Group(3);
            //test get amount owns with none
                //Assert.AreEqual(0,group.GetAmountPlayerOwns());
            //test 'has any buildings' with no buildings
            Assert.Equals(false, group.HasAnyBuildings());
            //test add property
                // group.AddProperty(property: Property);
            //test get amount owns with 1 
                //Assert.AreEqual(1,group.GetAmountPlayerOwns());
            //test 'has any buildings' with a building
            Assert.AreEqual(true,group.HasAnyBuildings());
        }
    }
}