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
            Console.WriteLine("3. Exit");
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
                        game = new Game(board, new ComputerPlayer(board, Pone.White), new HumanPlayer(board,Pone.Black));
                    }
                    else
                    {
                        game = new Game(board, new HumanPlayer(board, Pone.White), new ComputerPlayer(board, Pone.Black));
                    }

                    break;
                case "3":
                    return;
            }
            game.StartGame();
        }
    }
}
