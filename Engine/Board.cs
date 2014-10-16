using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Engine
{

    public enum Pone
    {
        Empty, White, Black
    }

    public enum MoveResult
    {
        None, NotKilling, Win, Loose, Draw
    }
    /// <summary>
    /// Klasa reprezentujaca plansze. Ona pilnuje czy ruchy sa poprawne. 
    /// 
    /// [0,0] to A1, a [7,7] to H8
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Pierwszy indeks to wiersz
        /// </summary>
        public Pone[,] Pones {get; private set;}


        public Board()
        {
            Pones = new Pone[8, 8];
        }
        public Board(Pone[,] arg)
        {
            if (arg == null)
                throw new ArgumentNullException();

            if (arg.GetLength(0) != 8 && arg.GetLength(1) != 8)
                throw new ArgumentException("Wrong board size!");

            Pones = (Pone[,])arg.Clone();
        }

        public Board GetCopy()
        {
            return new Board(Pones);
        }

        /// <summary>
        /// Robi ruch.
        /// </summary>
        /// <param name="move">Opis ruchu w formacie: a1 b2 c3 <- dwa skoki</param>
        /// <returns>true - legalny, false - nielegalny</returns>
        public bool MakeMove(string move)
        {
            string [] moves = move.Split(' ');

            var regex = new Regex(@"^([a-hA-H][1-8])$");

            if (moves.Any(x => !regex.IsMatch(x)))
                throw new FormatException();

            return false;
        }

        
        /// <summary>
        /// Drukuje plansze w postaci pozycji wszystkich pionkow.
        /// </summary>
        /// <returns>male litery - biale, duze litery - czarne</returns>
        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(Pones[i,j] != Pone.Empty)
                    {
                        result += ((char)((int)(Pones[i,j] == Pone.White ? 'a' : 'A') + i)).ToString()+ (j+1).ToString()+" ";
                    }
                }
            }

            return result;
        }

    }
}
