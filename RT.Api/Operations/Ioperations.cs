using RT.Data;

namespace RT.Api.Operations
{
    public interface Ioperations
    {
        List<string> getheadshotanddratio(string playername, Player x);
        List<Weapon> getIndividualWeaponRank(string type, string playername);
        List<string> getonline(string clanName);
        string gettingWeopanbaseurl(string type);
        Weapon GetWeapon(string player, string url);
        string getweaponrank(string playername, Player p);
        Weapon GetWeaponsingle(string player, string url);
        List<Player> loadplayers(string players);
        void setapplicationsvariable(string name, List<Player> value);
        Player getPlayer(string playername);
    }
}