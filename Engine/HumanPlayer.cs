using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{

    public class HumanPlayer : Player
    {

        public HumanPlayer()
        {

        }

        public HumanPlayer(Board board)
        {
            this.board = board;
        }

        public override Tuple<MoveResult, String> MakeMove()
        {
            var move = Console.ReadLine();

            var moveResult = board.MakeMove(move);

            Console.WriteLine(move + "\t" + moveResult);

            return Tuple.Create(MoveResult.None, move);
        }
    }
}
