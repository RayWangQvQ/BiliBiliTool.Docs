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

### 2.1.三个必须的Cookie
前面已经详细说过，这里跳过了。

### 2.2.安全相关的配置

#### 2.2.1.IntervalSecondsBetweenRequestApi（两次调用B站Api之间的间隔秒数）
因为有朋友反馈，程序在1到2秒内连续调用B站的Api过快，担心会被B站的安全策略检测到，影响自己的账号安全。所以我添加这个安全策略的配置，可以设置两次Api请求之间的最短时间间隔。举例来说，之前的5次投币可能是在1秒之内完成的，现在通过配置间隔时间，可以将其变为投币一次后，经过4到5秒才会投下一个，提升程序的演技，让它表现的就像真人在投币一样，骗过BiliBili~ 

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 两次调用B站Api之间的间隔秒数 |
| 值域   | [0,+] |
| 默认值   | 3 |
| 命令行示范   | `-intervalSecondsBetweenRequestApi=10` |


#### 2.2.2.IntervalMethodTypes（间隔秒数所针对的HttpMethod）
间隔秒数所针对的HttpMethod类型，服务于上一个配置。服务器一般对GET请求不是很敏感，建议只针对POST请求做间隔就可以了。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 两次调用B站Api之间的间隔秒数 |
| 值域   | [GET,POST]，多个以英文都好分隔 |
| 默认值   | POST |
| 命令行示范   | `-intervalMethodTypes=GET,POST` |

### 2.3.微信推送

#### 2.3.1.PushScKey（从Server酱申请到的微信SCKEY）
Server酱是一个免费的微信推送服务，我们可以去[http://sc.ftqq.com/3.version](http://sc.ftqq.com/3.version)网站下申请一个自己的SCKEY，将这个SCKEY配置到程序中，然后我们使用微信关注Server酱的公众号，之后就可以每天在公众号中收到推送的消息了。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 用于微信推送的SCKEY |
| 值域   | 一串字符串 |
| 默认值   | 空 |
| 命令行示范   | `-pushScKey=abcdefg` |

### 2.4.每日任务相关
#### 2.4.1.NumberOfCoins（每日投币数量）
每天投币的总目标数量，因为投币获取经验只与次数有关，所以程序每次投币只会投1个，也就是说该配置也表示每日投币次数。

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 每天投币的总目标数量 |
| 值域   | [0,5]，为安全考虑，程序内部还会做验证，最大不能超过5 |
| 默认值   | 5 |
| 命令行示范   | `-numberOfCoins=3` |

#### 2.4.2.SelectLike（投币时是否同时点赞）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 投币时是否同时点赞 |
| 值域   | [true,false]或者[1,0] |
| 默认值   | false |
| 命令行示范   | `-selectLike=1` |

#### 2.4.2.SupportUpIds（优先选择支持的up主Id集合）
专门为强迫症的朋友准备的配置。有人觉得随机选择视频来观看、分享和投币，一则不是自己的真实意愿，二则担心会影响B站对个人的喜好猜测产生偏差，导致以后推荐的视频都并不是自己真正喜欢的。所以就有这个配置，通过填入自己选择的up主ID，则以后观看、分享和投币，都会优先从配置的up主下面挑选视频，如果没有找到才去其他地方随机挑选视频。


|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 优先选择支持的up主Id集合 |
| 值域   | up主ID，多个用英文都好分隔 |
| 默认值   | 空 |
| 命令行示范   | `-supportUpIds=17819768,43619319,14583962,44473221,123938419,34858100` |

#### 2.4.3.DayOfAutoCharge（每月几号自动充电）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 每月几号自动充电 |
| 值域   | [-1,31]，-1表示不指定，默认月底最后一天；0表示不充电 |
| 默认值   | -1 |
| 命令行示范   | `-dayOfAutoCharge=25` |

#### 2.4.4.DayOfReceiveVipPrivilege（每月几号自动领取会员权益）

|   TITLE   | CONTENT   |
| ---------- | -------------- |
| 意义 | 每月几号自动领取会员权益 |
| 值域   | [-1,31]，-1表示不指定，默认月底最后一天；0表示不领取 |
| 默认值   | 1 |
| 命令行示范   | `-dayOfReceiveVipPrivilege=2` |

以上所有的配置都可以使用GitHub Secrets进行配置，如1.3中所示例。
