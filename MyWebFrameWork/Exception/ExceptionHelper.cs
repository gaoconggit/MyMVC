using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LandMVC
{
    public static class ExceptionHelper
    {
        public static void Throw403Exception(HttpContext context)
        {
            if (context == null)
                throw new HttpException(403, "很抱歉，您没有合适的权限访问该资源。");

            throw new HttpException(403,
                "很抱歉，您没有合适的权限访问该资源：" + context.Request.RawUrl);
        }

        public static void Throw404Exception(HttpContext context)
        {
            if (context == null)
                throw new HttpException(404, "要请求的资源不存在。");

            throw new HttpException(404,
                "没有找到能处理请求的服务类，当前请求地址：" + context.Request.RawUrl);
        }
    }
}
