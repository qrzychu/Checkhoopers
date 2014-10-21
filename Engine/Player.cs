using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {

    public abstract class Player {

        public Player() {

        }

        public int playerID;

        public Board board {get; set;}

        
        /// <summary>
        /// returns pair <moveResult, string move> 
        /// </summary>
        /// <returns></returns>
        public abstract Tuple<MoveResult, String> MakeMove();
    }

}
