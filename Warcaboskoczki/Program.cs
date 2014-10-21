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
            var b = new Board();
            b.ToString();

            var g = new Game(b);

            g.StartGame();
        }
    }
}
