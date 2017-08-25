using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebFrameWork.Common
{
    public class ServerDescription
    {
        public Type ServerType { get; private set; }

        public ServerDescription(Type type)
        {
            this.ServerType = type;
        }
    }


}
