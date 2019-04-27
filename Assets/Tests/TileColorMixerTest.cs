using NUnit.Framework;
using com.paintpuzzle;

namespace Tests
{
    public class TileColorMixerTest
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void MixTest()
        {
            Assert.AreEqual(TileColor.Red, TileColorMixer.Mix(TileColor.White, TileColor.Red));
            Assert.AreEqual(TileColor.Green, TileColorMixer.Mix(TileColor.White, TileColor.Green));
            Assert.AreEqual(TileColor.Blue, TileColorMixer.Mix(TileColor.White, TileColor.Blue));

            Assert.AreEqual(TileColor.Cyan, TileColorMixer.Mix(TileColor.Blue, TileColor.Green));
            Assert.AreEqual(TileColor.Green, TileColorMixer.Mix(TileColor.Green, TileColor.Green));
            Assert.AreEqual(TileColor.Magenta, TileColorMixer.Mix(TileColor.Blue, TileColor.Red));
        }
    }
}