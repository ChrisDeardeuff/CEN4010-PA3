using Microsoft.VisualStudio.TestTools.UnitTesting;
using PA3BackEnd.src.Monopoly;

namespace PA3Tests.tests.Monopoly
{
    [TestClass]
    public class RailroadsTest
    {
        [TestMethod]
        public void CanBuildTest()
        {
            var rails = new Railroads(5, new Group(8), 200, "Reading Railroad");
            var rail = new Railroads(15, new Group(8), 200, "Pennsylvania Railroad");
            var rail3 = new Railroads(25, new Group(8), 200, "B & 0 Railroad");
            var rail4 = new Railroads(35, new Group(8), 200, "Short Line");
            var player = new Player();
            player.addProperty(rails);
            //test can be mortgaged and can player build
            Assert.AreEqual(false, rails.CanBeMortaged());
            Assert.AreEqual(false, rails.CanPlayerBuild(0));
            //get rent when railroad is mortgaged
            rails.DevelopProperty(-1);
            Assert.AreEqual(0, rails.GetRent());
            //get rent when one railroad is owned
            player.addProperty(rail);
            Assert.AreEqual(25, rail.GetRent());
            //get rent with two railroads
            rails.DevelopProperty(0);
            Assert.AreEqual(50, rails.GetRent());
            //get rent with 3 railroads
            player.addProperty(rail3);
            Assert.AreEqual(75, rails.GetRent());
            //get rent with 4 railroads
            player.addProperty(rail4);
            Assert.AreEqual(100, rail3.GetRent());
            Assert.AreEqual(100, rail4.GetRent());
        }
    }
}