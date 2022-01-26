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
            return _operation.getPlayer(playername); ;
        }
        [HttpGet]
        [Route("api/rt/GetOnlinePlayers")]
        public List<string> GetOnlinePlayers(string clan= "DevilsPainbrush")
        {
            return _operation.getonline(clan); ;
        }
        [HttpGet]
        [Route("api/rt/GetPolibilicy")]
        public Player GetPolibilicy(string player = "pankaj")
        {
            Poliblicy p = new Poliblicy();
            return p.getPoliblicyProfile(player); ;
        }
    }
}
