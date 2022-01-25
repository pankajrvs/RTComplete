using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Data
{
    public class Player
    {
        public string playerName { get; set; }
        public int playerLevel { get; set; }
        public string levelImageUrl { get; set; }
        public bool isOnline { get; set; }
        public string playerRank { get; set; }
        public string playerWeaponRank { get; set; }
        public string playerExp { get; set; }
        public string playerKills { get; set; }
        public string playerDeath { get; set; }
        public string playerGamePlayed { get; set; }
        public string playerRageQuit { get; set; }
        public string playerKD { get; set; }
        public string playerHeadshots { get; set; }
        public string playerHeadshotsRatio { get; set; }
        public string playerClan { get; set; }
        public DateTime Datadate { get; set; }
        public string Weapon_RaceRatio { get; set; }
        public string Weapon_RaceWins { get; set; }
        public string last_seen { get; set; }
        public string avatar_url { get; set; }
        public string level_progress { get; set; }
        public string registered_since { get; set; }
        public string onlinefromserver { get; set; }

        public List<Weapon> weapons { get; set; }
    }
    public class Weapon
    {
        public string weaponName { get; set; }
        public string weaponRank { get; set; }
        public string TotalKill { get; set; }
        public string WPlayername { get; set; }
        public string weaponName_imgurl { get; set; }
    }
    public class weaponmap
    {
        public string name { get; set; }
        public string url { get; set; }

    }
}
