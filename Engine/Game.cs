using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {

    public class Game {

        public Game() {
            board = new Board();

            player1 = new HumanPlayer(board);
            player2 = new HumanPlayer(board);

            moveHistory = new List<string>();
        }

        public Game(Board board) {

            this.board = board;

            player1 = new HumanPlayer(board);
            player2 = new HumanPlayer(board);

            moveHistory = new List<string>();
        }

        public Game(Board board, Player player1, Player player2) {
            this.board = board;

            this.player1 = player1;
            this.player2 = player2;

            moveHistory = new List<string>();
        
        }

        public Player player1;
        public Player player2;

        public Board board;

        public List<String> moveHistory;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns winning player</returns>
        public Player StartGame() {

            int turn = 0;

            while (true) {

                var t = player1.MakeMove();
                ++turn;
                moveHistory.Add(turn.ToString() + "/t" + t.Item2);

                if (t.Item1 == MoveResult.Win) {
                    Console.WriteLine("Player 1 won");

                    return player1;
                }

                t = player2.MakeMove();
                ++turn;
                moveHistory.Add(turn.ToString() + "/t" + t.Item2);

                if (t.Item1 == MoveResult.Win) {
                    Console.WriteLine("Player 2 won");
                    
                    return player2;
                }

                Console.WriteLine( board.ToString() );
            }

        }

        public String GetHistory() {
            String s = "";


            return s;
        }
    }
}
