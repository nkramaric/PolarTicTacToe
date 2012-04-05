using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Utils
{
    public class GameRules
    {
        internal bool IsFinished(Models.Game game, out int? winner)
        {
            ///TODO: implement this
            winner = null;
            return false;
        }

        internal bool HasPlayed(Models.Game game, Tuple<int, int> spot)
        {
            return game.MoveList.FirstOrDefault(p => p.position.Equals(spot)) != null;
        }
    }
}