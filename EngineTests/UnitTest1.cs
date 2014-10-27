using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void BordToStringTest()
        {
            var pones = new Pone[8, 8];

            pones[0, 0] = Pone.White;
            pones[3, 1] = Pone.White;
            pones[7, 7] = Pone.Black;

            var b = new Board(pones);

            Assert.AreEqual("a1 d2 H8", b.ToString(), true, "ToStringFailed");
        }

        [TestMethod]
        public void BoardGetCopyTest()
        {
            var pones = new Pone[8, 8];

            pones[0, 0] = Pone.White;

            var b = new Board(pones);
            var b2 = b.GetCopy();

            b.Pones[0, 0] = Pone.Black;

            Assert.AreNotEqual(b.Pones[0, 0], b2.Pones[0, 0]);
        }

   
        [TestMethod]
        public void DrawTest()
        {
            // drawTest was made public in order to be able to test
            var pones = new Pone[8, 8];
            pones[4, 4] = Pone.White;
            pones[1, 7] = Pone.Black;

            var board = new Board(pones);

            Assert.IsTrue(board.checkDraw());

            board.Pones[1, 1] = Pone.Black;

            Assert.IsTrue(!board.checkDraw());

            pones = new Pone[8, 8];
            pones[4, 4] = Pone.White;
            pones[6, 7] = Pone.Black;

            board = new Board(pones);
            Assert.IsTrue(!board.checkDraw());
        }
    }
}
