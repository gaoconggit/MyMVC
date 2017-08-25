
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;

namespace MyWebFrameWork.Common
{
    public static class ServerHelp
    {
        //Server缓存字典
        public static Dictionary<string, ServerDescription> ServiceDict;
        static ServerHelp()
        {
            InitServers();
        }
        public static void InitServers()
        {
            List<ServerDescription> serviceList = new List<ServerDescription>();
            ICollection assemblies = BuildManager.GetReferencedAssemblies();
            //找出所有以Service结尾的类
            foreach (Assembly assembly in assemblies)
            {
                // 过滤以【System】开头的程序集，加快速度
                if (assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase))
                    continue;
                foreach (Type t in assembly.GetExportedTypes())
                {
                    if (t.IsClass == false)
                        continue;
                    if (t.Name.EndsWith("Server", StringComparison.OrdinalIgnoreCase))
                        serviceList.Add(new ServerDescription(t));
                }
            }
            ServiceDict = serviceList.ToDictionary(x => x.ServerType.Name, StringComparer.OrdinalIgnoreCase);
            ////短名称
            //ServiceDict = new Dictionary<string, ServerDescription>(serviceList.Count, StringComparer.OrdinalIgnoreCase);
            //foreach (ServerDescription description in serviceList)
            //{
            //    ServiceDict.Add(description.ServerType.Name, description);
            //}
        }
        public static ServerDescription GetServiceController(string controller)
        {
            if (string.IsNullOrEmpty(controller))
                throw new ArgumentNullException("controller");
            //容错处理
            if (!controller.EndsWith("Server", StringComparison.OrdinalIgnoreCase))
            {
                controller += "Server";
            }
            ServerDescription description = null;
            ServiceDict.TryGetValue(controller, out description);
            return description;
        }
    }
}