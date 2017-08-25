
# 写自己的asp.net框架

### 一、注意事项：
#### 1.服务的要求是一个C#类
#### 2.服务以Server结尾
#### 3.访问时Server可以省略,如HomeServer,访问时可写为Home

### 原理：
  `Web框架基于asp.net管线机制,用HttpModule注册了HttpApplicationr的PostResolveRequestCache事件`
  `当PostResolveRequestCache事件触发时,走HttpModule方法中定义的方法。`
  `HttpModule为一个简单的路由,解析/*Server/*类似规则的url。找到了就分配对应的HttpHandler。`
## 二、匹配对应的方法：
* 1.用一个静态构造方法和一个静态字典缓存以Server结尾的类型信息。
## 三、调对应的后台方法
* 1.用url中得到的Server和Action调对应的类、对应的方法

## 功能还不完善。只有基础功能。目前仅能当作web服务使用

### 1.视图引擎没有
### 2.action缓存没写
.......


## 四、使用说明：
* 1、添加	MyWebFrameWork.dll 引用


* 2、配置：

      <system.webServer>
      <validation validateIntegratedModeConfiguration="false"/>
      <modules>
        <remove name="ServerHttpModule"/>
        <add name="ServerHttpModule" type="MyWebFrameWork.HttpModule.ServerHttpModule,MyWebFrameWork"/>
      </modules>
      </system.webServer>

* 3、使用：
  添加以Server结尾的类。使用方法和asp.net mvc一样了。

  c#类

      public class HomeServer
        {
            public string Index(Student stu)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(stu);
            }
        }
