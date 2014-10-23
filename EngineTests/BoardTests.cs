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

            pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.White;

            board = new Board(pones);
            Assert.AreEqual(board.MakeMove("a1 c3 a1"), MoveResult.Illegal);
        }

        [TestMethod]
        public void GetMovesTest()
        {
            var pones = new Pone[8, 8];
            pones[1, 1] = Pone.Black;
            var board = new Board(pones);

            var res = board.GetMoves(1,1);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("b2 a1",0),
                        new Tuple<string,int>("b2 a3",0),
                        new Tuple<string,int>("b2 c1",0),
                        new Tuple<string,int>("b2 c3",0),
                    }
                    ));

            board.Pones[0, 0] = Pone.Black;
            res = board.GetMoves(1, 1);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("b2 a3",0),
                        new Tuple<string,int>("b2 c1",0),
                        new Tuple<string,int>("b2 c3",0),
                    }
                    ));
            pones = new Pone[8, 8];
            pones[1, 1] = Pone.Black;
            pones[3, 3] = Pone.Black;
            pones[4, 4] = Pone.Black;
            board = new Board(pones);
            res = board.GetMoves(4, 4);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("e5 d6",0),
                        new Tuple<string,int>("e5 f6",0),
                        new Tuple<string,int>("e5 f4",0),
                        new Tuple<string,int>("e5 c3",0),
                        new Tuple<string,int>("e5 c3 a1",0),
                    }
                    ));

            pones = new Pone[8, 8];
            pones[1, 1] = Pone.Black;
            pones[3, 3] = Pone.White;
            pones[4, 4] = Pone.Black;
            board = new Board(pones);
            res = board.GetMoves(4, 4);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("e5 d6",0),
                        new Tuple<string,int>("e5 f6",0),
                        new Tuple<string,int>("e5 f4",0),
                        new Tuple<string,int>("e5 c3",1),
                        new Tuple<string,int>("e5 c3 a1",1),
                    }
                    ));

            pones = new Pone[8, 8];
            pones[1, 1] = Pone.White;
            pones[3, 3] = Pone.White;
            pones[4, 4] = Pone.Black;
            board = new Board(pones);
            res = board.GetMoves(4, 4);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("e5 d6",0),
                        new Tuple<string,int>("e5 f6",0),
                        new Tuple<string,int>("e5 f4",0),
                        new Tuple<string,int>("e5 c3",1),
                        new Tuple<string,int>("e5 c3 a1",2),
                    }
                    ));
        }

        [TestMethod]
        public void GetMovesTest2()
        {
            var pones = new Pone[8, 8];
            pones[1, 1] = Pone.White;
            pones[3, 3] = Pone.White;
            pones[4, 4] = Pone.Black;
            pones[1, 3] = Pone.Black;
            pones[3, 1] = Pone.Black;
            var board = new Board(pones);

            var res = board.GetMoves(4, 4);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("e5 d6",0),
                        new Tuple<string,int>("e5 f6",0),
                        new Tuple<string,int>("e5 f4",0),
                        new Tuple<string,int>("e5 c3",1),
                        new Tuple<string,int>("e5 c3 a1",2),
                        new Tuple<string,int>("e5 c3 e1",1),
                        new Tuple<string,int>("e5 c3 a5",1),
                    }
                    ));

            pones = new Pone[8, 8];
            pones[1, 1] = Pone.Black;
            pones[3, 3] = Pone.Black;
            pones[2, 0] = Pone.Black;
            pones[1, 3] = Pone.Black;
            pones[3, 1] = Pone.Black;
            board = new Board(pones);

            res = board.GetMoves(2, 0);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("c1 e3",0),
                        new Tuple<string,int>("c1 e3 c5",0),
                        new Tuple<string,int>("c1 e3 c5 a3",0),
                        new Tuple<string,int>("c1 e3 c5 a3 c1",0),
                        new Tuple<string,int>("c1 a3",0),
                        new Tuple<string,int>("c1 a3 c5",0),
                        new Tuple<string,int>("c1 a3 c5 e3",0),
                        new Tuple<string,int>("c1 a3 c5 e3 c1",0),
                    }
                    ));

            board.Pones[5, 1] = Pone.White;
            res = board.GetMoves(2, 0);
            Assert.IsTrue(MoveListAreSame(res, new List<Tuple<string, int>>()
                    {
                        new Tuple<string,int>("c1 e3",0),
                        new Tuple<string,int>("c1 e3 c5",0),
                        new Tuple<string,int>("c1 e3 c5 a3",0),
                        new Tuple<string,int>("c1 e3 c5 a3 c1",0),
                        new Tuple<string,int>("c1 a3",0),
                        new Tuple<string,int>("c1 a3 c5",0),
                        new Tuple<string,int>("c1 a3 c5 e3",0),
                        new Tuple<string,int>("c1 a3 c5 e3 c1",0),
                        new Tuple<string,int>("c1 a3 c5 e3 g1",1),
                        new Tuple<string,int>("c1 e3 g1",1),
                    }
                   ));
        }

        private bool MoveListAreSame(List<Tuple<string,int>> a, List<Tuple<string,int>> b)
        {
            if (a.Count != b.Count)
                return false;

            foreach (var move in a)
            {
                if (!b.Contains(move))
                    return false;
            }

            return true;
        }
    }
}
