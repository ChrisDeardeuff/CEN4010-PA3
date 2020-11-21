using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class RailroadsTest
    {
        [TestMethod]
        public void RailroadRentTest()
        {
            var group = new Group(4);
            var rail1 = new Railroads(15, group, 200, "Pennsylvania Railroad");
            var rail2 = new Railroads(5, group, 200, "Reading Railroad");
            var rail3 = new Railroads(25, group, 200, "B & 0 Railroad");
            var rail4 = new Railroads(35, group, 200, "Short Line");
            
            //get rent when one railroad is owned
            rail1.BoughtByPlayer(0);
            Assert.AreEqual(25, rail1.GetRent());
            //get rent when railroad is mortgaged
            rail1.DevelopProperty(-1);
            Assert.AreEqual(0, rail1.GetRent());
            //get rent with two railroads
            rail1.DevelopProperty(0);
            rail2.BoughtByPlayer(0);
            Assert.AreEqual(50, rail1.GetRent());
            //get rent with 3 railroads
            rail3.BoughtByPlayer(0);
            Assert.AreEqual(100, rail1.GetRent());
            //get rent with 4 railroads
            rail4.BoughtByPlayer(0);
            Assert.AreEqual(200, rail1.GetRent());
        }

        [TestMethod]
        public void RailMortgageTest()
        {
            //testing can be mortgaged
            var group = new Group(1);
            var rail1 = new Railroads(15, group, 200, "Pennsylvania Railroad");
            //test can be mortgaged and can player build
            Assert.AreEqual(true, rail1.CanBeMortaged()); //separate method for this
        }

        [TestMethod]

        public void CanBuildOnRailroadTest()
        {
            //testing can player build
            var player = new Player();
            var group = new Group(1);
            var rail1 = new Railroads(15, group, 200, "Pennsylvania Railroad");
            //test can be mortgaged and can player build
            Assert.AreEqual(false, rail1.CanPlayerBuild(0)); //separate method for this
        }
    }
}