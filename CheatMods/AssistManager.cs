using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CheatMods
{
    public static class AssistManager
    {
        public static bool IsStaminaEnabled = false;
        public static bool IsHappinessEnabled = false;
        public static bool IsHealthEnabled = false;
        public static bool IsInfectionEnabled = false;
        public static bool IsFallDamageEnabled = false;
        public static bool IsAmmoEnabled = false;
        public static bool IsJamEnabled = false;
        public static bool IsCraftEnabled = false;
        public static bool IsOnePunchEnabled = false;
        public static bool IsEarthquakeEnabled = false;
        public static bool IsItemEnabled = false;
        public static bool IsEnergyEnabled = false;

        public static bool HandleCommand(string[] args, int start)
        {
            int argCount = args.Length - start;

            if (argCount > 3)
            {
                Plugin.Print("Error: Too many arguments. Usage: cheatmods assist <action> <true/false>");
                return false;
            }
            if (argCount < 2)
            {
                Plugin.Print("Usage: cheatmods assist <action> <true/false>");
                return false;
            }

            string action = args[start + 1].ToLower();

            if (action == "list_recipes")
            {
                if (argCount > 2)
                {
                    Plugin.Print("Error: 'list_recipes' does not take any arguments.");
                    return false;
                }
                ListAllRecipes();
                return true;
            }

            if (action != "all" && action != "god_mode" && action != "infinite_stamina" && action != "max_happiness" &&
                action != "no_infection" && action != "no_fall_damage" && action != "infinite_ammo" && action != "no_weapon_jam" &&
                action != "free_crafting" && action != "super_damage" && action != "no_earthquakes" && action != "unbreakable_items" &&
                action != "energy")
            {
                Plugin.Print($"Unknown assist action '{action}'. (Did you mean to use 'challenge' instead?)");
                return false;
            }

            bool state = true;
            if (argCount == 3)
            {
                if (!bool.TryParse(args[start + 2].ToLower(), out state))
                {
                    Plugin.Print($"Invalid boolean '{args[start + 2]}'. Use 'true' or 'false'.");
                    return false;
                }
            }

            if (state)
            {
                ChallengeManager.ResetChallenges();
                Plugin.Print("[WARNING] Assist mode enabled. All Challenge multipliers have been reset to default!");
            }

            if (action == "all")
            {
                IsStaminaEnabled = IsHappinessEnabled = IsHealthEnabled = IsInfectionEnabled =
                IsFallDamageEnabled = IsAmmoEnabled = IsJamEnabled = IsCraftEnabled =
                IsOnePunchEnabled = IsEarthquakeEnabled = IsItemEnabled = IsEnergyEnabled = state;
                Plugin.Print($"All Assist Cheats: {(state ? "ON" : "OFF")}");
                return true;
            }

            switch (action)
            {
                case "god_mode": IsHealthEnabled = state; break;
                case "infinite_stamina": IsStaminaEnabled = state; break;
                case "max_happiness": IsHappinessEnabled = state; break;
                case "no_infection": IsInfectionEnabled = state; break;
                case "no_fall_damage": IsFallDamageEnabled = state; break;
                case "infinite_ammo": IsAmmoEnabled = state; break;
                case "no_weapon_jam": IsJamEnabled = state; break;
                case "free_crafting": IsCraftEnabled = state; break;
                case "super_damage": IsOnePunchEnabled = state; break;
                case "no_earthquakes": IsEarthquakeEnabled = state; break;
                case "unbreakable_items": IsItemEnabled = state; break;
                case "energy": IsEnergyEnabled = state; break;
            }
            Plugin.Print($"Assist '{action}': {(state ? "ON" : "OFF")}");

            if (action == "no_earthquakes" && !state)
            {
                if (WorldGeneration.world != null) WorldGeneration.world.allowEarthquakes = true;
            }
            return true;
        }

        private static void ListAllRecipes()
        {
            Plugin.Print("=== All Available Recipes ===");
            try
            {
                if (Recipes.recipes == null)
                {
                    Plugin.Print("Error: Recipes.recipes is null. (Are you inside a game world?)");
                    return;
                }

                Plugin.Print($"Total recipes found: {Recipes.recipes.Count}");

                int count = 0;
                foreach (var r in Recipes.recipes)
                {
                    if (r == null) continue;

                    string id = "Unknown";
                    if (r.result != null && !string.IsNullOrEmpty(r.result.id))
                    {
                        id = r.result.id;
                        if (r.result.amount > 1) id += $" (x{r.result.amount})";
                    }

                    string category = "Unknown";
                    if (r.category != null) category = r.category.ToString();

                    Plugin.Print($"- [{category}] {id}");
                    count++;

                    if (count >= 30)
                    {
                        Plugin.Print("... (Output limited to 30 recipes to prevent console crash)");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Print($"Error while listing recipes: {ex.Message}");
            }
        }

        public static void DisableAll()
        {
            IsStaminaEnabled = false; IsHappinessEnabled = false; IsHealthEnabled = false;
            IsInfectionEnabled = false; IsFallDamageEnabled = false; IsAmmoEnabled = false;
            IsJamEnabled = false; IsCraftEnabled = false; IsOnePunchEnabled = false;
            IsEarthquakeEnabled = false; IsItemEnabled = false; IsEnergyEnabled = false;
        }

        public static void ShowStatus()
        {
            Plugin.Print("[ASSIST] cheatmods assist <action> <true/false>");
            Plugin.Print("  Actions: all, god_mode, infinite_stamina, max_happiness, energy, no_infection, no_fall_damage");
            Plugin.Print("           infinite_ammo, no_weapon_jam, free_crafting, super_damage, no_earthquakes, unbreakable_items");
            Plugin.Print("  Utility: list_recipes");
        }

        public static class AssistPatches
        {
            [HarmonyPatch(typeof(Body), "HandleBody")]
            [HarmonyPriority(Priority.Last)]
            public class DoctorPatch
            {
                static void Postfix(Body __instance)
                {
                    if (IsStaminaEnabled) { __instance.stamina = 100f; __instance.energy = 100f; }
                    if (IsEnergyEnabled) { __instance.energy = 100f; }
                    if (IsHappinessEnabled) { __instance.happiness = 100f; }
                    if (IsHealthEnabled)
                    {
                        __instance.brainHealth = 100f; __instance.bloodVolume = 100f; __instance.bloodOxygen = 100f;
                        __instance.bloodPressure = 120f; __instance.bloodViscosity = 0f; __instance.hunger = 100f; __instance.thirst = 100f;
                        __instance.averagePain = 0f; __instance.internalBleeding = 0f; __instance.totalBleedSpeed = 0f;

                        //锁定体温恒定 37 度
                        __instance.temperature = 37f;

                        if (__instance.limbs != null)
                        {
                            foreach (Limb limb in __instance.limbs)
                            {
                                limb.broken = false; limb.dislocated = false; limb.muscleHealth = 100f;
                                limb.skinHealth = 100f; limb.pain = 0f; limb.bleedAmount = 0f;
                            }
                        }
                    }
                    if (IsInfectionEnabled)
                    {
                        __instance.sicknessAmount = 0f; __instance.septicShock = 0f; __instance.radiationSickness = 0f;
                        if (__instance.limbs != null) { foreach (Limb limb in __instance.limbs) { limb.infected = false; } }
                    }
                }
            }

            [HarmonyPatch(typeof(Item), "Update")]
            public class InfiniteItemPatch
            {
                static void Postfix(Item __instance)
                {
                    if (!IsItemEnabled) return;
                    __instance.condition = 1f;
                }
            }

            [HarmonyPatch(typeof(Body), "Jump")]
            public class InfiniteJumpPatch
            {
                static readonly AccessTools.FieldRef<Body, float> JumpCooldownRef = AccessTools.FieldRefAccess<Body, float>("jumpCooldown");
                static bool Prefix(Body __instance)
                {
                    if (!IsStaminaEnabled) return true;
                    __instance.rb.velocity = new Vector2(__instance.rb.velocity.x, __instance.jumpSpeed);
                    JumpCooldownRef(__instance) = 0f;
                    return false;
                }
            }

            [HarmonyPatch(typeof(Body), "OnCollisionEnter2D")]
            public class NoFallDamagePatch
            {
                static bool Prefix(Body __instance, Collision2D collision)
                {
                    if (!IsFallDamageEnabled) return true;
                    if (__instance.lastTimeStepVelocity.y < -Mathf.Max(__instance.actualJumpSpeed * 2f, __instance.jumpSpeed * 1.5f)) { return false; }
                    return true;
                }
            }

            [HarmonyPatch(typeof(GunScript), "JamChance")]
            public class NoJamPatch
            {
                static bool Prefix(ref float __result)
                {
                    if (!IsJamEnabled) return true;
                    __result = 0f;
                    return false;
                }
            }

            [HarmonyPatch(typeof(GunScript), "Update")]
            public class InfiniteAmmoPatch
            {
                static void Postfix(GunScript __instance)
                {
                    if (!IsAmmoEnabled) return;
                    if (__instance.hasMag) __instance.roundsInMag = __instance.magCapacity;
                    if (__instance.roundInChamber == GunScript.RoundInChamber.Casing || __instance.roundInChamber == GunScript.RoundInChamber.None)
                    { __instance.roundInChamber = GunScript.RoundInChamber.Round; }
                }
            }

            [HarmonyPatch(typeof(Body), "Attack")]
            public class OnePunchPatch
            {
                static void Prefix(ref AttackInfo atk)
                {
                    if (!IsOnePunchEnabled) return;
                    atk.damage *= 10000f;
                    atk.structuralDamage *= 10000f;
                }
            }

            [HarmonyPatch(typeof(Recipe), "TryMake")]
            public class FreeCraftPatch
            {
                static bool Prefix(Recipe __instance)
                {
                    if (!IsCraftEnabled) return true;
                    __instance.result.SpawnResult(__instance.INT);
                    Sound.Play("combine", PlayerCamera.main.body.transform.position, false, true, null, 1f, 1f, false, false);
                    PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Click, 1f);
                    return false;
                }
            }
        }
    }
}