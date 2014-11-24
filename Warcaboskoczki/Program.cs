using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * Tu będziemy pisać program działający w lini komend
 * 
 */
namespace Warcaboskoczki
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            Game game = null;

            Console.WriteLine("Choose game mode:");
            Console.WriteLine("1. Hot seat");
            Console.WriteLine("2. Human vs Computer");
            Console.WriteLine("3. Computer vs Computer");
            Console.WriteLine("4. Exit");
            string res;
            do
            {
                res = Console.ReadLine();
            } while (res != "1" && res != "2" && res != "3");


            switch (res)
            {
                case "1":
                    game = new Game(board);
                    break;
                case "2":
                    while (res != "black" && res != "white")
                    {
                        Console.WriteLine("Pick color (black/white):");
                        res = Console.ReadLine();
                    }


                    if (res == "black")
                    {
                        game = new Game(board, new ComputerPlayer(board, Pone.White, true) { MaxDepth = 3 }, new HumanPlayer(board, Pone.Black));
                    }
                    else
                    {
                        game = new Game(board, new HumanPlayer(board, Pone.White), new ComputerPlayer(board, Pone.Black, true) { MaxDepth = 3 });
                    }

                    break;
                case "3":
                    Console.Write("Specify number of simulations:  ");
                    res = Console.ReadLine();

                    int count = int.Parse(res), white = 0, black = 0, draw = 0;


                    for (int i = 0; i < count; i++)
                    {
                        var newBoard = board.GetCopy();
                        game = new Game(newBoard, new ComputerPlayer(newBoard, Pone.White, true) { MaxDepth = 5 }, new ComputerPlayer(newBoard, Pone.Black, true) { MaxDepth = 4 }) { Silent = true };

                        game.StartGame();

                        var result = newBoard.IsGameOver();
                        if (result == Pone.White)
                        {
                            ++white;
                        }
                        else if (result == Pone.Black)
                        {
                            ++black;
                        }
                        else
                        {
                            ++draw;
                        }
                    }

                    Console.WriteLine("White: {0}\tBlack: {1}\tDraw: {3}", white, black, draw);

                    Console.ReadLine();
                    break;
                default:
                    return;
            }
            game.StartGame();
        }
    }
}
