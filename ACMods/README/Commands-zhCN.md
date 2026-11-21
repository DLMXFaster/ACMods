如果你在游戏里使用了这个 Mod，并且在控制台里输入了 acmods 的任何一个指令，你的存档将会直接通过游戏底层被标记为作弊存档（将无法获得任何 Steam 成就）。作者对此机制及其产生的任何后果不负任何责任。

所有指令均在游戏内控制台（按 ~ 键）输入。指令结构严格遵循树状分类，请勿输入多余的参数，否则会触发系统报错拦截并拒绝执行。

============================
ACMods v1.1 指令参考
============================
【使用说明】
1. 游戏内打开控制台，直接复制指令执行

============================
# 全局基础命令
============================
acmods status
	显示Mod帮助菜单，查看全部可用指令

acmods reset
	重置全部作弊、重置难度、恢复timeScale=1.0，关闭timescale强制变速

============================
# assist 辅助作弊组
============================
acmods assist all true
	开启全部Assist作弊

acmods assist all false
	关闭全部Assist作弊

acmods assist infinite_stamina true
	无限体力

acmods assist max_happiness true
	设置最大精神幸福度

acmods assist no_infection true
	关闭感染

acmods assist no_fall_damage true
	关闭跌落伤害

acmods assist infinite_ammo true
	无限弹药

acmods assist no_weapon_jam true
	武器禁止卡壳

acmods assist free_crafting true
	无消耗制作

acmods assist super_damage true
	超高伤害

acmods assist no_earthquakes true
	禁用地震事件

acmods assist unbreakable_items true
	物品不会损耗损坏

acmods assist energy true
	角色精力拉满

acmods assist list_recipes
	输出配方相关信息

============================
# challenge 难度调节组
============================
acmods challenge difficulty relaxed
acmods challenge difficulty ordinary
acmods challenge difficulty tough
acmods challenge difficulty hell
	整体难度预设：relaxed简单｜ordinary普通｜tough困难｜hell地狱

acmods challenge frailty hunger 1.0
acmods challenge frailty thirst 1.0
acmods challenge frailty impact 1.0
acmods challenge frailty fall 1.0
acmods challenge frailty infection 1.0
acmods challenge frailty depression 1.0
acmods challenge frailty energy 1.0
acmods challenge frailty temperature 1.0
	生存分项难度倍率；0.25降低压力，2~5提高生存压力

acmods challenge leveling str 1.0
acmods challenge leveling res 1.0
acmods challenge leveling int 1.0
	属性获取倍率 str力量 res体质 int智力

acmods challenge fragility 1.0
	物品耐久损耗倍率

acmods challenge tremor 1.0
	地震事件发生频率倍率

acmods challenge failcraft 1.0
	制作失败概率倍率

============================
# others 工具指令组
============================
acmods others instant_quake
	立刻触发一次地震

acmods others timescale 0.1
acmods others timescale 0.5
acmods others timescale 1.0
acmods others timescale 2.0
acmods others timescale 3.0
acmods others timescale 5.0
acmods others timescale 10.0
	自定义游戏时间缩放，合法范围0.1‑10.0
	注意：暂停、昏迷、死亡黑屏会临时取消变速，不建议用，因为原版已经有这个功能，

acmods others kill_final_talker
	结局实验室：触发击杀科学家，启动逃脱结局剧情
	限制：必须处于End‑Lab结局场景