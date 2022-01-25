using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;
using RT.Data;

namespace RT.Api.Operations
{
    public class operations : Ioperations
    {
        String pattern = @"\s+";

        public void setapplicationsvariable(string name, List<Player> value)
        {
            //  HttpContext.Current.Application[name] = value;
        }
        public List<Player> loadplayers(string players)
        {
            List<Player> args_players = new List<Player>();
            List<string> arg_player = players.Split(',').ToList();
            foreach (string p in arg_player)
            {
                args_players.Add(setplayer(p));
            }
            return args_players;

        }
        public Player setplayer(string playername)
        {


            Player p = new Player();
            BrowserSession b = new BrowserSession();
            b.Get("https://www.gaming-style.com/RushTeam/index.php?page=Ranking");
            b.FormElements["user"] = playername;
            var res = b.Post("https://www.gaming-style.com/RushTeam/index.php?page=Ranking");
            try
            {
                p.playerName = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Nickname']").InnerText.Trim();
                p.playerRank = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Rank']/text()").InnerText.Replace("/t", "").Trim();
                p.playerLevel = Convert.ToInt32(res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Level']//img").Attributes["title"].Value.Replace("Level :", ""));
                p.playerExp = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Experience']").InnerText.Trim();
                p.playerKills = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Kill']").InnerText.Trim();
                p.playerGamePlayed = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Game Played']").InnerText.Trim();
                p.playerRageQuit = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Rage Quit']").InnerText.Trim();
                p.playerKD = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='K/D']").InnerText.Trim();
                p.playerClan = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Clan']").InnerText.Trim();

                p.playerName = Regex.Replace(p.playerName, pattern, "").Trim();
                p.playerRank = Regex.Replace(p.playerRank, pattern, "").Trim();
                p.playerExp = Regex.Replace(p.playerExp, pattern, "").Trim();
                p.playerKills = Regex.Replace(p.playerKills, pattern, "").Trim();
                p.playerGamePlayed = Regex.Replace(p.playerGamePlayed, pattern, "").Trim();

                p.playerRageQuit = Regex.Replace(p.playerRageQuit, pattern, "").Trim();
                p.playerKD = Regex.Replace(p.playerKD, pattern, "").Trim();
                p.playerClan = Regex.Replace(p.playerClan, pattern, "").Trim();

            }
            catch
            {
                p.playerName = "Error on fetching data";
            }
            try
            {
                p.playerWeaponRank = getweaponrank(playername, p);
                p.playerWeaponRank = Regex.Replace(p.playerWeaponRank, pattern, "").Trim();
            }
            catch
            {
                p.playerWeaponRank = "error in fetching rank";
            }
            try
            {
                getheadshotanddratio(playername, p);
            }
            catch
            {
                p.playerHeadshots = "trouble getting data";
                p.playerHeadshotsRatio = "trouble gettimng ratio";

            }

            // section[@id = "facts"]//td[@data-label="Rank"]
            return p;
        }

        public string getweaponrank(string playername, Player p)
        {

            string res1 = "error";
            try
            {
                BrowserSession b = new BrowserSession();
                b.Get("https://www.gaming-style.com/RushTeam/index.php?page=RankingWr");
                b.FormElements["user"] = playername;
                var res = b.Post("https://www.gaming-style.com/RushTeam/index.php?page=RankingWr");
                p.Weapon_RaceRatio = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Win Ratio']/text()").InnerText.Replace("/t", "").Trim();
                p.Weapon_RaceWins = res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Game Win']/text()").InnerText.Replace("/t", "").Trim();
                return res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Rank']/text()").InnerText.Replace("/t", "").Trim();
            }
            catch
            {
                p.Weapon_RaceWins = "Not Active";
                p.Weapon_RaceRatio = "Not Active";
                return res1;
            }


        }
        public List<string> getheadshotanddratio(string playername, Player x)
        {
            List<string> response = new List<string>();
            try
            {

                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.gaming-style.com/RushTeam/Index.php?page=NewRanking&Player=" + playername);
                var names = doc.DocumentNode.SelectNodes("//li[contains(text(),'HeadShot')]").ToList();
                foreach (var data in names)
                {
                    if (data.InnerText.Contains("HeadShot Ratio"))
                    {
                        x.playerHeadshotsRatio = data.InnerText.Split(':').Last().Trim();
                    }
                    else
                    {
                        x.playerHeadshots = data.InnerText.Split(':').Last().Trim();
                    }
                }
                x.last_seen = doc.DocumentNode.SelectSingleNode("//div[contains(text(),'Has ') and contains(text(),'since')]").InnerText.Replace("/t", "").Trim();
                x.registered_since = doc.DocumentNode.SelectSingleNode("//div[contains(text(),'Registered since')]").InnerText.Replace("/t", "").Trim();
                if (x.playerName.ToLower().Contains("umnik"))
                {
                    x.avatar_url = "https://cdn.discordapp.com/attachments/695018753884422154/696459753710157945/unknown-1.png";
                }
                //else if(x.playerName.ToLower().Contains("xsayyahx")|| x.playerName.ToLower().Contains("chewbacca"))
                //    {
                //    x.avatar_url = "https://cdn.discordapp.com/attachments/688507451632648218/709187868131721277/trst.jpg";
                //}
                else if (x.playerName.ToLower().Contains("igroot") || x.playerName.ToLower().Contains("mrbravo"))
                {
                    x.avatar_url = "https://cdn.discordapp.com/attachments/688507451632648218/709401575529119835/unknown.png";
                }
                else
                {
                    x.avatar_url = "https://www.gaming-style.com/RushTeam/" + doc.DocumentNode.SelectSingleNode("//img[contains(@class,'img-responsive center-block')]").Attributes["src"].Value;
                }
                x.level_progress = doc.DocumentNode.SelectSingleNode("//li[contains(@class,'strength')]").InnerText.Replace("%", "").Trim().Trim();
                x.playerDeath = doc.DocumentNode.SelectSingleNode("//li[contains(text(),'Deaths :')]").InnerText.Split(':').Last().Trim().Trim();
                try
                {
                    x.onlinefromserver = doc.DocumentNode.SelectSingleNode("//font[contains(text(),'Online')]").InnerText.Split('-').Last().Trim();
                }
                catch
                {
                    x.onlinefromserver = "Offline";
                }
            }
            catch
            {
                x.playerHeadshots = "trouble getting profile";
            }
            return response;
            // return res.DocumentNode.SelectSingleNode("//section[@id='facts']//td[@data-label='Rank']/text()").InnerText.Replace("/t", "");
        }

        public List<Weapon> getIndividualWeaponRank(string type, string playername)
        {
            List<Weapon> w = new List<Weapon>();
            string url = gettingWeopanbaseurl(type);
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            var names = doc.DocumentNode.SelectNodes("//div[contains(@class, 'box')]").ToList();
            foreach (var data in names)
            {
                string x = data.ParentNode.Attributes["href"].Value.Split('?')[1];
                Weapon n = GetWeapon(playername, x);
                n.weaponName = data.SelectSingleNode(".//div[contains(@class, 'war-weapon')]").InnerText.Trim().Split('-')[1];
                w.Add(n);
            }

            return w;
        }
        public string gettingWeopanbaseurl(string type)
        {
            string url = "";
            if (type == "melee")
            {
                url = "https://www.gaming-style.com/RushTeam/index.php?page=MeleeChoice";
            }
            if (type == "grenade")
            {
                url = "https://www.gaming-style.com/RushTeam/index.php?page=GrenadeChoice";
            }
            if (type == "pistol")
            {
                url = "https://www.gaming-style.com/RushTeam/index.php?page=PistolChoice";
            }
            if (type == "rifle")
            {
                url = "https://www.gaming-style.com/RushTeam/index.php?page=RifleChoice";
            }
            if (type == "snipe")
            {
                url = "https://www.gaming-style.com/RushTeam/index.php?page=SnipeChoice";
            }


            return url;
        }
        public Weapon GetWeapon(string player, string url)
        {
            Weapon w = new Weapon();
            string fullurl = "https://www.gaming-style.com/RushTeam/Index.php?" + url;
            BrowserSession b = new BrowserSession();
            b.Get(fullurl);
            b.FormElements["user"] = player;
            var res = b.Post(fullurl);
            w.weaponRank = res.DocumentNode.SelectSingleNode("//td[@data-label='Rank']/text()").InnerText.Trim();
            w.TotalKill = res.DocumentNode.SelectSingleNode("//td[@data-label='Kill']/text()").InnerText.Trim();
            w.WPlayername = player;
            return w;
        }
        public Weapon GetWeaponsingle(string player, string url)
        {
            Weapon w = new Weapon();
            string fullurl = url;
            BrowserSession b = new BrowserSession();
            b.Get(fullurl);
            b.FormElements["user"] = player;
            var res = b.Post(fullurl);
            w.weaponRank = res.DocumentNode.SelectSingleNode("//td[@data-label='Rank']/text()").InnerText.Trim();
            w.TotalKill = res.DocumentNode.SelectSingleNode("//td[@data-label='Kill']/text()").InnerText.Trim();
            w.WPlayername = player;
            w.weaponName_imgurl = "https://www.gaming-style.com/RushTeam/" + res.DocumentNode.SelectSingleNode("//div[contains(@class,'war-weapon')]/img").Attributes["src"].Value;
            return w;
        }
        public static string getnextKDadvance(decimal currentkills, decimal currentdeath, string kd1)
        {
            try
            {
                double tokd = Convert.ToDouble(kd1) + 0.01;
                int desiredkd = Convert.ToInt32(kd1.Split('.')[0]) + 1;
                int desirekdadv = desiredkd + 1;
                decimal kd = currentkills / currentdeath;
                decimal kdrounded = Math.Round(kd, 4);
                int percentagedone = Convert.ToInt32(kdrounded.ToString().Split('.')[1]);
                decimal x;
                x = ((currentdeath * Convert.ToDecimal(tokd)) - currentkills);

                decimal y = (Convert.ToDecimal(desiredkd) - Convert.ToDecimal(tokd));
                decimal y1 = (Convert.ToDecimal(desirekdadv) - Convert.ToDecimal(tokd));
                Int64 z = Convert.ToInt64(x / y);
                Int64 z1 = Convert.ToInt64(x / y1);
                return "\n  (To Get **" + tokd + "K/D **  you need to kill **" + z * desiredkd + "** with rate of ** " + desiredkd + " KD ** OR **" + z1 * desirekdadv + "** Kills with rate of ** " + desirekdadv + " KD **)";
            }
            catch
            {
                return "something went wrong";
            }
        }
        public List<string> getonline(string clanName)
        {
            string clanname = "[DevilsPainbrush]";
            if (clanName != "")
            {
                clanName.Replace("[", "").Replace("]", "");
                clanname = string.Format("[{0}]", clanName);
            }

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.gaming-style.com/RushTeam/index.php?page=Clan&ShowClan=" + clanname);
            var names = doc.DocumentNode.SelectNodes("//td[@data-label='Nickname']/a").ToList();
            List<string> onlineplayers = new List<string>();
            foreach (var data in names)
            {

                Player p = new Player();
                string player;
                player = data.InnerText;
                var test = data.ParentNode.ParentNode.Descendants();
                foreach (var h in test)
                {
                    if (h.Name == "img")
                    {

                        if (h.Attributes["class"].Value == "imgOnOff")
                        {
                            if (!h.Attributes["src"].Value.Contains("OffLine"))
                                onlineplayers.Add(player);
                        }

                    }

                }
            }
            return onlineplayers;
        }
    }

}