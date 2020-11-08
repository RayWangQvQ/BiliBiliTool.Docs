# 配置说明

## 1.配置方式

### 1.1.方式一：修改appsettings.json文件
推荐在本地运行的朋友使用，直接打开文件，将对应的配置值填入，保存即可生效。
### 1.2.方式二：命令行参数启动
如下所示：

```
dotnet run -p ./src/Ray.BiliBiliTool.Console -userId=123 -sessData=456 -biliJct=789 -numberOfCoins=5
```

使用`-key=value`的形式拼接就行了。具体有哪些key，这些key又对应上面appsettings.json里的哪个配置，可参见[Constants.cs](https://github.com/RayWangQvQ/BiliBiliTool/blob/main/src/Ray.BiliBiliTool.Config/Constants.cs) 中的CommandLineMapper。

### 1.3.方式三：托管在GitHub Actions上，使用GitHub Secrets配置

使用GitHub Actions，除了三个必填的Cookie是每个对应一个Secret外，其他的所有配置都可以通过添加名为“OTHERCONFIGS”的Secret的实现，其值为多个配置的拼接。

比如，配置微信推送的SCKEY：
Secret Key：`OTHERCONFIGS`
Secret Value：`-pushScKey=123456`

如果有多个，比如既要配微信推送，又要配置优先投币的up主，往后拼接即可：
Secret Key：`OTHERCONFIGS`
Secret Value：`-pushScKey=123456 -supportUpIds=123,456`

这个Value会拼接到启动的命令行之后，作为命令行参数传入程序当中，所以使用GitHub Secrets配置的本质还是使用命令行参数配置。

## 2.配置详细信息

//todo

