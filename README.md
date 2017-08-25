## 一、注：
### 1.服务的要求是一个C#类
### 2.服务以Server结尾
### 3.访问时Server可以省略,如HomeServer,访问时可写为Home

原理：
Web框架基于asp.net管线机制,用HttpModule注册了
HttpApplicationr的PostResolveRequestCache事件。
当PostResolveRequestCache事件触发时,走HttpModule方法中定义的方法。

HttpModule为一个简单的路由,解析/*Server/*类似规则的url。找到了就分配对应的
HttpHandler。

## 二、匹配对应的方法：
用一个静态构造方法和一个静态字典缓存以Server结尾的类型信息。
三、调对应的后台方法
用url中得到的Server和Action调对应的类、对应的方法

功能还不完善。只有基础功能。

### 1.视图引擎没有
### 2.action缓存没写
.......


## 三、使用说明：
添加	MyWebFrameWork.dll 引用


配置：
 <system.webServer>
    <validation validateIntegratedModeConfiguration="false"/>
    <modules>
     

      <remove name="ServerHttpModule"/>
      <add name="ServerHttpModule" type="MyWebFrameWork.HttpModule.ServerHttpModule,MyWebFrameWork"/>
      
    </modules>
  </system.webServer>

  使用：
  添加以Server结尾的类。使用方法和asp.net mvc一样了。
