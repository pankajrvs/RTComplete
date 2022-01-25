using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RT.Data;
using RT.Api.Operations;

namespace RT.Api.Controllers
{
    
    [ApiController]
    public class RTController : ControllerBase
    {
        Ioperations _operation;
        public RTController(Ioperations operation)
        {
            _operation = operation;
        }
        [HttpGet]
        [Route("api/rt/GetPlayer")]
        public Player GetPlayer( string playername)
        {
            Player p = new Player();
            p = _operation.setplayer(playername);
                return p;
        }
        [HttpGet]
        [Route("api/rt/GetOnlinePlayers")]
        public List<string> GetOnlinePlayers(string clan)
        {
            List<string> onlineplayers = new List<string>();
            onlineplayers = _operation.getonline(clan);
            return onlineplayers;
        }
    }
}
