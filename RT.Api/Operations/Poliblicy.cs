using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net;
using RT.Data;

namespace RT.Api.Operations
{
    public class Poliblicy
    {
        HttpClient httpClient;
        string url = "https://www.gaming-style.com/POLYBLICY/index.php?page=ranking";
        public Poliblicy()
        {
            //Create the request sender object
            httpClient = new HttpClient();

            //Set the headers
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("MessageService/3.1");

        }

        public Player getPoliblicyProfile(string playerName)
        {
            Player p = new Player();


            var formContent = new FormUrlEncodedContent(new[]
             {
                    new KeyValuePair<string, string>("Playername", playerName),
              });
            var response2 = httpClient.PostAsync(url, formContent).Result;
            var contents = response2.Content.ReadAsStringAsync().Result;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(contents);
            try
            {
                p.playerName = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='Nickname']").InnerText.Trim();
                p.playerRank = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='Rank']").InnerText.Trim();
                p.playerExp = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='Experience']").InnerText.Trim();
                p.playerKills = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='Kill']").InnerText.Trim();
                p.playerDeath = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='Death']").InnerText.Trim();
                p.playerKD = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='K/D']").InnerText.Trim();
                p.playerClan = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='Clan']").InnerText.Trim();
                p.playerLevel = Convert.ToInt32(doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'Mytable ')]//td[@data-label='Level']//div").Attributes["title"].Value.Replace("Level", "").Trim());
            }
            catch
            {
                p.playerName = Constants.errorMessage;
            }
            return p;
        }


    }

}
