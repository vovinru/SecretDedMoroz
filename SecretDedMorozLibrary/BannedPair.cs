using System;
using System.Collections.Generic;
using System.Text;

namespace SecretDedMorozLibrary
{
    /// <summary>
    /// Запрещенные пары
    /// </summary>
    public class BannedPair
    {
        public Player Player1
        {
            get;
            set;
        }

        public Player Player2
        {
            get;
            set;
        }

        public BannedPair(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public override string ToString()
        {
            return Player1.Name + " - " + Player2.Name;
        }
    }
}
