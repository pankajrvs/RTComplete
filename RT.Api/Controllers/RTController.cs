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
        public Player GetPlayer(string playername)
        {
            return _operation.getPlayer(playername); ;
        }
        [HttpGet]
        [Route("api/rt/GetOnlinePlayers")]
        public List<string> GetOnlinePlayers(string clan = "DevilsPainbrush")
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
        [HttpPost]
        [Route("api/moosend/test")]
        public async Task<string> testAsync(string zip = "00000")
        {
            var baseAddress = new Uri("https://api.moosend.com/v3");
            var maillistid = "58ee3976-7622-4d25-8dd1-082a3afd3d85";
            var subscriberid = "628f214a-6ea5-7572-d91e-20f7622fb148";
            var apikey = "1178ac30-8714-44b7-9094-7185289fd37a";

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                try
                {

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                    using (var content = new StringContent("{  \"Name\": \"pankaj prasad\",  \"Email\": \"pprasad@horizontal.com\",  \"CustomFields\": [    \"ZipCode="+zip+"\"  ]}", System.Text.Encoding.Default, "application/json"))
                    {
                        using (var response = await httpClient.PostAsync(baseAddress+"/subscribers/" + maillistid + "/update/" + subscriberid + ".json?apikey=" + apikey, content))
                        {
                            string responseData = await response.Content.ReadAsStringAsync();
                            return responseData;
                        }
                    }
                }
                catch
                {
                    return "faild";
                }

                
            }
        }
    }
}