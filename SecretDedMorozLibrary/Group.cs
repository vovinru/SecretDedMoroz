using System;
using System.Collections.Generic;
using System.Text;

namespace SecretDedMorozLibrary
{
    /// <summary>
    /// Основной класс работы тайного санты
    /// </summary>
    public class Group
    {
        public List<Player> Players
        {
            get;
            set;
        }

        public List<BannedPair> BannedPairs
        {
            get;
            set;
        }

        public Group()
        {
            Players = new List<Player>();
            BannedPairs = new List<BannedPair>();
        }

        /// <summary>
        /// Получить случайные пары
        /// </summary>
        /// <returns></returns>
        public List<Tuple<Player,Player>> GeneratePairs()
        {
            Random random = new Random();

            for (int i = 0; i < 10000; i++)
            {
                List<Tuple<Player, Player>> pairs = new List<Tuple<Player, Player>>();

                List<Player> playersMake = new List<Player>(Players);
                List<Player> playersGet = new List<Player>(Players);

                foreach (Player p in playersMake)
                {
                    List<Player> playersChoose = new List<Player>(playersGet);
                    p.GetBannedPlayers(BannedPairs).ForEach(player => playersChoose.Remove(player));

                    if (playersChoose.Count > 0)
                    {
                        int index = random.Next(playersChoose.Count);
                        pairs.Add(new Tuple<Player, Player>(p, playersChoose[index]));
                        playersGet.Remove(playersChoose[index]);
                    }

                    else
                        break;
                }

                if (pairs.Count == playersMake.Count)
                    return pairs;
            }

            return new List<Tuple<Player, Player>>() ;
        }
    }
}
