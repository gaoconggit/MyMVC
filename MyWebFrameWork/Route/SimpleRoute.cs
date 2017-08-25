using LandMVC;
using MVCDemo;
using MyWebFrameWork.Common;
using MyWebFrameWork.HttpHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyWebFrameWork.Route
{
    public class SimpleRoute : IWebRoute
    {
        public void distributionHttpHandler(string url, HttpContext context)
        {
            if (url == "/")
            {
                ExceptionHelper.Throw404Exception(context);
            }
            else
            {
                Regex reg = new Regex(@"/(?<Server>[^\s]+(?:Server)?)/(?<Action>[^\s?]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match match = reg.Match(url);
                if (match.Success)
                {
                    //分配httpHandler
                    context.RemapHandler(new ServerHttpHandler(new ServerActionPair()
                    {
                        Server = match.Groups["Server"].Value,
                        Action = match.Groups["Action"].Value
                    }, 
                    new DefaultModelBinder()
                    ));
                }
                else
                {
                    ExceptionHelper.Throw404Exception(context);
                }
            }
        }
    }
}