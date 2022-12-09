using System;
using System.Collections.Generic;
using System.Text;

namespace SecretDedMorozLibrary
{
    /// <summary>
    /// участник игры в тайного санту
    /// </summary>
    public class Player
    {
        public string Name 
        { 
            get; 
            set; 
        }

        public string MailAddress
        {
            get;
            set;
        }

        public Player(string name, string mailAddress)
        {
            Name = name;
            MailAddress = mailAddress;
        }

        internal List<Player> GetBannedPlayers(List<BannedPair> bannedPairs)
        {
            List<Player> playerList = new List<Player>();

            playerList.Add(this);

            foreach(BannedPair pair in bannedPairs)
            {
                if (pair.Player1 == this)
                    playerList.Add(pair.Player2);
                if(pair.Player2 == this)
                    playerList.Add(pair.Player1);
            }

            return playerList;
        }

        public override string ToString()
        {
            return Name + " (" + MailAddress + ")";
        }
    }
}
