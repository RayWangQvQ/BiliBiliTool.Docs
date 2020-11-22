# 常见问题

## 1.Actions定时任务没有每天自动运行
Fork的仓库，actions默认是关闭的，需要对仓库进行1次操作才会触发webhook。
可以通过在页面上点击创建wiki来触发，也可以通过任意一次提交推送代码来触发。

## 2.Actions修改定时任务的执行时间
如果需要修改每日任务执行的时间，请修改`.github/workflows/bilibili-daily-task.yml` 中的cron表达式:

```yml
  schedule:
    - cron: '10 6 * * *'
    # cron表达式，Actions时区是UTC时间，比我们东8区要早8个小时，所以如果想每天14点10分运行，则小时数要输入6（14-8=6），如上示例。
```

## 3.我 Fork 之后怎么同步原作者的更新内容？
网上搜索下其实就有很多教程了，有同Git命令的，也有直接通过GitHub的网页操作的。这里我贴一个随手网上搜到的直接使用GitHub的网页操作的教程，需要的朋友可以 [>>点击查看](https://www.cnblogs.com/hzhhhbb/p/11488861.html)

## 4.近期出现登录报412异常
目前猜测跟B站近期风控有关，群里大佬（[@JunzhouLiu](https://github.com/JunzhouLiu)）发现改UserAgent可以暂时解决问题。
于是在V1.0.10版本添加了UA的配置，详情请进入 [配置信息](https://github.com/RayWangQvQ/BiliBiliTool.Docs/blob/main/configuration.md) 查看。
如果遇到412报错，请浏览器登录B站后F12，从接口调用信息中拿到自己的User-Agent，将其作为配置添加后再次尝试运行。
