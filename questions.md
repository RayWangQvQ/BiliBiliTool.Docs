# 常见问题

## 1.运行出现异常怎么办？
第一步：根据异常信息，请先仔细阅读文档（特别是 [常见问题文档](https://github.com/RayWangQvQ/BiliBiliTool.Docs/blob/main/questions.md) 和 [配置说明文档](https://github.com/RayWangQvQ/BiliBiliTool.Docs/blob/main/configuration.md) ），查找相关信息

第二步：如果文档没有找到，请到 [issues](https://github.com/RayWangQvQ/BiliBiliTool/issues) 下面查找相关问题，看是否有人其他人也遇到类似问题，并确认issue下是否已经有解决方案

第三步：如果仍没有解决，请将日志输出等级配置为Debug，该等级会输出详细的日志信息，修改后请再次运行，并查看详细的日志信息。如何配置请详见 [配置说明文档](https://github.com/RayWangQvQ/BiliBiliTool.Docs/blob/main/configuration.md)

第四步：拿到详细日志后，如果自己无法根据日志信息确定问题，请将日志信息贴到讨论群里，群里会有大佬及时帮忙解答

第五步：如果根据详细日志信息可以确认是 Bug（缺陷），可以到 [issues](https://github.com/RayWangQvQ/BiliBiliTool/issues) 下新建一条 issue 。如何新建issue请见下面的常见问题中的**如何提交issue**，如果是不符合要求的issue，会被关闭，严重的会被删除。

## 2.如何提交issue（如何提交Bug或建议）
issues 被 GitHub 译为**议题**，用来为开源项目反馈 Bug、提出建议、讨论设计与需求等。

首先先提前感谢所有提交议题的朋友们，你们的反馈和建议会让开源程序优化的越来越好。

但为了使 issues 下面的议题便于维护，便于其他人搜索查找历史议题，避免淹没在一堆无用或重复的 issues 里，请大家自觉遵守下面的提交规范：

a.提交前请先确认自己的议题是否是新的议题（是否在文档中已有说明、是否已经有其他人提过类似的议题），重复议题会被标记为重复并关闭，严重的会被删除

b.issue标题请填写完整，语义需清晰，以便在不点击进入详情时，仅根据标题就可以定位到该 issue 所反应的问题

c.如果是提交 bug ，请描述清楚问题，**并贴上详细日志信息（Debug等级的日志信息）**。如果获取Debug等级的日志信息请参见常见问题中的**运行出现异常怎么办？**，如果没有日志信息，或日志信息不是Debug等级的日志信息，或在没有日志的情况下描述也不清晰，导致无法复现或无法定位问题，该 issue 会被标记为不清晰的议题，且会被忽略或关闭，严重的会被删除。

## 3.Actions定时任务没有每天自动运行
Fork的仓库，actions默认是关闭的，需要对仓库进行1次操作才会触发webhook。

可以通过在页面上点击创建wiki来触发，也可以通过任意一次提交推送代码来触发。

## 4.Actions修改定时任务的执行时间
如果需要修改每日任务执行的时间，请修改`.github/workflows/bilibili-daily-task.yml` 中的cron表达式:

```yml
  schedule:
    - cron: '10 6 * * *'
    # cron表达式，Actions时区是UTC时间，比我们东8区要早8个小时，所以如果想每天14点10分运行，则小时数要输入6（14-8=6），如上示例。
```

## 5.我 Fork 之后怎么同步原作者的更新内容？
Fork 被 GitHub 译为复刻，相当于拷贝了一份源作者的代码到自己的 Repository （仓库）里，Fork 后，源作者更新自己的代码内容（比如发新的版本），Fork 的项目并不会自动更新源作者的修改。

想要同步源作者的修改，网上搜索下其实就有很多教程了，有同Git命令的，也有直接通过GitHub的网页操作的。

这里我贴一个随手网上搜到的直接使用GitHub的网页操作的教程，需要的朋友可以 [>>点击查看](https://www.cnblogs.com/hzhhhbb/p/11488861.html)

通过上述操作，可以将作者更新的内容PR到自己的仓库里，从而实现 BiliBiliTool 的版本升级。

当然，手动删除自己之前 Fork 的项目，然后再重新 Fork 一遍也是可以的，但是会导致之前自己的私人修改内容丢失，比如自己push的代码的修改，比如添加的 Secrets 配置。

## 6.近期出现登录报412异常
目前猜测跟B站近期风控有关，群里大佬（[@JunzhouLiu](https://github.com/JunzhouLiu)）发现改UserAgent可以暂时解决问题。

于是在V1.0.10版本添加了UA的配置，详情请进入 [配置信息](https://github.com/RayWangQvQ/BiliBiliTool.Docs/blob/main/configuration.md) 查看。

如果遇到412报错，请浏览器登录B站后F12，从接口调用信息中拿到自己的User-Agent，将其作为配置添加后再次尝试运行。
