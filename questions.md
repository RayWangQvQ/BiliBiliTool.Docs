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
网上搜索下其实就有很多教程了，有同Git命令的，也有直接通过GitHub的网页操作的。这里我提供一个网上的直接使用GitHub的网页操作的教程，需要的朋友可以 [>>点击查看](https://www.cnblogs.com/hzhhhbb/p/11488861.html)

