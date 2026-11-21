If you use this mod in the game and enter any acmods command in the console, your save file will be permanently flagged as a cheated save via the game's core system (disabling all Steam achievements). The author assumes no responsibility for this mechanic or any consequences that may result from it.

All commands are entered in the in-game console (press the ~ key). The command structure strictly follows a hierarchical format. Do not enter extra arguments, or the system will reject the command with an error.

============================
ACMods v1.1 Command Reference
============================
Instructions:
1. Open in-game console, copy commands to execute.
1. 
============================
# Global Base Commands
============================
acmods status
	Show mod help menu and list all available commands

acmods reset
	Disable all assists, reset challenge multipliers, restore timeScale=1.0, turn off forced timescale

============================
# assist Cheat Group
============================
acmods assist all true
	Enable all assist cheats

acmods assist all false
	Disable all assist cheats

acmods assist infinite_stamina true
	Infinite stamina

acmods assist max_happiness true
	Set happiness/mental state to maximum

acmods assist no_infection true
	Disable infections

acmods assist no_fall_damage true
	Disable fall damage

acmods assist infinite_ammo true
	Infinite ammo

acmods assist no_weapon_jam true
	Prevent weapon jams

acmods assist free_crafting true
	Craft without consuming materials

acmods assist super_damage true
	Super high attack damage

acmods assist no_earthquakes true
	Disable earthquake events

acmods assist unbreakable_items true
	Items do not lose durability

acmods assist energy true
	Max out player energy

acmods assist list_recipes
	Print recipe information

============================
# challenge Difficulty Group
============================
acmods challenge difficulty relaxed
acmods challenge difficulty ordinary
acmods challenge difficulty tough
acmods challenge difficulty hell
	Overall difficulty preset: relaxed | ordinary | tough | hell

acmods challenge frailty hunger 1.0
acmods challenge frailty thirst 1.0
acmods challenge frailty impact 1.0
acmods challenge frailty fall 1.0
acmods challenge frailty infection 1.0
acmods challenge frailty depression 1.0
acmods challenge frailty energy 1.0
acmods challenge frailty temperature 1.0
	Survival stat multipliers. 0.25 reduces difficulty, 2~5 increases survival pressure

acmods challenge leveling str 1.0
acmods challenge leveling res 1.0
acmods challenge leveling int 1.0
	Attribute gain multiplier: str = Strength, res = Resilience, int = Intelligence

acmods challenge fragility 1.0
	Item durability wear multiplier

acmods challenge tremor 1.0
	Earthquake spawn frequency multiplier

acmods challenge failcraft 1.0
	Crafting failure chance multiplier

============================
# others Utility Commands
============================
acmods others instant_quake
	Trigger an earthquake immediately

acmods others timescale 0.1
acmods others timescale 0.5
acmods others timescale 1.0
acmods others timescale 2.0
acmods others timescale 3.0
acmods others timescale 5.0
acmods others timescale 10.0
	Custom game time scale, valid range: 0.1 - 10.0
	Note: Timescale temporarily resets during pause, unconscious state or death screen. Reverts automatically after exiting those states.
	timescale 1.0 returns control to original game time

acmods others kill_final_talker
	Ending lab scene: trigger scientist death and start escape sequence
	Restriction: Must be inside the End-Lab ending scene