# 配置说明

## 1.配置方式

### 1.1.方式一：修改配置文件
推荐使用Release包在本地运行的朋友使用，直接打开文件，将对应的配置值填入，保存即可生效。

默认有3个配置文件：`appsettings.json`、`appsettings.Development.json`、`appsettings.Production.json`，分别对应默认、开发与生产环境。

如果运行环境为开发环境，则`appsettings.Development.json`优先级高于`appsettings.json`，即`appsettings.Development.json`里的配置会覆盖默认配置（不是全部覆盖，`appsettings.Development.json`里加了几个就覆盖几个）；

如果运行环境为生产环境，则`appsettings.Production.json`优先级高于`appsettings.json`，即`appsettings.Production.json`里的配置会覆盖默认配置（同样不是全部覆盖，`appsettings.Production.json`里加了几个就覆盖几个）。

对于不是开发人员的大部分人来说，只需要关注`appsettings.Production.json`即可，因为非调试状态下运行的默认环境就是生产环境。此时如需自定义配置，推荐做法是，将`appsettings.json`的内容全部拷贝进`appsettings.Production.json`当中，然后在`appsettings.Production.json`文件中进行修改（并且以后都只修改`appsettings.Production.json`文件，`appsettings.json`只作为默认默认模板而存在）

### 1.2.方式二：命令行参数启动
仅以自包含运行环境的 Windows 版本为例（其他版本同理，参见 README.md 章节 1.2.2），运行命令：

```
Ray.BiliBiliTool.Console.exe -userId=123 -sessData=456 -biliJct=789 -numberOfCoins=5
```

在启动时使用`-key=value`的形式拼接，具体有哪些key，这些key又对应上面appsettings.json里的哪个配置，可参见[Constants.cs](https://github.com/RayWangQvQ/BiliBiliTool/blob/main/src/Ray.BiliBiliTool.Config/Constants.cs) 中的CommandLineMapper，下面罗列每个配置项时也会给出示例。

### 1.3.方式三：托管在GitHub Actions上，使用GitHub Secrets配置

使用GitHub Actions，可以通过添加Secret实现配置。

比如，配置微信推送的SCKEY，可以添加如下Secret：

Secret Name：`PUSHSCKEY`

Secret Value：`123abc`

这些 Secrets 会通过 workflow 里的 yml 脚本映射为环境变量，在应用启动时作为环境变量配置源传入程序当中，所以使用GitHub Secrets配置的本质是使用环境变量配置。

## 2.配置详细信息

### 2.1.三个必须的Cookie
这三个配置是必填项。
#### 2.1.1.BiliJct
|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | Cookie中的bili_jct项 |
| 值域   | 字符串 |
| 默认值   | 空 |
| 命令行示范   | `-biliJct=123` |
| GitHub Secrets   | Name:`BILIJCT`  Value: `123`|
#### 2.1.2.SessData
|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | Cookie中的SESSDATA项 |
| 值域   | 字符串 |
| 默认值   | 空 |
| 命令行示范   | `-sessData=123` |
| GitHub Secrets   | Name:`SESSDATA`  Value: `123`|
#### 2.1.3.UserId
|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | Cookie中的DEDEUSERID项 |
| 值域   | 字符串 |
| 默认值   | 空 |
| 命令行示范   | `-userId=123` |
| GitHub Secrets   | Name:`USERID`  Value: `123`|

### 2.2.安全相关的配置
#### 2.2.1.IsSkipDailyTask（是否跳过执行任务）
用于特殊情况下，通过配置灵活的开启和关闭任务.
配置为关闭后，程序会跳过整个每日任务，不会调用B站任何接口。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 是否跳过执行任务 |
| 值域   | [true,false] |
| 默认值   | false |
| 命令行示范   | 暂未开放命令行 |
| GitHub Secrets   | Name:`ISSKIPDAILYTASK`  Value: `true`|

#### 2.2.2.IntervalSecondsBetweenRequestApi（两次调用B站Api之间的间隔秒数）
因为有朋友反馈，程序在1到2秒内连续调用B站的Api过快，担心会被B站的安全策略检测到，影响自己的账号安全。

所以我添加这个安全策略的配置，可以设置两次Api请求之间的最短时间间隔。

举例来说，之前的5次投币可能是在1秒之内完成的，现在通过配置间隔时间，可以将其变为投币一次后，经过4到5秒才会投下一个，提升程序的演技，让它表现的就像真人在投币一样，骗过BiliBili~ 

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 两次调用B站Api之间的间隔秒数 |
| 值域   | [0,+] |
| 默认值   | 3 |
| 命令行示范   | `-intervalSecondsBetweenRequestApi=10` |
| GitHub Secrets   | Name:`INTERVALSECONDSBETWEENREQUESTAPI`  Value: `10`|


#### 2.2.3.IntervalMethodTypes（间隔秒数所针对的HttpMethod）
间隔秒数所针对的HttpMethod类型，服务于上一个配置。服务器一般对GET请求不是很敏感，建议只针对POST请求做间隔就可以了。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 两次调用B站Api之间的间隔秒数 |
| 值域   | [GET,POST]，多个以英文逗号分隔 |
| 默认值   | POST |
| 命令行示范   | `-intervalMethodTypes=GET,POST` |
| GitHub Secrets   | Name:`INTERVALMETHODTYPES`  Value: `GET,POST`|

#### 2.2.4.UserAgent（请求B站接口时头部传递的User-Agent）
近期出现登录接口报错412（[#61](https://github.com/RayWangQvQ/BiliBiliTool/issues/61)）,有朋友发现通过修改UA可以暂时解决问题，所以开放为了配置。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 请求B站接口时头部传递的User-Agent |
| 值域   | 字符串，可以F12从自己的浏览器获取 |
| 默认值   | Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36 Edg/87.0.664.41 |
| 命令行示范   | 不开放命令行 |
| GitHub Secrets   | Name:`USERAGENT`  Value: `Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36 Edg/87.0.664.41`|

获取浏览器中自己的UA的方法见下图：

![获取User-Agent](https://github.com/RayWangQvQ/BiliBiliTool.Docs/blob/main/imgs/get-user-agent.png)

### 2.3.推送
v1.0.x仅支持推送到Server酱，v1.1.x之后重新定义了推送地概念，将推送仅看作不同地日志输出端，与Console、File没有本质区别。

配置多个，多个端均会收到日志消息。推荐Telegram、企业微信、Server酱。

#### 2.3.1.Telegram机器人
##### 2.3.1.1.botToken
点击 https://core.telegram.org/api#bot-api 查看获取方式。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 用于将日志输出到Telegram机器人 |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHTGTOKEN`  Value: `123456:abcdefg`|

##### 2.3.1.2.chatId
点击 https://api.telegram.org/bot{TOKEN}/getUpdates 获取（用上面获取到的Token替换进链接里的{TOKEN}后访问）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 用于将日志输出到Telegram机器人 |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHTGCHATID`  Value: `654321`|

#### 2.3.2.企业微信机器人
在群内添加机器人，获取到机器人的WebHook地址，添加到配置中。
##### 2.3.2.1.webHookUrl

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 用于将日志输出到企业微信机器人 |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHWEIXINURL`  Value: `abcdefg`|

#### 2.3.3.钉钉机器人
在群内添加机器人，获取到机器人的WebHook地址，添加到配置中。
##### 2.3.3.1.webHookUrl

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 用于将日志输出到钉钉机器人 |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHDINGURL`  Value: `abcdefg`|

#### 2.3.4.Server酱
##### 2.3.4.1.ScKey（从Server酱申请到的微信SCKEY）
Server酱是一个免费的微信推送服务，我们可以去[http://sc.ftqq.com/3.version](http://sc.ftqq.com/3.version)网站下申请一个自己的SCKEY，将这个SCKEY配置到程序中，然后我们使用微信关注Server酱的公众号，之后就可以每天在公众号中收到推送的消息了。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 用于微信推送的SCKEY |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | `-pushScKey=abcdefg` |
| GitHub Secrets   | Name:`PUSHSCKEY`  Value: `abcdefg`|

#### 2.3.5.酷推
https://cp.xuthus.cc/
##### 2.3.5.1.sKey
该平台可能还在完善当中，对接时我发现其接口定义不规范，且机器人容易被封，所以不推荐使用，且不接受提酷推推送相关bug。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 用于将日志输出到QQ |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHCOOLSKEY`  Value: `abcdefg`|

#### 2.3.5.推送到自定义Api
这是我简单封装了一个通用的推送接口，可以推送到任意的api地址，如果有自己的机器人或自己的用于接受日志的api，可以根据需要自定义配置。
#### 2.3.5.1.api

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 自定义用来接受日志的api地址 |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHOTHERAPI`  Value: `abcdefg`|
#### 2.3.5.2.placeholder

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 占位符 |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHOTHERPLACEHOLDER`  Value: `#msg#`|
#### 2.3.5.2.bodyJsonTemplate

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | post发送的body，格式为json字符串 |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | 无 |
| GitHub Secrets   | Name:`PUSHOTHERBODYJSONTEMPLATE`  Value: `{\"content\":#msg#}`|

### 2.4.每日任务相关
#### 2.4.1.NumberOfCoins（每日投币数量）
每天投币的总目标数量，因为投币获取经验只与次数有关，所以程序每次投币只会投1个，也就是说该配置也表示每日投币次数。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 每天投币的总目标数量 |
| 值域   | [0,5]，为安全考虑，程序内部还会做验证，最大不能超过5 |
| 默认值   | 5 |
| 命令行示范   | `-numberOfCoins=3` |
| GitHub Secrets   | Name:`NUMBEROFCOINS`  Value: `3`|

#### 2.4.2.SelectLike（投币时是否同时点赞）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 投币时是否同时点赞 |
| 值域   | [true,false] |
| 默认值   | false |
| 命令行示范   | `-selectLike=true` |
| GitHub Secrets   | Name:`SELECTLIKE`  Value: `true`|

#### 2.4.2.SupportUpIds（优先选择支持的up主Id集合）
专门为强迫症的朋友准备的配置。有人觉得随机选择视频来观看、分享和投币，一则不是自己的真实意愿，二则担心会影响B站对个人的喜好猜测产生偏差，导致以后推荐的视频都并不是自己真正喜欢的。

所以就有这个配置，通过填入自己选择的up主ID，则以后观看、分享和投币，都会优先从配置的up主下面挑选视频，如果没有找到才去其他地方随机挑选视频。

其优先等级是最高的，如果配置了，在投币或观看、分享视频时，会优先从配置的up主中随机获取视频。

程序会最多尝试随机获取10次，如果10均未获取到可投币的视频（比如都已经投过，不能重复投了），则会去你的**特别关注**列表中随机再获取，再然后会去**普通关注**列表中随机获取，最后会去排行榜中随机获取。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 优先选择支持的up主Id集合 |
| 值域   | up主ID，多个用英文逗号分隔，默认是作者本人的UpId，如需删除可以配置为空格字符串或“-1”，也可以配置为其他人的UpId |
| 默认值   | 220893216 |
| 命令行示范   | `-supportUpIds=220893216,17819768,43619319,14583962,44473221,123938419,34858100` |
| GitHub Secrets   | Name:`SUPPORTUPIDS`  Value: `220893216,17819768,43619319,14583962,44473221,123938419,34858100`|

获取UP主的Id方法：打开bilibili，进入欲要选择的UP主主页，在url中和简介中，都可获得该UP主的Id，如下图所示：

![UpId](/imgs/get-up-id.png)

#### 2.4.3.DayOfAutoCharge（每月几号自动充电）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 每月几号自动充电 |
| 值域   | [-1,31]，-1表示不指定，默认月底最后一天；0表示不充电 |
| 默认值   | -1 |
| 命令行示范   | `-dayOfAutoCharge=25` |
| GitHub Secrets   | Name:`DAYOFAUTOCHARGE`  Value: `25`|

#### 2.4.4.AutoChargeUpId（充电对象）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 充电对象的Id |
| 值域   | up的Id字符串，默认是作者本人的UpId；-1表示不指定，为自己充电；其他Id则会尝试为配置的UpId充电 |
| 默认值   | 220893216 |
| 命令行示范   | `-autoChargeUpId=220893216` |
| GitHub Secrets   | Name:`AUTOCHARGEUPID`  Value: `220893216`|

#### 2.4.5.DayOfReceiveVipPrivilege（每月几号自动领取会员权益）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 每月几号自动领取会员权益 |
| 值域   | [-1,31]，-1表示不指定，默认每月1号；0表示不领取 |
| 默认值   | 1 |
| 命令行示范   | `-dayOfReceiveVipPrivilege=2` |
| GitHub Secrets   | Name:`DAYOFRECEIVEVIPPRIVILEGE`  Value: `2`|

#### 2.4.6.IsExchangeSilver2Coin（是否开启直播中心银瓜子兑换硬币）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 是否开启直播中心银瓜子兑换硬币 |
| 值域   | [false,true] |
| 默认值   | true |
| 命令行示范   | `-isExchangeSilver2Coin=false` |
| GitHub Secrets   | Name:`IsExchangeSilver2Coin`  Value: `false`|

### 2.5.日志相关

#### 2.5.1.ConsoleLogLevel（日志输出等级）
这里的日志等级指的是 Console 的等级，即 GitHub Actions 里和微信推送里看到的日志。

为了美观， BiliBiliTool 默认只输出最低等级为 Information 的日志，保证只展示最精简的信息。

但是经过几轮反馈发现，这样会造成 GitHub Actions 运行的朋友遇到异常时无法查看详细日志信息（本地运行的朋友可以通过日志文件看到详细的日志信息）。

所以就将日志等级开放为配置了，通过更改等级，可以指定日志输出的详细程度。

BiliBiliTool 使用 Serilog 作为日志组件，所以其值域与 Serilog 的日志等级选项相同，这里只建议在需要调试时改为`Debug`，应用会输出详细的调试日志信息，包括每次调用B站Api的请求参数与返回数据。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 设置Console输出的日志的详细程度 |
| 值域   | [Infromation,Debug] |
| 默认值   | 1 |
| 命令行示范   | 暂未开放到命令行 |
| GitHub Secrets   | Name:`CONSOLELOGLEVEL`  Value: `Debug`|

#### 2.5.2.ConsoleLogTemplate（日志输出样式）
这里的日志样式指的是 Console 的等级，即 GitHub Actions 里和微信推送里看到的日志。

通过更改模板样式，可以指定日志输出的样式，比如不输出时间和等级，做到最精简的样式。

BiliBiliTool 使用 Serilog 作为日志组件，所以可以参考 Serilog 的日志样式模板。


|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 设置Console输出的日志样式 |
| 值域   | 字符串 |
| 默认值   | `[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}` |
| 命令行示范   | 太长了，不考虑开放到命令行 |
| GitHub Secrets   | Name:`CONSOLELOGTEMPLATE`  Value: `{Message:lj}{NewLine}{Exception}`|

### 2.6.代理
增加代理支持，如果需要请看:
1. 【github action】 : 在secrets中增加`PROXY`,值为代理地址+端口，如`127.0.0.1:10240`
2. 【本地运行或docker】: 设置环境变量`RAY_WebProxy`=`代理地址，格式如上`


#### 关于如何配置为Debug日志模式获取详细的日志信息
第一步，将ConsoleLogLevel配置为`Debug`

第二步，将ConsoleLogTemplate配置为`[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}`

#### 关于如何按环境切换配置
增加指定不同环境来加载配置文件的功能(增加一个自己的避免更新配置被覆盖),仅针对`appsettings.json`中的配置。使用方法:

1. 复制一个`appsettings.json`文件， 改为`appsettings.PRD.json`，中间这个`PRD`你也可以取其它名字，设置环境变量时匹配即可。
2. 删除所有配置，然后把你想要修改的设置项复制过来，修改为你想要的值。
3. 设置环境变量

【github action】 : 在secrets中增加`ENV`,值为刚才取的名字
【本地运行或docker】: 设置环境变量`ASPNETCORE_ENVIRONMENT`=`刚才取的名字`
