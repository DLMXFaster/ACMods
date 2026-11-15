using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using CUCoreLib.Registries;

namespace CheatMods
{
    [BepInPlugin("com.dlmx.casualties.cheatmods", "CheatMods", "1.0.0")]
    [BepInDependency("net.cucorelib", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource ModLogger;

        private void Awake()
        {
            ModLogger = Logger;

            var harmony = new Harmony("com.dlmx.casualties.cheatmods");

            // Assist & Challenge 补丁
            harmony.PatchAll(typeof(AssistManager.AssistPatches.DoctorPatch));
            harmony.PatchAll(typeof(AssistManager.AssistPatches.InfiniteJumpPatch));
            harmony.PatchAll(typeof(AssistManager.AssistPatches.NoFallDamagePatch));
            harmony.PatchAll(typeof(AssistManager.AssistPatches.NoJamPatch));
            harmony.PatchAll(typeof(AssistManager.AssistPatches.InfiniteAmmoPatch));
            harmony.PatchAll(typeof(AssistManager.AssistPatches.FreeCraftPatch));
            harmony.PatchAll(typeof(AssistManager.AssistPatches.OnePunchPatch));
            harmony.PatchAll(typeof(AssistManager.AssistPatches.InfiniteItemPatch));

            harmony.PatchAll(typeof(ChallengeManager.ChallengePatches.ChallengeMetabolismPatch));
            harmony.PatchAll(typeof(ChallengeManager.ChallengePatches.ChallengeImpactPatch));
            harmony.PatchAll(typeof(ChallengeManager.ChallengePatches.ChallengeFallPatch));
            harmony.PatchAll(typeof(ChallengeManager.ChallengePatches.ChallengeInfectionPatch));
            harmony.PatchAll(typeof(ChallengeManager.ChallengePatches.ChallengeCraftFailPatch));
            harmony.PatchAll(typeof(ChallengeManager.ChallengePatches.ChallengeSkillsPatch));

            try
            {
                var autofill = new Dictionary<int, List<string>>
                {
                    { 0, new List<string> { "assist", "challenge", "instant_quake", "status", "reset" } },
                    { 1, new List<string> {
                        "all", "god_mode", "infinite_stamina", "max_happiness", "no_infection", "no_fall_damage",
                        "infinite_ammo", "no_weapon_jam", "free_crafting", "super_damage", "no_earthquakes", "unbreakable_items",
                        "energy", "list_recipes",
                        "difficulty", "frailty", "leveling", "fragility", "tremor", "failcraft"
                    } },
                    { 2, new List<string> {
                        "true", "false",
                        "relaxed", "ordinary", "tough", "hell",
                        "hunger", "thirst", "impact", "fall", "infection", "depression", "energy", "temperature",
                        "str", "res", "int"
                    } },
                    { 3, new List<string> { "0.1", "0.5", "1.0", "2.0", "3.0", "5.0" } }
                };

                var argDesc = new (string, string)[]
                {
                    ("string category", "assist, challenge, instant_quake, status, reset"),
                    ("string action", "The cheat or challenge type"),
                    ("string sub/value", "Sub-type (for frailty/leveling) or the actual value"),
                    ("float value", "Multiplier value")
                };

                ConsoleCommandRegistry.Register(
                    "cheatmods",
                    "CheatMods: Assist & Challenge System",
                    (args) => OnCheatModsCommand(args),
                    autofill,
                    argDesc
                );

                Logger.LogInfo("CheatMods Command registered.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to register command: {ex.Message}");
            }
        }

        private static void OnCheatModsCommand(string[] args)
        {
            int start = (args.Length > 0 && args[0].ToLower() == "cheatmods") ? 1 : 0;
            int argCount = args.Length - start;

            if (argCount == 0 || args[start].ToLower() == "help" || args[start] == "?")
            {
                ShowStatus();
                return;
            }

            string category = args[start].ToLower();

            if (category == "status") { ShowStatus(); return; }
            if (category == "reset")
            {
                AssistManager.DisableAll();
                ChallengeManager.ResetChallenges();
                TriggerCheatFlag(); // 触发作弊标记
                Print("All cheats and challenges reset. Everything back to normal.");
                return;
            }
            if (category == "instant_quake")
            {
                if (WorldGeneration.world != null)
                {
                    WorldGeneration.world.earthquakeTime = 15f;
                    WorldGeneration.world.earthquakeDelay = 99999f;
                    Sound.Play("instantearthquake", Vector2.zero, true, false, null, 1f, 1f, true, true);
                    PlayerCamera.main.shaker.Shake(350f);
                    TriggerCheatFlag(); // 触发作弊标记
                    Print("Triggered an earthquake instantly!");
                }
                return;
            }

            if (category == "assist")
            {
                if (AssistManager.HandleCommand(args, start))
                {
                    TriggerCheatFlag(); 
                }
                return;
            }

            if (category == "challenge")
            {
                if (ChallengeManager.HandleCommand(args, start))
                {
                    AssistManager.DisableAll();
                    Print("[WARNING] Entered CHALLENGE mode. All Assist cheats disabled!");
                    TriggerCheatFlag(); // 只有指令合法执行了，才触发作弊标记
                }
                return;
            }

            Print($"Unknown category '{category}'. Type 'cheatmods help' for menu.");
        }

        // 触发游戏原生的作弊标记
        public static void TriggerCheatFlag()
        {
            // 如果不在教程关，且作弊标记还没被触发
            if ((!WorldGeneration.world || WorldGeneration.world.biomeOverride != WorldGeneration.OverrideSceneType.Tutorial) && !SteamManager.disableAchievements)
            {
                SteamManager.disableAchievements = true;

                // 调用原版的控制台 UI 弹红字警告
                if (ConsoleScript.instance != null)
                {
                    ConsoleScript.instance.Alert("<color=#ff006a>WARNING: You used a cheat command. Achievements are disabled until you restart the game.</color>");
                }
            }
        }

        public static void ShowStatus()
        {
            Print("=== CheatMods Menu ===");
            AssistManager.ShowStatus();
            Print("");
            ChallengeManager.ShowStatus();
            Print("");
            Print("  Presets: cheatmods challenge difficulty <relaxed|ordinary|tough|hell>");
        }

        public static void Print(string message)
        {
            UnityEngine.Debug.LogWarning($"[CheatMods] {message}");
            ModLogger.LogWarning(message);
        }
    }
}