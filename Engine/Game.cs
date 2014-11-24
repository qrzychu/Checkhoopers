using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{

    public class Game
    {

        public Game()
        {
            board = new Board();

            player1 = new HumanPlayer(board, Pone.White);
            player2 = new HumanPlayer(board, Pone.Black);

            moveHistory = new List<string>();
            Silent = false;
        }

        public Game(Board board)
        {

            this.board = board;

            player1 = new HumanPlayer(board, Pone.White);
            player2 = new HumanPlayer(board, Pone.Black);

            moveHistory = new List<string>();
            Silent = false;

        }

        bool exit = false;

        public Game(Board board, Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            moveHistory = new List<string>();
            this.board = board;
            Silent = false;

        }

        public Player player1;
        public Player player2;

        public Board board;

        public List<String> moveHistory;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns winning player</returns>
        public Player StartGame()
        {





            int turn = 0;
            if (!Silent)
                PrintBoard();
            while (board.IsGameOver() == null)
            {
                var moveResult = player1.MakeMove();
                ++turn;
                moveHistory.Add(turn.ToString() + "/t" + moveResult.Item2);
                if (!Silent)
                    PrintBoard();
                if (moveResult.Item1 == MoveResult.Win)
                {
                    Console.WriteLine("Player 1 won");

                    return player1;
                }
                if (!Silent)
                    Console.WriteLine();

                moveResult = player2.MakeMove();

                ++turn;
                moveHistory.Add(turn.ToString() + "/t" + moveResult.Item2);
                if (!Silent)
                    PrintBoard();
                if (moveResult.Item1 == MoveResult.Win)
                {
                    Console.WriteLine("Player 2 won");

                    return player2;
                }
                if (!Silent)
                    Console.WriteLine();
            }
            var res = board.IsGameOver();
            return res == Pone.White ? player1 : (res == Pone.Black ? player2 : null);

        }

        private void PrintBoard()
        {
            string letters = "ABCDEFGH";
            Console.Write(" ");
            for (int i = 1; i < 9; i++)
            {
                Console.Write(" {0} ", i);
            }
            Console.WriteLine();
            for (int x = 0; x < 8; x++)
            {
                Console.Write(letters[x]);
                for (int y = 0; y < 8; y++)
                {
                    switch (board.Pones[x, y])
                    {
                        case Pone.Empty:
                            Console.Write(" {0} ", (x + y) % 2 == 0 ? "@" : " ");
                            break;
                        case Pone.Black:
                            Console.Write(" B ");
                            break;
                        case Pone.White:
                            Console.Write(" W ");
                            break;
                    }
                }
                Console.Write(letters[x]);
                Console.WriteLine();
            }
            Console.Write(" ");
            for (int i = 1; i < 9; i++)
            {
                Console.Write(" {0} ", i);
            }
            Console.WriteLine();
        }

        public String GetHistory()
        {
            return string.Join(Environment.NewLine, moveHistory);
        }

        public bool Silent { get; set; }
    }
}
