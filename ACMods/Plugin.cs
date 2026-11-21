using HarmonyLib;
using BepInEx;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using UnityEngine;
using CUCoreLib.Registries;

namespace ACMods
{
    [BepInPlugin("com.dlmx.casualties.acmods", "ACMods", "1.1.0")]
    [BepInDependency("net.cucorelib", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource ModLogger;

        // Mod自定义时间缩放变量
        public static float modTargetTimeScale = 1f;
        public static bool useModTimeScale = false;

        private void Awake()
        {
            ModLogger = Logger;
            var harmony = new Harmony("com.dlmx.casualties.acmods");

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

            // 注册字符串反射版本 timescale 补丁
            harmony.PatchAll(typeof(TimeScalePatch));

            try
            {
                var autofill = new Dictionary<int, List<string>>
                {
                    { 0, new List<string> { "assist", "challenge", "others", "status", "reset" } },
                    { 1, new List<string> {
                        "all", "god_mode", "infinite_stamina", "max_happiness", "no_infection", "no_fall_damage",
                        "infinite_ammo", "no_weapon_jam", "free_crafting", "super_damage", "no_earthquakes", "unbreakable_items",
                        "energy", "list_recipes",
                        "difficulty", "frailty", "leveling", "fragility", "tremor", "failcraft",
                        "instant_quake", "timescale", "kill_final_talker"
                    } },
                    { 2, new List<string> {
                        "true", "false",
                        "relaxed", "ordinary", "tough", "hell",
                        "hunger", "thirst", "impact", "fall", "infection", "depression", "energy", "temperature",
                        "str", "res", "int",
                        "0.25","0.5","1.0","2.0","3.0","5.0"
                    } },
                    { 3, new List<string> { "0.1", "0.5", "1.0", "2.0", "3.0", "5.0" } }
                };
                var argDesc = new (string, string)[]
                {
                    ("string category", "assist, challenge, others, status, reset"),
                    ("string action", "The cheat or challenge type"),
                    ("string sub/value", "Sub‑type (for frailty/leveling) or the actual value"),
                    ("float value", "Multiplier value")
                };
                ConsoleCommandRegistry.Register(
                    "acmods",
                    "ACMods: Assist & Challenge System",
                    (args) => OnAcModsCommand(args),
                    autofill,
                    argDesc
                );
                Logger.LogInfo("ACMods Command registered.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to register command: {ex.Message}");
            }
        }

        /// <summary>
        /// Harmony补丁：字符串反射定位PlayerCamera.LateUpdate，规避编译期找不到游戏内部类
        /// </summary>
        [HarmonyPatch]
        [HarmonyPatch]
        public static class TimeScalePatch
        {
            static MethodBase TargetMethod()
            {
                // 不用AccessTools.AssemblyByName，自己遍历程序集（兼容旧版Harmony）
                Assembly asm = null;
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (a.GetName().Name == "Assembly-CSharp")
                    {
                        asm = a;
                        break;
                    }
                }
                if (asm == null) throw new Exception("找不到 Assembly-CSharp");

                Type playerCamType = asm.GetType("PlayerCamera");
                if (playerCamType == null) throw new Exception("找不到 PlayerCamera 类");

                MethodInfo lateUpdate = playerCamType.GetMethod("LateUpdate", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                return lateUpdate;
            }

            static void Postfix(object __instance)
            {
                if (!Plugin.useModTimeScale) return;
                try
                {
                    // 静态字段 PauseHandler.paused
                    Type pauseHandlerType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == "PauseHandler");
                    if (pauseHandlerType == null)
                    {
                        foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            var t = a.GetType("PauseHandler");
                            if (t != null) { pauseHandlerType = t; break; }
                        }
                    }
                    FieldInfo f_paused = pauseHandlerType.GetField("paused", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    bool paused = (bool)f_paused.GetValue(null);
                    if (paused) return;

                    // 实例字段 didDeathScreen
                    FieldInfo f_didDeathScreen = __instance.GetType().GetField("didDeathScreen", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    bool didDeathScreen = (bool)f_didDeathScreen.GetValue(__instance);
                    if (didDeathScreen) return;

                    // 实例字段 body
                    FieldInfo f_body = __instance.GetType().GetField("body", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    object bodyObj = f_body.GetValue(__instance);
                    if (bodyObj != null)
                    {
                        FieldInfo f_conscious = bodyObj.GetType().GetField("conscious", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        bool conscious = (bool)f_conscious.GetValue(bodyObj);
                        if (!conscious) return;
                    }
                }
                catch
                {
                    return;
                }
                Time.timeScale = Plugin.modTargetTimeScale;
            }
        }

        private static void OnAcModsCommand(string[] args)
        {
            int start = (args.Length > 0 && args[0].ToLower() == "acmods") ? 1 : 0;
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

                // reset指令复原自定义时间缩放
                Plugin.useModTimeScale = false;
                Plugin.modTargetTimeScale = 1f;
                Time.timeScale = 1f;

                TriggerCheatFlag();
                Print("All cheats and challenges reset. Everything back to normal.");
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
                    if (AssistManager.IsAnyAssistEnabled())
                    {
                        AssistManager.DisableAll();
                        Print("[WARNING] Entered CHALLENGE mode. All Assist cheats disabled!");
                    }
                    TriggerCheatFlag();
                }
                return;
            }
            if (category == "others")
            {
                if (argCount < 2)
                {
                    Print("Usage: acmods others <action>");
                    return;
                }
                string action = args[start + 1].ToLower();
                if (action == "instant_quake")
                {
                    if (WorldGeneration.world != null)
                    {
                        WorldGeneration.world.earthquakeTime = 15f;
                        WorldGeneration.world.earthquakeDelay = 99999f;
                        Sound.Play("instantearthquake", Vector2.zero, true, false, null, 1f, 1f, true, true);
                        PlayerCamera.main.shaker.Shake(350f);
                        TriggerCheatFlag();
                        Print("Triggered an earthquake instantly!");
                    }
                    return;
                }
                if (action == "timescale")
                {
                    if (argCount < 3)
                    {
                        Print("Usage: acmods others timescale <speed>  (valid:0.1‑10.0 , 1.0 = normal speed)");
                        return;
                    }
                    string scaleStr = args[start + 2];
                    if (!float.TryParse(scaleStr, out float scaleVal))
                    {
                        Print($"Invalid float value: {scaleStr}");
                        return;
                    }
                    float clamped = Mathf.Clamp(scaleVal, 0.1f, 10f);
                    modTargetTimeScale = clamped;
                    if (Math.Abs(clamped - 1.0f) < 0.001f)
                    {
                        useModTimeScale = false;
                        Time.timeScale = 1f;
                        Print($"Time‑scale restored to normal 1.0");
                    }
                    else
                    {
                        useModTimeScale = true;
                        Time.timeScale = clamped;
                        Print($"Mod time‑scale active: {clamped:F2}x (will auto‑disable on pause/death/unconscious)");
                    }
                    TriggerCheatFlag();
                    return;
                }
                if (action == "kill_final_talker")
                {
                    if (WorldGeneration.world == null)
                    {
                        Print("[ERROR] No active game world.");
                        return;
                    }
                    if (EndLabScript.instance == null)
                    {
                        Print("[ERROR] Cannot find final talker. You must be inside End‑Lab final scene.");
                        return;
                    }
                    if (EndLabScript.instance.doingEscape)
                    {
                        Print("[INFO] Escape sequence is already running, nothing to do.");
                        return;
                    }
                    EndLabScript.instance.BeginEscapeSequence();
                    PlayerCamera.main.blockItemUsage = false;

                    TriggerCheatFlag();
                    Print("[OK] Triggered killing final lab talker, escape sequence started.");
                    return;
                }

                Print($"Unknown others action '{action}'.");
                return;
            }
            Print($"Unknown category '{category}'. Type 'acmods help' for menu.");
        }

        public static void TriggerCheatFlag()
        {
            if ((!WorldGeneration.world || WorldGeneration.world.biomeOverride != WorldGeneration.OverrideSceneType.Tutorial) && !SteamManager.disableAchievements)
            {
                SteamManager.disableAchievements = true;
                if (ConsoleScript.instance != null)
                {
                    ConsoleScript.instance.Alert("<color=#ff006a>WARNING: You used a cheat command. Achievements are disabled until you restart the game.</color>");
                }
            }
        }

        public static void ShowStatus()
        {
            Print("=== ACMods Menu ===");
            Print("[GLOBAL] acmods status | acmods reset");
            Print("");
            AssistManager.ShowStatus();
            Print("");
            ChallengeManager.ShowStatus();
            Print("");
            Print("[OTHERS] acmods others <action>");
            Print("  Actions: instant_quake, timescale, kill_final_talker");
        }

        public static void Print(string message)
        {
            UnityEngine.Debug.LogWarning($"[ACMods] {message}");
            ModLogger.LogWarning(message);
        }
    }
}
