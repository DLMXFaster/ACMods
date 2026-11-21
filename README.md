# ACMods

ACMods(Assist/Challenge Mods) is a BepInEx mod for the game *CasualtiesUnknown*, providing assists, challenge difficulty modifiers and debug utility console commands.

## REQUIREMENTS
- BepInEx
- CUCoreLib (Hard Dependency)

## FEATURES
- Assist: infinite stamina, no fall damage, infinite ammo, no weapon jam, free crafting and more
- Challenge system: adjust survival difficulty multipliers, attribute gain rate, item wear, earthquake frequency and crafting failure chance
- Utility commands: custom timescale, instant earthquake, trigger ending lab escape sequence
- Console command auto-completion support

## WARNING
Using any acmods command will disable achievements. Achievements will be restored after restarting the game.

## INSTALLATION
1. Install BepInEx and CUCoreLib first.
2. Place the compiled ACMods DLL file into your game's BepInEx/plugins folder.
3. Launch *Casualties*, open the in-game console to use commands.

## CONSOLE COMMANDS

### GLOBAL COMMANDS
`acmods status`
Show mod help menu and list all available commands.

`acmods reset`
Disable all assist cheats, reset all challenge multipliers, restore timescale to 1.0 and turn off forced time scaling.

### ASSIST COMMANDS
`acmods assist all true`
Enable all assist cheats.

`acmods assist all false`
Disable all assist cheats.

`acmods assist infinite_stamina true`
Enable infinite stamina.

`acmods assist max_happiness true`
Set player happiness / mental state to maximum.

`acmods assist no_infection true`
Disable infection system.

`acmods assist no_fall_damage true`
Disable fall damage.

`acmods assist infinite_ammo true`
Enable infinite ammo.

`acmods assist no_weapon_jam true`
Prevent weapon jams.

`acmods assist free_crafting true`
Craft items without consuming materials.

`acmods assist super_damage true`
Enable super high attack damage.

`acmods assist no_earthquakes true`
Disable earthquake events.

`acmods assist unbreakable_items true`
Items do not lose durability.

`acmods assist energy true`
Max out player energy.

`acmods assist list_recipes`
Print recipe information to console.

### CHALLENGE COMMANDS
`acmods challenge difficulty relaxed`
`acmods challenge difficulty ordinary`
`acmods challenge difficulty tough`
`acmods challenge difficulty hell`
Overall difficulty preset for Casualties.

`acmods challenge frailty hunger 1.0`
`acmods challenge frailty thirst 1.0`
`acmods challenge frailty impact 1.0`
`acmods challenge frailty fall 1.0`
`acmods challenge frailty infection 1.0`
`acmods challenge frailty depression 1.0`
`acmods challenge frailty energy 1.0`
`acmods challenge frailty temperature 1.0`
Survival stat multipliers. 0.25 reduces difficulty, values from 2 to 5 increase survival pressure.

`acmods challenge leveling str 1.0`
`acmods challenge leveling res 1.0`
`acmods challenge leveling int 1.0`
Attribute gain multiplier: str = Strength, res = Resilience, int = Intelligence.

`acmods challenge fragility 1.0`
Item durability wear multiplier.

`acmods challenge tremor 1.0`
Earthquake spawn frequency multiplier.

`acmods challenge failcraft 1.0`
Crafting failure chance multiplier.

### UTILITY COMMANDS
`acmods others instant_quake`
Trigger an earthquake immediately inside the game world.

`acmods others timescale 0.1`
`acmods others timescale 0.5`
`acmods others timescale 1.0`
`acmods others timescale 2.0`
`acmods others timescale 3.0`
`acmods others timescale 5.0`
`acmods others timescale 10.0`
Custom game time scale, valid range: 0.1 - 10.0.
Timescale temporarily resets during pause, unconscious state or death screen, and automatically resumes after exiting those states.
Set timescale to 1.0 to return control to original game time.

`acmods others kill_final_talker`
Trigger scientist death and start escape sequence inside the End-Lab ending scene.
This command only works in the ending lab scene of CasualtiesUnknown.
