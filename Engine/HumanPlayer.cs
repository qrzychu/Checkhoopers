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

        public HumanPlayer(Board board, Pone color) : base(board,color)
        {
        }

        public override Tuple<MoveResult, String> MakeMove()
        {
            Tuple<MoveResult, string> moveResult;
            do
            {
                var move = Console.ReadLine();

                moveResult = new Tuple<MoveResult, string>(board.MakeMove(move), move);
                if (moveResult.Item1 == MoveResult.Illegal)
                    Console.WriteLine("This move is illegal. Try something else");
            } while (moveResult.Item1 == MoveResult.Illegal);

            return moveResult;
        }
    }
}
