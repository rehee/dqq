# 摸鱼用电子斗蛐蛐

想做一个挂机的摸鱼消磨时间的小游戏. 之前写过一个版本. 使用signalr做的即时战斗. 但是发现了一些问题

- 所有战斗都在服务器不断的演算. 内存开销是真的大
- 仔细思索后, 觉得既然是摸鱼, 很大程度上就是收菜. 玩家选择装备,技能,策略. 然后挂机. 过一会收一次菜.
- 基于收菜思路. 战斗是否是即时传回也影响不大. 毕竟也没什么演出.

所以想在做一版. 电子斗蛐蛐

毕竟选择角色,装备技能. 然后就扔到地图里自动战斗到某一方全灭. 战斗的过程和收获在战斗结束后以日志的方式返回.

## 所以这不就是斗蛐蛐么

主要的玩法参考暗黑和poe. 

 - 怪物随等级指数成长. 每级怪物都比上一级的强度增加百分之5. 但是只有基础比如攻击力血量上升.
 - 怪物无基础减伤
 - 玩家角色线性增长.
 - 技能与玩家使用武器挂钩.(暗黑3,4)的计算方式.
 - 技能可以组合(poe)的方式. 但不想让技能做成升级的方式.
 - 技能消耗统一为资源 或者(冷却) 不同职业的资源获取与消耗方式不同. 还没想好

## 暂时先写这么多. 持续更新
