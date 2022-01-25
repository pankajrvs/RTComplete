using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RT.Data;
using RT.Api.Operations;

namespace RT.Api.Controllers
{
    
    [ApiController]
    public class RTController : ControllerBase
    {
        [HttpGet]
        [Route("api/rt/GetPlayer")]
        public Player GetPlayer( string playername)
        {
            operations o = new operations();
            Player p = new Player();
            p = o.setplayer(playername);
                return p;
        }
        [HttpGet]
        [Route("api/rt/GetOnlinePlayers")]
        public List<string> GetOnlinePlayers(string clan)
        {
            List<string> onlineplayers = new List<string>();
            operations o = new operations();
            onlineplayers = o.getonline(clan);
            return onlineplayers;
        }
    }
}
