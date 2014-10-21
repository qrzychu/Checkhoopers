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
        None, NotKilling, Win, Illegal, Draw // loose chyba niepotrzebne
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
        public Pone[,] Pones { get; private set; }


        public Board()
        {
            Pones = new Pone[8, 8];

            for (int i = 0; i < 8; i += 2)
            {
                Pones[i, 0] = Pone.White;
                Pones[i + 1, 1] = Pone.White;

                Pones[i, 6] = Pone.Black;
                Pones[i + 1, 7] = Pone.Black;
            }
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
        public MoveResult MakeMove(string move)
        {
            string[] moves = move.Split(' ');

            var regex = new Regex(@"^([a-hA-H][1-8])$");

            if (moves.Any(x => !regex.IsMatch(x)) || moves.Length < 2)
            {
                throw new FormatException(); /// trochę lipa
            }
            moves = moves.Select(x => x.ToLower()).ToArray();
            int a = (int)'a';
            var coords = moves.Select(x => new int[2] { (int)x[0] - a, (int)x[1] - (int)'1' }).ToArray();

            // 
            if (coords.Any(x => (x[0] + x[1]) % 2 != 0) && Pones[coords[0][0], coords[0][1]] != Pone.Empty)
                return MoveResult.Illegal;

            bool canKill = CanKill(Pones[coords[0][0],coords[0][1]]);
            bool killed = false;
            MoveResult result = MoveResult.None;

            for (int i = 1; i < coords.Length; i++)
            {
                if (Pones[coords[i][0], coords[i][1]] == Pone.Empty)
                {

                    if (Math.Abs(coords[i - 1][0] - coords[i - 1][1]) == 1 && Math.Abs(coords[i - 1][0] - coords[i - 1][1]) == 1)
                    {
                        if (moves.Length != 2)
                        {
                            result = MoveResult.Illegal;
                            break;
                        }

                        Pones[coords[i][0], coords[i][1]] = Pones[coords[i - 1][0], coords[i - 1][1]];
                        Pones[coords[i - 1][0], coords[i - 1][1]] = Pone.Empty;

                        if (checkDraw())
                        {
                            result = MoveResult.Draw;
                        }
                    }
                    else
                    {
                        var x = (coords[i - 1][0] + coords[i][0]) / 2;
                        var y = (coords[i - 1][1] + coords[i][1]) / 2;
                        if (Math.Abs(coords[i - 1][0] - coords[i][0]) == 2 && Math.Abs(coords[i - 1][1] - coords[i][1]) == 2 && Pones[x, y] != Pone.Empty)
                        {
                            if(Pones[x,y] != Pones[coords[i - 1][0],coords[i - 1][1]])
                            {
                                killed = true;
                                Pones[x, y] = Pone.Empty;
                            }
                            Pones[coords[i][0], coords[i][1]] = Pones[coords[i - 1][0], coords[i - 1][1]];
                            Pones[coords[i - 1][0], coords[i - 1][1]] = Pone.Empty;
                        }
                    }
                }


            }
            if (canKill && !killed)
                return MoveResult.NotKilling;

            return result;
        }

        private bool CanKill(Pone color)
        {
            return false;
        }

        public bool checkDraw()
        {
            int black = 0, white = 0;
            foreach (var pone in Pones)
            {
                if (pone == Pone.White)
                    ++white;
                else if (pone == Pone.Black)
                    ++black;
            }

            if (black != 1 || white != 1)
                return false;

            //       B7                            A7                              G1                              H2
            if (Pones[1, 7] != Pone.Empty || Pones[0, 6] != Pone.Empty || Pones[7, 0] != Pone.Empty || Pones[1, 6] != Pone.Empty)
                return true;
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
                    if (Pones[i, j] != Pone.Empty)
                    {
                        result += ((char)((int)(Pones[i, j] == Pone.White ? 'a' : 'A') + i)).ToString() + (j + 1).ToString() + " ";
                    }
                }
            }

            return result;
        }

    }
}
