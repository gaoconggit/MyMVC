using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo
{
    public interface IModelBinder
    {
        object BindModel(string modelName, Type modelType);
    }
}