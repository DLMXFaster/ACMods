If you use this mod in the game and enter any CheatMods command in the console, your save file will be permanently flagged as a cheated save via the game's core system (disabling all Steam achievements). The author assumes no responsibility for this mechanic or any consequences that may result from it.

All commands are entered in the in-game console (press the ~ key). The command structure strictly follows a hierarchical format. Do not enter extra arguments, or the system will reject the command with an error.

Note: Enabling a valid Challenge command will force-disable all Assist functions; enabling a valid Assist command will reset all Challenge multipliers.

I. Global Commands
cheatmods status
Displays the current status of all Assist toggles and Challenge multipliers.

cheatmods reset
Resets everything (Disables all Assist cheats, resets Challenge difficulty to Ordinary).

cheatmods instant_quake
Triggers a massive earthquake at your current location immediately.

II. Assist Mode
Syntax: cheatmods assist <action> <true/false>
(Defaults to true if true/false is omitted)

cheatmods assist all true
Toggles all Assist functions below on or off.

cheatmods assist god_mode true
God mode. Locks health, brain health, hunger, prevents fractures, clears pain, and locks body temperature at 37°C.

cheatmods assist infinite_stamina true
Infinite stamina and energy. Includes infinite jumping.

cheatmods assist energy true
Always energetic. Locks energy drain independently.

cheatmods assist max_happiness true
Locks mood/happiness to maximum.

cheatmods assist no_infection true
No infections, sepsis, or radiation sickness.

cheatmods assist no_fall_damage true
No fall damage. You will not turn into a ragdoll.

cheatmods assist infinite_ammo true
Infinite ammo. Magazines are always full.

cheatmods assist no_weapon_jam true
Weapons never jam.

cheatmods assist super_damage true
One-Punch Man. Multiplies melee and gun damage by 10,000x.

cheatmods assist free_crafting true
Free crafting. No materials required, always succeeds.

cheatmods assist unbreakable_items true
Unbreakable items. Item durability is locked at 100%.

cheatmods assist no_earthquakes true
Disables earthquakes. The world stops shaking.

Assist Utility:

cheatmods assist list_recipes
Prints a list of all available crafting recipes in the console (Limited to 30 entries to prevent console crashes).

III. Challenge Mode
Syntax: cheatmods challenge <type> <value>

1. Difficulty Presets
cheatmods challenge difficulty relaxed
Relaxed mode. (Slower metabolism, durable items, more XP, fewer earthquakes).

cheatmods challenge difficulty ordinary
Ordinary mode. (All multipliers reset to 1.0, default game settings).

cheatmods challenge difficulty tough
Tough mode. (Faster hunger, fragile items, less XP, frequent earthquakes).

cheatmods challenge difficulty hell
Hell mode. (Extremely fast metabolism, high fracture chance, rapid item decay, frequent earthquakes, extremely slow leveling).

2. Custom Modifiers
Frailty / Metabolism (frailty)
Range limit: 0.1 ~ 3.0

Global Adjustment: cheatmods challenge frailty 2.0 (Sets all sub-types to 2x)

Individual Adjustment: cheatmods challenge frailty <sub-type> <value>

Sub-types:

hunger (Hunger rate)

thirst (Thirst rate)

impact (Physical impact / blunt damage)

fall (Fall damage)

infection (Infection speed)

depression (Happiness drain rate)

energy (Energy drain / fatigue rate)

temperature (Rate at which body temperature is affected by the environment. Higher values = faster hypothermia or heatstroke)

Example: cheatmods challenge frailty temperature 2.5

Leveling (leveling)
Range limit: 0.2 ~ 5.0 (Higher value = faster XP gain)

Global Adjustment: cheatmods challenge leveling 0.5 (Halves XP gain for all skills)

Individual Adjustment: cheatmods challenge leveling <sub-type> <value>

Sub-types:

str (Strength)

res (Resilience)

int (Intelligence)

Example: cheatmods challenge leveling int 3.0

Other Individual Modifiers

cheatmods challenge fragility <value>
Item fragility, range 0.1 ~ 3.0. Higher value = faster decay/damage.

cheatmods challenge tremor <value>
Earthquake frequency, range 0.1 ~ 5.0. Higher value = more frequent earthquakes.

cheatmods challenge failcraft <value>
Crafting failure chance, range 0.0 ~ 1.0. 0.0 = Never fails, 1.0 = Always fails.

