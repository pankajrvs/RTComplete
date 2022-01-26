using HtmlAgilityPack;
using System.Net;

namespace RT.Api.Operations
{
    public interface IBrowserSession
    {
        CookieCollection Cookies { get; set; }
        FormElementCollection FormElements { get; set; }

        string Get(string url);
        HtmlDocument Post(string url);
    }
}