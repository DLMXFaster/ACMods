如果你在游戏里使用了这个 Mod，并且在控制台里输入了 CheatMods 的任何一个指令，你的存档将会直接通过游戏底层被标记为作弊存档（将无法获得任何 Steam 成就）。作者对此机制及其产生的任何后果不负任何责任。

所有指令均在游戏内控制台（按 ~ 键）输入。指令结构严格遵循树状分类，请勿输入多余的参数，否则会触发系统报错拦截并拒绝执行。

注：开启合法的 Challenge 指令会强制关闭所有 Assist 功能；开启合法的 Assist 指令会强制重置所有 Challenge 倍率。

一、 全局指令
cheatmods status
查看当前所有辅助开关状态及挑战倍率。

cheatmods reset
一键重置所有状态（关闭所有辅助，挑战难度归位普通）。

cheatmods instant_quake
立即在当前位置触发一场大地震。

二、 辅助模式 (Assist)
语法：cheatmods assist <功能名> <true/false>
(若省略 true/false，则默认为 true)

cheatmods assist all true
一键开启或关闭下方所有辅助功能。

cheatmods assist god_mode true
锁血、锁脑力、锁饥渴、防骨折、清空疼痛，并强制锁定体温恒定 37 度。

cheatmods assist infinite_stamina true
无限体力与精力，并附带无限段跳功能。

cheatmods assist energy true
永远精神抖擞（单独锁定精力不掉）。

cheatmods assist max_happiness true
锁死心情值（永远开心）。

cheatmods assist no_infection true
无感染、无败血症、无辐射病。

cheatmods assist no_fall_damage true
无坠落伤害（不会摔成布娃娃）。

cheatmods assist infinite_ammo true
无限子弹（弹匣永远满载）。

cheatmods assist no_weapon_jam true
枪械永不卡壳。

cheatmods assist super_damage true
一拳超人（近战及枪械伤害放大 10000 倍）。

cheatmods assist free_crafting true
免费合成（无需材料，必定成功）。

cheatmods assist unbreakable_items true
物品永不损坏（耐久锁定 100%）。

cheatmods assist no_earthquakes true
屏蔽地震（世界不再摇晃）。

辅助实用工具：

cheatmods assist list_recipes
在控制台打印游戏内所有可合成的配方列表（受控制台输出限制，最多显示 30 条）。

三、 挑战模式 (Challenge)
语法：cheatmods challenge <类型> <数值>

1. 难度预设
cheatmods challenge difficulty relaxed
轻松模式（代谢慢，物品难坏，经验多，地震少）。

cheatmods challenge difficulty ordinary
普通模式（所有倍率归 1.0，游戏默认）。

cheatmods challenge difficulty tough
困难模式（饿得快，物品易坏，经验少，地震多）。

cheatmods challenge difficulty hell
地狱模式（代谢极快，极度易骨折，物品腐烂极快，频繁地震，极难升级）。

2. 自定义倍率微调
虚弱/代谢类 (frailty)
范围限制：0.1 ~ 3.0

全局调整：cheatmods challenge frailty 2.0 (所有子项变为 2 倍)

单项调整：cheatmods challenge frailty <子类型> <数值>

子类型列表：

hunger (饥饿速度)

thirst (口渴速度)

impact (物理撞击/钝器伤害)

fall (坠落伤害)

infection (感染速度)

depression (沮丧/心情掉落速度)

energy (精力消耗/疲惫速度)

temperature (体温受环境温度影响的速度，数值越高越容易失温或中暑)

示例：cheatmods challenge frailty temperature 2.5

经验类 (leveling)
范围限制：0.2 ~ 5.0 (数值越高，经验获取越快)

全局调整：cheatmods challenge leveling 0.5 (所有技能经验获取减半)

单项调整：cheatmods challenge leveling <子类型> <数值>

子类型列表：

str (力量)

res (抗性)

int (智力)

示例：cheatmods challenge leveling int 3.0

其他单项微调

cheatmods challenge fragility <数值>
物品易坏程度，范围 0.1 ~ 3.0。数值越高，腐烂/损坏越快。

cheatmods challenge tremor <数值>
地震频率，范围 0.1 ~ 5.0。数值越高，地震越频繁。

cheatmods challenge failcraft <数值>
合成失败率，范围 0.0 ~ 1.0。0.0 为绝不失败，1.0 为必定失败。