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
        None, NotKilling, Win, Illegal, Draw
    }
    /// <summary>
    /// Class representing board. It handles pone movement
    /// 
    /// [0,0] is A1, and [7,7] is H8
    /// </summary>
    public class Board
    {
        /// <summary>
        /// First index means row (letters on real board)
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
        /// Makes move.
        /// </summary>
        /// <param name="move">Move description in format: a1 b2 c3 <- two jumps</param>
        /// <returns>MoveResult</returns>
        public MoveResult MakeMove(string move)
        {
            string[] moves = move.Split(' ');

            var regex = new Regex(@"^([a-hA-H][1-8])$");

            if (moves.Any(x => !regex.IsMatch(x)) || moves.Length < 2)
            {
                throw new FormatException();
            }
            moves = moves.Select(x => x.ToLower()).ToArray();
            int a = (int)'a';
            var coords = moves.Select(x => new int[2] { (int)x[0] - a, (int)x[1] - (int)'1' }).ToArray();

            // check if it is black field and if there is pone to move
            if (coords.Any(x => (x[0] + x[1]) % 2 != 0) || Pones[coords[0][0], coords[0][1]] == Pone.Empty)
                return MoveResult.Illegal;

            bool canKill = CanKill(Pones[coords[0][0], coords[0][1]]);
            bool killed = false;
            MoveResult result = MoveResult.None;

            for (int i = 1; i < coords.Length; i++)
            {
                // check if target field is empty and move is not back and forth
                if (Pones[coords[i][0], coords[i][1]] == Pone.Empty && (i > 1 ? (coords[i - 2][0] != coords[i][0] && coords[i - 2][1] != coords[i - 1][1]) : true))
                {
                    // check if pone moves just with one field
                    if (Math.Abs(coords[i - 1][0] - coords[i][0]) == 1 && Math.Abs(coords[i - 1][1] - coords[i][1]) == 1)
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
                    // else it jumps over something
                    else
                    {
                        var x = (coords[i - 1][0] + coords[i][0]) / 2;
                        var y = (coords[i - 1][1] + coords[i][1]) / 2;
                        if (Math.Abs(coords[i - 1][0] - coords[i][0]) == 2 && Math.Abs(coords[i - 1][1] - coords[i][1]) == 2 && Pones[x, y] != Pone.Empty)
                        {
                            if (Pones[x, y] != Pones[coords[i - 1][0], coords[i - 1][1]])
                            {
                                killed = true;
                                Pones[x, y] = Pone.Empty;
                            }
                            Pones[coords[i][0], coords[i][1]] = Pones[coords[i - 1][0], coords[i - 1][1]];
                            Pones[coords[i - 1][0], coords[i - 1][1]] = Pone.Empty;
                        }
                        //nothing to jump over
                        else
                        {
                            result = MoveResult.Illegal;
                            break;
                        }
                    }

                }
                else
                {
                    result = MoveResult.Illegal;
                    break;
                }

            }
            if (canKill && !killed && result != MoveResult.Illegal)
                return MoveResult.NotKilling;

            return result;
        }

        private bool CanKill(Pone color)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Pones[i, j] == color)
                        if (GetMoves(i, j).Any(x => x.Item2 != 0))
                            return true;
                }
            }
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

            //       B8                            A7                              G1                              H2
            if (Pones[1, 7] != Pone.Empty || Pones[0, 6] != Pone.Empty || Pones[7, 0] != Pone.Empty || Pones[1, 6] != Pone.Empty)
                return true;
            return false;
        }


        /// <summary>
        /// Prints board as position of all pones
        /// </summary>
        /// <returns>lowercase - white, uppercase - blacks</returns>
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

            return result.TrimEnd(' ');
        }

        /// <summary>
        /// Gets all possible move for given pone
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>List of pairs of move and kill count</returns>
        public List<Tuple<string, int>> GetMoves(int row, int col)
        {
            if (Pones[row, col] == Pone.Empty)
                return null;

            List<Tuple<string, int>> result = new List<Tuple<string, int>>();

            if (row > 0)
            {

                if (col > 0 && Pones[row - 1, col - 1] == Pone.Empty)
                {
                    result.Add(new Tuple<string, int>(getCoordString(row,col)+" " + getCoordString(row -1, col-1), 0));
                }
                if (col < 7 && Pones[row - 1, col + 1] == Pone.Empty)
                {
                    result.Add(new Tuple<string, int>(getCoordString(row, col) + " " + getCoordString(row - 1, col + 1), 0));
                }
            }
            if (row < 7)
            {
                if (col < 7 && Pones[row + 1, col + 1] == Pone.Empty)
                {
                    result.Add(new Tuple<string, int>(getCoordString(row, col) + " " + getCoordString(row + 1, col + 1), 0));
                }
                if (col > 0 && Pones[row + 1, col - 1] == Pone.Empty)
                {
                    result.Add(new Tuple<string, int>(getCoordString(row, col) + " " + getCoordString(row + 1, col - 1), 0));
                }
            }

            canJump(row, col,new Tuple<string,int>(getCoordString(row,col),0), result);


            return result;
        }

        private static string getCoordString(int row, int col)
        {
            int a = (int)'a';
            return ((char)(row + a)).ToString() + (col + 1).ToString();
        }

        private void canJump(int row, int col,Tuple<string,int> prevMove, List<Tuple<string, int>> result)
        {
            if (prevMove.Item1.Length > 4 && prevMove.Item1.Substring(0, 2) == getCoordString(row, col) && prevMove.Item2 == 0)
                return;

            if (row > 1)
            {

                if (col > 1 && Pones[row - 2, col - 2] == Pone.Empty && Pones[row - 1, col - 1] != Pone.Empty && (prevMove.Item1.Length > 4 ? !getCoordString(row - 2, col - 2).Equals(prevMove.Item1.Substring(prevMove.Item1.Length - 5, 2)) : true))
                {
                    bool kill = Pones[row - 1, col - 1] != Pones[row, col];
                    result.Add(new Tuple<string, int>(prevMove.Item1 + " " + getCoordString(row-2,col-2), prevMove.Item2 + (kill ? 1 : 0)));

                    if (kill)
                    {
                        Pones[row - 1, col - 1] = Pone.Empty;
                    }

                    Pones[row - 2, col - 2] = Pones[row,col];
                    Pones[row, col] = Pone.Empty;
                    canJump(row - 2, col - 2, result.Last(), result);
                    Pones[row, col] = Pones[row - 2, col - 2];
                    Pones[row - 2, col - 2] = Pone.Empty;
                    if(kill)
                        Pones[row - 1, col - 1] = Pones[row,col] == Pone.White ? Pone.Black : Pone.White;
                }
                if (col < 6 && Pones[row - 2, col + 2] == Pone.Empty && Pones[row - 1, col + 1] != Pone.Empty && (prevMove.Item1.Length > 4 ? !getCoordString(row - 2, col + 2).Equals(prevMove.Item1.Substring(prevMove.Item1.Length - 5, 2)) : true))
                {
                    bool kill = Pones[row - 1, col + 1] != Pones[row, col];
                     result.Add(new Tuple<string, int>(prevMove.Item1 + " " + getCoordString(row-2,col+2), prevMove.Item2 + (kill ? 1 : 0)));

                    if (kill)
                    {
                        Pones[row - 1, col + 1] = Pone.Empty;
                    }

                    Pones[row - 2, col + 2] = Pones[row, col];
                    Pones[row, col] = Pone.Empty;
                    canJump(row - 2, col + 2, result.Last(), result);
                    Pones[row, col] = Pones[row - 2, col + 2];
                    Pones[row - 2, col + 2] = Pone.Empty;
                    if (kill)
                        Pones[row - 1, col + 1] = Pones[row, col] == Pone.White ? Pone.Black : Pone.White;
                }
            }
            if (row < 6)
            {
                if (col > 1 && Pones[row + 2, col - 2] == Pone.Empty && Pones[row + 1, col - 1] != Pone.Empty && (prevMove.Item1.Length > 4 ? !getCoordString(row + 2, col - 2).Equals(prevMove.Item1.Substring(prevMove.Item1.Length - 5, 2)) : true))
                {
                    bool kill = Pones[row + 1, col - 1] != Pones[row, col];
                    result.Add(new Tuple<string, int>(prevMove.Item1 + " " + getCoordString(row + 2, col - 2), prevMove.Item2 + (kill ? 1 : 0)));

                    if (kill)
                    {
                        Pones[row + 1, col - 1] = Pone.Empty;
                    }

                    Pones[row + 2, col - 2] = Pones[row, col];
                    Pones[row, col] = Pone.Empty;
                    canJump(row + 2, col - 2, result.Last(), result);
                    Pones[row, col] = Pones[row + 2, col - 2];
                    Pones[row + 2, col - 2] = Pone.Empty;
                    if (kill)
                        Pones[row + 1, col - 1] = Pones[row, col] == Pone.White ? Pone.Black : Pone.White;
                }
                if (col < 6 && Pones[row + 2, col + 2] == Pone.Empty && Pones[row + 1, col + 1] != Pone.Empty && (prevMove.Item1.Length > 4 ? !getCoordString(row + 2, col + 2).Equals(prevMove.Item1.Substring(prevMove.Item1.Length - 5, 2)) : true))
                {
                    bool kill = Pones[row + 1, col + 1] != Pones[row, col];
                    result.Add(new Tuple<string, int>(prevMove.Item1 + " " + getCoordString(row + 2, col + 2), prevMove.Item2 + (kill ? 1 : 0)));

                    if (kill)
                    {
                        Pones[row + 1, col + 1] = Pone.Empty;
                    }

                    Pones[row + 2, col + 2] = Pones[row, col];
                    Pones[row, col] = Pone.Empty;
                    canJump(row + 2, col + 2, result.Last(), result);
                    Pones[row, col] = Pones[row + 2, col + 2];
                    Pones[row + 2, col + 2] = Pone.Empty;
                    if (kill)
                        Pones[row + 1, col + 1] = Pones[row, col] == Pone.White ? Pone.Black : Pone.White;
                }
            }
        }
    }
}
