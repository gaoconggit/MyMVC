using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyWebFrameWork.Route
{
    public interface IWebRoute
    {
        /// <summary>
        /// 分配HttpHandler
        /// </summary>
        void distributionHttpHandler(string url, HttpContext context);
    }
}
