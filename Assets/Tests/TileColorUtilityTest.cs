using NUnit.Framework;
using com.paintpuzzle;

namespace Tests
{
    public class TileColorUtilityTest
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void MixColorTest()
        {
            Assert.AreEqual(TileColor.Red, TileColorUtility.MixColor(TileColor.White, TileColor.Red));
            Assert.AreEqual(TileColor.Green, TileColorUtility.MixColor(TileColor.White, TileColor.Green));
            Assert.AreEqual(TileColor.Blue, TileColorUtility.MixColor(TileColor.White, TileColor.Blue));

            Assert.AreEqual(TileColor.Cyan, TileColorUtility.MixColor(TileColor.Blue, TileColor.Green));
            Assert.AreEqual(TileColor.Green, TileColorUtility.MixColor(TileColor.Green, TileColor.Green));
            Assert.AreEqual(TileColor.Magenta, TileColorUtility.MixColor(TileColor.Blue, TileColor.Red));
        }
    }
}