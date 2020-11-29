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

Ⅰ. 提交前请先确认自己的议题是否是新的议题（是否在文档中已有说明、是否已经有其他人提过类似的议题），重复议题会被标记为重复并关闭，严重的会被删除

Ⅱ. issue标题请填写完整，语义需清晰，以便在不点击进入详情时，仅根据标题就可以定位到该 issue 所反应的问题

Ⅲ. 如果是提交 bug ，请描述清楚问题，**并贴上详细日志信息（Debug等级的日志信息）**。如果获取Debug等级的日志信息请参见常见问题中的**运行出现异常怎么办？**，如果没有日志信息，或日志信息不是Debug等级的日志信息，或在没有日志的情况下描述也不清晰，导致无法复现或无法定位问题，该 issue 会被标记为不清晰的议题，且会被忽略或关闭，严重的会被删除。

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

修改后提交即可。

## 5.我 Fork 之后怎么同步原作者的更新内容？
Fork 被 GitHub 译为复刻，相当于拷贝了一份源作者的代码到自己的 Repository （仓库）里，Fork 后，源作者更新自己的代码内容（比如发新的版本），Fork 的项目并不会自动更新源作者的修改。

想要同步源作者的修改，这里提供如下三种方法。

### 5.2.方法一：使用提供的 Repo Sync 工作流脚本同步
BiliBiliTool提供了一个用于自动同步上游仓库的脚本 [repo-sync.yml](https://github.com/RayWangQvQ/BiliBiliTool/blob/main/.github/workflows/repo-sync.yml)，其内部需要一个Token参数完成授权，我们要做的共两步：获取自己的 Token 并添加到 Secrets 中，第二步运行脚本。详细步骤如下：

Ⅰ. [>>点击 Generate a token](https://github.com/settings/tokens/new?description=repo-sync&scopes=repo,workflow) 生成 `Token`，将生成的 `Token` 复制下来（只显示一次，没复制只能重新生成）。更多关于加密机密的说明可以查看 Github 官方文档：[加密机密](https://docs.github.com/cn/free-pro-team@latest/actions/reference/encrypted-secrets)。

![Generate a token 01](https://cdn.jsdelivr.net/gh/Ryanjiena/BiliBiliTool.Docs@main/imgs/generate_a_token_01.png)

![Generate a token 02](https://cdn.jsdelivr.net/gh/Ryanjiena/BiliBiliTool.Docs@main/imgs/generate_a_token_02.png)

Ⅱ. 将上一步生成的 `Token `添加到 `Github Secrets` 中。

   | GitHub Secrets | CONTENT               |
   | -------------- | --------------------- |
   | Name           | `PAT`                 |
   | Value          | 上一步生成的 `Token ` |

![New repository secret 01](https://cdn.jsdelivr.net/gh/Ryanjiena/BiliBiliTool.Docs@main/imgs/new_repository_secret_01.png)

![New repository secret 02](https://cdn.jsdelivr.net/gh/Ryanjiena/BiliBiliTool.Docs@main/imgs/new_repository_secret_02.png)

Ⅲ. 手动触发 `workflow` 工作流进行代码同步。

![Run sync workflow](https://cdn.jsdelivr.net/gh/Ryanjiena/BiliBiliTool.Docs@main/imgs/run_sync_workflows.png)
   
如果想要让自动同步的脚本定时自动运行，可以编辑 [repo-sync.yml](https://github.com/RayWangQvQ/BiliBiliTool/blob/main/.github/workflows/repo-sync.yml) 里面的 schedule 内容。定时同步默认是关闭的，需要更具自己的实际需求自己手动编辑开启。

### 5.2.方法二：手动PR同步
网上搜索下其实就有很多如何PR同步的教程了，有同Git命令的，也有直接通过GitHub的网页操作的。

这里我贴一个随手网上搜到的直接使用 GitHub 的网页操作的教程，需要的朋友可以 [>>点击查看](https://www.cnblogs.com/hzhhhbb/p/11488861.html)

通过上述操作，可以将作者更新的内容PR到自己的仓库里，从而实现 BiliBiliTool 的版本升级。

当然，手动删除自己之前 Fork 的项目，然后再重新 Fork 一遍也是可以的，但是会导致之前自己的私人修改内容丢失，比如自己push的代码的修改，比如添加的 Secrets 配置。

### 5.3.方法三：使用插件 Pull App 同步
需要安装 [![](https://prod.download/pull-18h-svg) Pull app](https://github.com/apps/pull) 插件。

安装过程中会让你选择要选择那一种方式;

`All repositories`表示同步已经 frok 的仓库以及未来 fork 的仓库；

`Only select repositories`表示仅选择要自己需要同步的仓库，其他 fork 的仓库不会被同步。

根据自己需求选择，实在不知道怎么选择，就选 `All repositories`。

点击 `install`，完成安装。

![Install Pull App](https://cdn.jsdelivr.net/gh/Ryanjiena/BiliBiliTool.Docs@main/imgs/install_pull_app.png)

Pull App 可以指定是否保留自己已经修改的内容，分为下面两种方式，如果你不知道他们的区别，就请选择方式一；如果你知道他们的区别，并且懂得如何解决 git 冲突，可根据需求自由选择任一方式：

#### Pull App 方式一： 源作者内容直接覆盖自己内容
> 该方式会将源作者的内容直接强制覆盖到自己的仓库中，也就是不会保留自己已经修改过的内容。
步骤如下：

Ⅰ. 确认已安装 [![](https://prod.download/pull-18h-svg) Pull app](https://github.com/apps/pull) 插件。

Ⅱ. 编辑 [pull.yml](https://github.com/RayWangQvQ/BiliBiliTool/blob/main/.github/pull.yml) 文件，将第 5 行内容修改为 `mergeMethod: hardreset`，然后保存提交。

（默认就是hardreset，如果未修改过，可以不用再次提交）

完成后，上游代码更新后 pull 插件会自动发起 PR 更新**覆盖**自己仓库的代码！

当然也可以立即手动触发同步：`https://pull.git.ci/process/${owner}/${repo}`

#### Pull App 方式二： 保留自己内容

> 该方式会在上游代码更新后，判断上游更新内容和自己分支代码是否存在冲突，如果有冲突则需要自己手动合并解决（也就是不会直接强制直接覆盖）。如果上游代码更新涉及 workflow 里的文件内容改动，这时也需要自己手动合并解决。

步骤如下：

Ⅰ. 确认已安装 [![](https://prod.download/pull-18h-svg) Pull app](https://github.com/apps/pull) 插件。

Ⅱ. 编辑 [pull.yml](https://github.com/RayWangQvQ/BiliBiliTool/blob/main/.github/pull.yml) 文件，将第 5 行内容修改为 `mergeMethod: merge`，然后保存提交。

完成后，上游代码更新后 pull 插件就会自动发起 PR 更新自己分支代码！只是如果存在冲突，需要自己手动去合并解决冲突。

当然也可以立即手动触发同步：`https://pull.git.ci/process/${owner}/${repo}`

## 6.近期出现登录报412异常
目前原因还未确定，暂时猜测跟 GitHub 的 IP 有关，因为大量 Fork 的项目在使用相同的 IP 在相同的时间去请求 B 站 API ，导致IP被封禁一小段时间。

如果你遇到了412错误：

第一步，请确认你的 Actions 定时任务的执行时间是否没有修改过，没改过的建议自己修改下，尽量避免和其他人重复，错开峰值（修改方式见上面的**Actions修改定时任务的执行时间**）；

第二步，请换个环境再次运行下，如果之前是 Actions 运行的，可以换到本地再运行下，如果是本地运行的，可以换个 IP 再运行一下；

第三步，如果还没有解决，可以稍等一下，过一段时间再来试一下，一般封禁时间不会太长，不用太着急
