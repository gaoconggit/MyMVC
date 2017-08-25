using LandMVC;
using MVCDemo;
using MyWebFrameWork.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MyWebFrameWork.HttpHandler
{
    public class ServerHttpHandler : IHttpHandler
    {
        public ServerActionPair currentServerActionPair { get; set; }
        private IModelBinder _modelBinder;
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
        public ServerHttpHandler()
        {

        }
        public ServerHttpHandler(ServerActionPair pair, IModelBinder modelBinder)
        {
            this.currentServerActionPair = pair;
            this._modelBinder = modelBinder;
        }
        public void ProcessRequest(HttpContext context)
        {
            //执形对应服务的方法
            ServerDescription serverTypeDescription = ServerHelp.GetServiceController(this.currentServerActionPair.Server);
            if (serverTypeDescription == null)
            {
                ExceptionHelper.Throw404Exception(context);
            }
            //服务对应的Type
            Type type = serverTypeDescription.ServerType;
            //得到方法
            MethodInfo method = type.GetMethod(this.currentServerActionPair.Action,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            if (method == null)
            {
                ExceptionHelper.Throw404Exception(context);
            }
            List<object> parameters = new List<object>();
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                parameters.Add(this._modelBinder.BindModel(parameter.Name, parameter.ParameterType));
            }

            object obj = Activator.CreateInstance(type);

            object content = method.Invoke(obj, parameters.ToArray());
            context.Response.ContentType = "text/plain";
            context.Response.Write(content);
        }

    }
}