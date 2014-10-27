using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{


    public class ComputerPlayer : Player
    {
        public enum State
        {
            Opening, MiddleGame, Ending
        }

        public State PlayerState { get; set; }


        private int openingMoveCounter = 0;


        private Tuple<MoveResult, string> GoEnding()
        {
            throw new NotImplementedException();
        }

        private Tuple<MoveResult, string> GoMiddleGame()
        {
            List<int[]> myPones = new List<int[]>();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.Pones[x, y] == Color)
                        myPones.Add(new int[2] { x, y });
                }
            }

            Dictionary<string, List<Tuple<string, int>>> possibleMoves = new Dictionary<string, List<Tuple<string, int>>>();
            myPones.ForEach(x => possibleMoves.Add(Board.GetCoordString(x[0],x[1]), board.GetMoves(x[0], x[1])));


            //dummy
            return null;
        }

        public ComputerPlayer(Pone color)
        {
            PlayerState = State.Opening;
            Color = color;
        }

        public ComputerPlayer(Board board, Pone color) : base(board,color)
        {
            
        }

        /// <summary>
        /// Computes move value. Assume that move is legal
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public int RateMove(Board board, string move)
        {
            // dummy
            return 0;
        }

        public override Tuple<MoveResult,string> MakeMove()
        {
            switch (PlayerState)
            {
                case State.Opening:
                    var move = GoOpening();
                    if (move != null && board[move.Substring(3)] == Pone.Empty)
                    {
                        board.MakeMove(move);
                        return new Tuple<MoveResult, string>(MoveResult.None, move);
                    }
                    return null;
                case State.MiddleGame:
                    return GoMiddleGame();
                //case State.Ending:
                //    return GoEnding();
                default:
                    // this stays like that
                    throw new NotImplementedException();
            }
        }

        private string GoOpening()
        {
            if (board.CanKill(Color))
            {
                PlayerState = State.MiddleGame;
                return null;
            }
            ++openingMoveCounter;
            if (Color == Pone.White)
                switch (openingMoveCounter)
                {
                    case 1:
                        return "a1 c3";
                    case 2:
                        return "a5 c7";
                    case 3:
                        return "a7 c5";
                    case 4:
                        return "a3 c1";
                    case 5:
                        return "b8 d6";
                    case 6:
                        return "b6 d8";
                    case 7:
                        return "b4 d2";
                    case 8:
                        return "b2 d4";
                    default:
                        PlayerState = State.MiddleGame;
                        return null;
                }
            else
                switch (openingMoveCounter)
                {
                    case 1:
                        return "h8 f6";
                    case 2:    
                        return "h4 f2";
                    case 3:    
                        return "h2 f4";
                    case 4:    
                        return "h6 f8";
                    case 5:    
                        return "g1 e3";
                    case 6:    
                        return "g7 e5";
                    case 7:    
                        return "g3 e1";
                    case 8:    
                        return "g5 e7";
                    default:
                        PlayerState = State.MiddleGame;
                        return null;
                }
        }
    }
}
