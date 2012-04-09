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

            if (game.MoveList.Count < 7)
            {
                return false;
            }

            if (CheckHorizontal(game))
            {

            }

            return false;
        }

        private bool CheckHorizontal(Models.Game game)
        {
            var lastMove = game.MoveList.Last();
            int player = game.MoveList.Last().UserID;

            //if (game.Position(lastMove.position
            return false;
        }

        internal bool HasPlayed(Models.Game game, Tuple<int, int> spot)
        {
            return game.MoveList.FirstOrDefault(p => p.position.Equals(spot)) != null;
        }
    }
}