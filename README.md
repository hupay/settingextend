# settingextend
[![CI](https://github.com/huxb-home/settingextend/workflows/.NET%20Core/badge.svg)](https://github.com/huxb-home/settingextend)<br/>
灵感来源于公司的一个公共类库，想对这个进行重新设计。实现配置内容的引用，让配置更简单、灵活。经过处理后获得一串字符串，用于页面展示。或者获得一种数据类型（目前只有数组和字典），用于代码编写。

# 存在的问题
- 代码整体逻辑使用字符串的查找匹配，希望后续能找到更好的方法来代替。
- 语法设计较简陋，后续会持续完善。

# 语法
将配置值划分为头部和配置两部分。头部是声明此配置的类型，目前有类型、引用共两种。<br/>
头部通过关键词来识别。
- #开头，表示注释，不会解析
- 空行，会被过滤
## 头部语法
### 类型
分为数组、字典和代码。语法简单，申明只有顶部一行。语法如下：<br/>
数组：
```
# 这是一个数组，解析对象中为字符串数组
type array

33
年龄
abc
```
字典：
```
# 这是一个字典，解析对象中键值均为字符串
type dictionary

user=用户
admin=管理员
editor=编辑
```
代码：
```
# 这是代码示例，解析对象中值为字符串。通过```print```函数输出，多个输出最后用换行符连接。
# 目前功能简陋，还不清晰用途。
# 语法与.NET语法保持一致。暂时不支持命名空间和类库引用，在代码文本混合类型中支持
type code

print("输出1");
print(DateTime.Now.ToString("yyyy-MM-dd"));
```
### 引用
此类输出对象值为字符串。设计用途为公共页面渲染。<br/>
注意：dll看起来不用引用，在当前目录下会识别。<br/>
语法如下：
```
import [path|dll|namespace] [路径|dll名称|命名空间] [变量名]
```
### 变量
会引起代码中变量冲突，暂时不实现。

# 使用
在“appsettings.json”中新增“settingextend.provider”键，值为对应供应者的全部typename。见单元测试。<br/>
目前单元测试里提供了一种基于文件的配置供应者。通过扩展，理论上支持etcd、zookeeper这些统一配置服务。
```json
{
  // 配置供应者配置
  "settingextend.provider": "SettingExtend.Test.FileConfigProvider,SettingExtend.Test",
  "FileConfigPath": "../../../../../doc/FileConfig"
}
```