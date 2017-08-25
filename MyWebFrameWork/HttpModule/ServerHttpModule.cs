using MyWebFrameWork.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyWebFrameWork.HttpModule
{
    public class ServerHttpModule : IHttpModule
    {
        public void Dispose()
        {
          
        }
        public void PostResolveRequestCache(object sender, EventArgs e)
        {
            //得到http上下文
            HttpContext context = (sender as HttpApplication).Context;
            //当前url
            string currentUrl = context.Request.RawUrl;
            IWebRoute route = new SimpleRoute();
            //url过路由
            route.distributionHttpHandler(currentUrl, context);




        }
        public void Init(HttpApplication context)
        {
            //注册管线事件
            context.PostResolveRequestCache += new EventHandler(PostResolveRequestCache);
        }
    }
}