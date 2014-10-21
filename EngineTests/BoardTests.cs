using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Engine.Tests
{
    [TestClass()]
    public class MakeMoveTests
    {
        [TestMethod()]
        public void NormalMoveTest()
        {
            var pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            var board = new Board(pones);

            Assert.AreEqual(board.MakeMove("a1 b2"), MoveResult.None);
        }

        [TestMethod]
        public void JumpTest()
        {
            var pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.White;
            var board = new Board(pones);

            Assert.AreEqual(board.MakeMove("a1 c3"), MoveResult.None);

            pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.White;
            pones[3, 3] = Pone.White;

            board = new Board(pones);
            Assert.AreEqual(board.MakeMove("a1 c3 e5"), MoveResult.None);
        }

        [TestMethod]
        public void KillTest()
        {
            var pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.Black;
            var board = new Board(pones);

            Assert.AreEqual(board.MakeMove("a1 c3"), MoveResult.None);
            Assert.AreEqual(board.ToString(), "c3");

            pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.Black;
            pones[3, 3] = Pone.Black;

            board = new Board(pones);
            Assert.AreEqual(board.MakeMove("a1 c3 e5"), MoveResult.None);
            Assert.AreEqual(board.ToString(), "e5");
        }



        [TestMethod]
        public void IllegalMovesTest()
        {
            var pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.Black;
            var board = new Board(pones);

            Assert.AreEqual(board.MakeMove("a1 a3"), MoveResult.Illegal);
            Assert.AreEqual(board.MakeMove("a1 a2"), MoveResult.Illegal);

            pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.Black;
            pones[2, 2] = Pone.Black;

            board = new Board(pones);

            Assert.AreEqual(board.MakeMove("a1 c3"), MoveResult.Illegal);
        }
    }
}
