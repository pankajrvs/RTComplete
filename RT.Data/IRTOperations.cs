using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Data
{
    public interface IRTOperations
    {
        Player GetPlayer(string PlayerName);
        List<string> GetOnlineplayers(string ClanName);
    }
}
