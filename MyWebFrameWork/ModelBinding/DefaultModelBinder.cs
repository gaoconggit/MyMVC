using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace MVCDemo
{
    public class DefaultModelBinder : IModelBinder
    {
        /// <summary>
        /// 绑定值类型
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public object GetBindInstanceForValueType(string modelName, Type modelType)
        {
            object modelInstance = Activator.CreateInstance(modelType);
            object propertyValue;
            if (GetValueTypeInstance(modelName, modelType, out propertyValue))
            {
                modelInstance = propertyValue;
            }
            return modelInstance;
        }
        /// <summary>
        /// 绑定String类型
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public object GetBindInstanceForStringType(string modelName, Type modelType)
        {
            object modelInstance = Activator.CreateInstance(modelType, "".ToArray());
            object propertyValue;
            if (GetValueTypeInstance(modelName, modelType, out propertyValue))
            {
                modelInstance = propertyValue;
            }
            return modelInstance;
        }
        /// <summary>
        /// 绑定实体类型
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public object GetBindInstanceForEntityType(string modelName, Type modelType)
        {
            object modelInstance = Activator.CreateInstance(modelType);
            foreach (PropertyInfo property in modelType.GetProperties())
            {
                object propertyValue;
                if (GetValueTypeInstance(property.Name,
                    property.PropertyType, out propertyValue))
                {
                    property.SetValue(modelInstance, propertyValue, null);
                }
            }
            return modelInstance;
        }


        public object GetBindInstanceByModelTypeAndModelName(string modelName, Type modelType, Func<string, Type, object> GetBindInstance)
        {
            return GetBindInstance(modelName, modelType);
        }
        public object BindModel(string modelName, Type modelType)
        {
            if (modelType.IsValueType == true)//值类型直接赋值
            {
                return GetBindInstanceByModelTypeAndModelName(modelName, modelType, GetBindInstanceForValueType);
            }
            else
            {
                if (typeof(String) == modelType)//特殊的类型String
                {
                    return GetBindInstanceByModelTypeAndModelName(modelName, modelType, GetBindInstanceForStringType);
                }
                else//实体类
                {
                    return GetBindInstanceByModelTypeAndModelName(modelName, modelType, GetBindInstanceForEntityType);
                }
            }
        }
        private bool GetValueTypeInstance(string modelName, Type modelType, out object value)
        {
            Dictionary<string, object> dataSource = new Dictionary<string, object>();
            //数据来源一：HttpContext.Current.Request.Form
            foreach (string key in HttpContext.Current.Request.Form)
            {
                if (dataSource.ContainsKey(key.ToLower()))
                {
                    continue;
                }
                dataSource.Add(key.ToLower(), HttpContext.Current.Request.Form[key]);
            }

            //数据来源二：HttpContext.Current.Request.QueryString
            foreach (string key in HttpContext.Current.Request.QueryString)
            {
                if (dataSource.ContainsKey(key.ToLower()))
                {
                    continue;
                }
                dataSource.Add(key.ToLower(), HttpContext.Current.Request.QueryString[key]);
            }

            if (dataSource.TryGetValue(modelName.ToLower(), out value))
            {
                value = Convert.ChangeType(value, modelType);
                return true;
            }
            return false;
        }
    }
}