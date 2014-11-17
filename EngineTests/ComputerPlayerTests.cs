using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;

namespace EngineTests
{
    /// <summary>
    /// Summary description for ComputerPlayerTests
    /// </summary>
    [TestClass]
    public class ComputerPlayerTests
    {
        

        [TestMethod]
        public void OpeningTest()
        {
            Opening(Pone.White);
            Opening(Pone.Black);
        }

        [TestMethod]
        public void KillTest()
        {
            var pones = new Pone[8, 8];
            pones[0, 0] = Pone.White;
            pones[1, 1] = Pone.Black;
            pones[3, 3] = Pone.Black;

            var board = new Board(pones) { CurrentPlayer = Pone.White};

            var ai = new ComputerPlayer(Pone.White) { board = board, PlayerState = ComputerPlayer.State.MiddleGame };

            var move = ai.MakeMove();
        }

        [TestMethod]
        public void EndingTest()
        {
            var pones = new Pone[8, 8];
            pones[6, 4] = Pone.Black;
            pones[3, 5] = Pone.White;

            var board = new Board(pones) { CurrentPlayer = Pone.White };

            var ai = new ComputerPlayer(Pone.White) { board = board, PlayerState = ComputerPlayer.State.MiddleGame };

            var move = ai.MakeMove();

            board.MakeMove(move.Item2);
            board.MakeMove("g5 h6");
            move = ai.MakeMove();

            board.MakeMove(move.Item2);
            board.MakeMove("h6 g5");
            move = ai.MakeMove();

            board.MakeMove(move.Item2);

            Assert.IsTrue(board.IsGameOver() == Pone.White);
        }
        
        public void Opening(Pone color)
        {
            var player = new ComputerPlayer(color);

            var board = new Board();

            player.board = board;

            for (int i = 0; i < 8; i++)
            {
                var t = player.MakeMove();
                Assert.AreEqual(player.PlayerState, ComputerPlayer.State.Opening);
            }
            player.MakeMove();
            Assert.AreEqual(player.PlayerState, ComputerPlayer.State.MiddleGame);
            //Assert.AreEqual(player.board.ToString(), "");
        }
    }
}
