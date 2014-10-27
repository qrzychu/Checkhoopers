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
