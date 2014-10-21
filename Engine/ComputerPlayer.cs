using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {

    public class ComputerPlayer : Player {

        public ComputerPlayer() {

        }

        public ComputerPlayer(Board board) {
            this.board = board;
        }

        public override Tuple<MoveResult, String> MakeMove() {

            Console.WriteLine("Computer move");

            return Tuple.Create(MoveResult.None, "");
        }
    }
}
