CheatMods for Casualties: Unknown
[中文] CheatMods 是一款为《Casualties: Unknown》量身定制的全能修改器 Mod。内置强大的辅助系统与硬核的挑战系统。所有功能均通过游戏内置控制台指令无缝切换。
[English] CheatMods is a comprehensive trainer/cheat mod tailor-made for Casualties: Unknown. It features a powerful "Cyber Doctor" Assist system and a hardcore Challenge system. All features are seamlessly toggled via the in-game console commands.

前置依赖 / Requirements
BepInEx 5 (x64) - [基础 Mod 框架 / Basic Modding Framework]

CUCoreLib - [社区开源核心库，本 Mod 硬依赖于它 / Community open-source core library, hard dependency for this mod]

🛠️ 安装方法 / Installation
确保已正确安装 BepInEx 5（将 winhttp.dll 等文件解压至游戏根目录）。
Ensure BepInEx 5 is properly installed (extract winhttp.dll etc., to the game root).

将 CUCoreLib.dll 和 CheatMods.dll 放入 BepInEx/plugins 文件夹中。
Place CUCoreLib.dll and CheatMods.dll into the BepInEx/plugins folder.

启动游戏，按 ~ 键呼出控制台，输入 cheatmods help 即可查看指令。
Launch the game, press ~ to open the console, and type cheatmods help to view commands.

重要警告 / IMPORTANT WARNING
[中文]
如果你在游戏里使用了这个 Mod，并且在控制台里输入了 CheatMods 的任何一个指令，你的存档将会直接通过游戏底层被标记为作弊存档（将无法获得任何 Steam 成就）。作者对此机制及其产生的任何后果不负任何责任。

[English]
If you use this mod in the game and enter any CheatMods command in the console, your save file will be permanently flagged as a cheated save via the game's core system (disabling all Steam achievements). The author assumes no responsibility for this mechanic or any consequences that may result from it.

指令集手册 / Command List
所有指令均在游戏内控制台（按 ~ 键）输入。指令结构严格遵循树状分类。
All commands are entered in the in-game console (press ~). Structure strictly follows hierarchy.
注：开启合法的 Challenge 指令会强制关闭所有 Assist 功能；反之亦然。
Note: Enabling a valid Challenge command will force-disable all Assist functions; vice versa.

I. 全局指令 / Global Commands
cheatmods status
查看当前所有状态。 / Displays all current statuses.

cheatmods reset
一键重置所有状态（关闭辅助，挑战归位普通）。 / Resets everything (disables Assist, resets Challenge).

cheatmods instant_quake
立即触发一场大地震。 / Triggers a massive earthquake instantly.

II. 辅助模式 / Assist Mode
语法 Syntax: cheatmods assist <action> <true/false> (省略布尔值则默认为 true / defaults to true)

cheatmods assist all true
一键开关所有辅助。 / Toggles all Assist functions.

cheatmods assist god_mode true
锁血/防骨折/清空疼痛/锁定体温 37度。 / God mode: Locks health, prevents fractures, locks temp at 37°C.

cheatmods assist infinite_stamina true
无限体力与精力，附带无限段跳。 / Infinite stamina & energy, enables infinite jump.

cheatmods assist energy true
永远精神抖擞（单独锁定精力）。 / Always energetic (locks energy drain).

cheatmods assist max_happiness true
锁死心情值（永远开心）。 / Locks mood/happiness to max.

cheatmods assist no_infection true
无感染、败血症、辐射病。 / No infections, sepsis, or radiation.

cheatmods assist no_fall_damage true
无坠落伤害。 / No fall damage.

cheatmods assist infinite_ammo true
无限子弹。 / Infinite ammo.

cheatmods assist no_weapon_jam true
枪械永不卡壳。 / Weapons never jam.

cheatmods assist super_damage true
一拳超人（伤害放大1万倍）。 / Super damage / One-Punch (10,000x damage).

cheatmods assist free_crafting true
免费合成（无材料，必成功）。 / Free crafting (no materials, always succeeds).

cheatmods assist unbreakable_items true
物品永不损坏（耐久锁定）。 / Unbreakable items (locks durability).

cheatmods assist no_earthquakes true
屏蔽地震。 / Disables earthquakes.

cheatmods assist list_recipes
打印所有合成配方（限30条）。 / Prints all crafting recipes (limited to 30).

III. 挑战模式 / Challenge Mode
语法 Syntax: cheatmods challenge <type> <value>

1. 难度预设 / Difficulty Presets

cheatmods challenge difficulty relaxed (轻松 / Relaxed)

cheatmods challenge difficulty ordinary (普通 / Ordinary)

cheatmods challenge difficulty tough (困难 / Tough)

cheatmods challenge difficulty hell (地狱 / Hell)

2. 自定义倍率 / Custom Modifiers

虚弱/代谢类 (Frailty) 范围 Limit: 0.1 ~ 3.0

cheatmods challenge frailty 2.0 (全局设置 / All sub-types)

cheatmods challenge frailty <sub-type> <value>

子类型 / Sub-types:

hunger (饥饿 / Hunger)

thirst (口渴 / Thirst)

impact (撞击 / Impact)

fall (坠落 / Fall)

infection (感染 / Infection)

depression (沮丧 / Depression)

energy (精力消耗 / Energy drain)

temperature (温度影响 / Temp impact)

经验类 (Leveling) 范围 Limit: 0.2 ~ 5.0

cheatmods challenge leveling 0.5 (全局设置 / All skills)

cheatmods challenge leveling <sub-type> <value>

子类型 / Sub-types:

str (力量 / Strength)

res (抗性 / Resilience)

int (智力 / Intelligence)

其他微调 / Other Modifiers

cheatmods challenge fragility <0.1~3.0> (易坏程度 / Item fragility)

cheatmods challenge tremor <0.1~5.0> (地震频率 / Quake frequency)

cheatmods challenge failcraft <0.0~1.0> (合成失败率 / Craft fail chance)

开源协议 / License
[中文] 本项目基于 MIT 许可证开源。
[English] This project is licensed under the MIT License.

text
MIT License

Copyright (c) 2026 DLMX

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
