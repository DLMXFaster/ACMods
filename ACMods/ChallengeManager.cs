using HarmonyLib;
using System;
using UnityEngine;

namespace ACMods
{
    public static class ChallengeManager
    {
        public static float ChallengeHunger = 1f;
        public static float ChallengeThirst = 1f;
        public static float ChallengeImpact = 1f;
        public static float ChallengeFall = 1f;
        public static float ChallengeInfection = 1f;
        public static float ChallengeDepression = 1f;
        public static float ChallengeEnergyDrain = 1f;
        public static float ChallengeTemperature = 1f;

        public static float ChallengeLeveling = 1f;
        public static float ChallengeSTR = 1f;
        public static float ChallengeRES = 1f;
        public static float ChallengeINT = 1f;
        public static float ChallengeFragility = 1f;
        public static float ChallengeTremor = 1f;
        public static float ChallengeFailCraft = 0f;
        public static string CurrentDifficulty = "ordinary";

        // 检查挑战是否被修改过
        public static bool IsChallengeModified()
        {
            return ChallengeHunger != 1f || ChallengeThirst != 1f || ChallengeImpact != 1f || ChallengeFall != 1f ||
                   ChallengeInfection != 1f || ChallengeDepression != 1f || ChallengeEnergyDrain != 1f || ChallengeTemperature != 1f ||
                   ChallengeLeveling != 1f || ChallengeSTR != 1f || ChallengeRES != 1f || ChallengeINT != 1f ||
                   ChallengeFragility != 1f || ChallengeTremor != 1f || ChallengeFailCraft != 0f ||
                   CurrentDifficulty != "ordinary";
        }

        public static bool HandleCommand(string[] args, int start)
        {
            int argCount = args.Length - start;
            if (argCount < 3) { Plugin.Print("Usage: acmods challenge <type> [sub-type] <value>"); return false; }

            string type = args[start + 1].ToLower();

            if (type == "difficulty")
            {
                if (argCount > 3) { Plugin.Print("Error: Too many arguments for 'difficulty'."); return false; }
                string diff = args[start + 2].ToLower();
                if (ApplyDifficulty(diff))
                {
                    Plugin.Print($"Difficulty preset applied: {diff.ToUpper()}");
                    return true;
                }
                return false;
            }

            if (type == "frailty" || type == "leveling")
            {
                if (argCount > 4) { Plugin.Print($"Error: Too many arguments for '{type}'."); return false; }

                if (argCount == 3)
                {
                    string valStr = args[start + 2].ToLower();
                    if (!float.TryParse(valStr, out float inputValue)) { Plugin.Print($"Invalid value '{valStr}'."); return false; }

                    if (type == "frailty")
                    {
                        float clamped = Mathf.Clamp(inputValue, 0.1f, 3f);
                        ChallengeHunger = ChallengeThirst = ChallengeImpact = ChallengeFall = ChallengeInfection = ChallengeDepression = ChallengeEnergyDrain = ChallengeTemperature = clamped;
                        Plugin.Print($"Body Frailty set to {clamped}x for ALL sub-types.");
                    }
                    else
                    {
                        float clamped = Mathf.Clamp(inputValue, 0.2f, 5f);
                        ChallengeLeveling = ChallengeSTR = ChallengeRES = ChallengeINT = clamped;
                        Plugin.Print($"Leveling set to {clamped}x for ALL skills.");
                    }
                    CurrentDifficulty = "custom";
                    return true;
                }

                string subType = args[start + 2].ToLower();
                string subValStr = args[start + 3].ToLower();
                if (!float.TryParse(subValStr, out float subInputValue)) { Plugin.Print($"Invalid value '{subValStr}'."); return false; }

                if (type == "frailty")
                {
                    float clamped = Mathf.Clamp(subInputValue, 0.1f, 3f);
                    switch (subType)
                    {
                        case "hunger": ChallengeHunger = clamped; break;
                        case "thirst": ChallengeThirst = clamped; break;
                        case "impact": ChallengeImpact = clamped; break;
                        case "fall": ChallengeFall = clamped; break;
                        case "infection": ChallengeInfection = clamped; break;
                        case "depression": ChallengeDepression = clamped; break;
                        case "energy": ChallengeEnergyDrain = clamped; break;
                        case "temperature": ChallengeTemperature = clamped; break;
                        case "all": ChallengeHunger = ChallengeThirst = ChallengeImpact = ChallengeFall = ChallengeInfection = ChallengeDepression = ChallengeEnergyDrain = ChallengeTemperature = clamped; break;
                        default: Plugin.Print($"Unknown frailty type '{subType}'."); return false;
                    }
                    Plugin.Print($"Challenge Frailty [{subType}] set to {clamped}x.");
                }
                else
                {
                    float clamped = Mathf.Clamp(subInputValue, 0.2f, 5f);
                    switch (subType)
                    {
                        case "str": ChallengeSTR = clamped; break;
                        case "res": ChallengeRES = clamped; break;
                        case "int": ChallengeINT = clamped; break;
                        case "all": ChallengeSTR = ChallengeRES = ChallengeINT = clamped; break;
                        default: Plugin.Print($"Unknown leveling type '{subType}'."); return false;
                    }
                    Plugin.Print($"Challenge Leveling [{subType}] set to {clamped}x.");
                }
                CurrentDifficulty = "custom";
                return true;
            }

            if (argCount > 3) { Plugin.Print($"Error: Too many arguments for '{type}'."); return false; }
            string valStr2 = args[start + 2].ToLower();
            if (!float.TryParse(valStr2, out float val)) { Plugin.Print($"Invalid value '{valStr2}'."); return false; }

            float cVal = 1f;
            switch (type)
            {
                case "failcraft": cVal = Mathf.Clamp(val, 0f, 1f); ChallengeFailCraft = cVal; Plugin.Print($"Craft fail chance: {cVal}"); break;
                case "fragility": cVal = Mathf.Clamp(val, 0.1f, 3f); ChallengeFragility = cVal; ModifyRunSetting("itemdecayrate", cVal); Plugin.Print($"Item decay speed: {cVal}x"); break;
                case "tremor": cVal = Mathf.Clamp(val, 0.1f, 5f); ChallengeTremor = cVal; ModifyRunSetting("timebetweenearthquakes", 1f / cVal); Plugin.Print($"Earthquake frequency: {cVal}x"); break;
                default: Plugin.Print($"Unknown challenge type '{type}'."); return false;
            }
            CurrentDifficulty = "custom";
            return true;
        }

        public static void ResetChallenges()
        {
            ChallengeHunger = ChallengeThirst = ChallengeImpact = ChallengeFall = ChallengeInfection = ChallengeDepression = ChallengeEnergyDrain = ChallengeTemperature = 1f;
            ChallengeLeveling = ChallengeSTR = ChallengeRES = ChallengeINT = 1f;
            ChallengeFragility = 1f; ChallengeTremor = 1f; ChallengeFailCraft = 0f;
            CurrentDifficulty = "ordinary";
            ModifyRunSetting("itemdecayrate", 1f);
            ModifyRunSetting("timebetweenearthquakes", 1f);
            ModifyRunSetting("xpgain", 1f);
        }

        public static bool ApplyDifficulty(string diff)
        {
            ResetChallenges();
            switch (diff)
            {
                case "relaxed":
                    ChallengeHunger = ChallengeThirst = ChallengeImpact = ChallengeFall = ChallengeInfection = ChallengeDepression = ChallengeEnergyDrain = ChallengeTemperature = 0.5f;
                    ChallengeLeveling = ChallengeSTR = ChallengeRES = ChallengeINT = 1.5f;
                    ChallengeFragility = 0.5f; ChallengeTremor = 0.5f; CurrentDifficulty = "relaxed"; break;
                case "ordinary": CurrentDifficulty = "ordinary"; break;
                case "tough":
                    ChallengeHunger = ChallengeThirst = ChallengeImpact = ChallengeFall = ChallengeInfection = ChallengeDepression = ChallengeEnergyDrain = ChallengeTemperature = 2f;
                    ChallengeLeveling = ChallengeSTR = ChallengeRES = ChallengeINT = 0.5f;
                    ChallengeFragility = 2f; ChallengeTremor = 2f; ChallengeFailCraft = 0.2f; CurrentDifficulty = "tough"; break;
                case "hell":
                    ChallengeHunger = ChallengeThirst = ChallengeImpact = ChallengeFall = ChallengeInfection = ChallengeDepression = ChallengeEnergyDrain = ChallengeTemperature = 3f;
                    ChallengeLeveling = ChallengeSTR = ChallengeRES = ChallengeINT = 0.2f;
                    ChallengeFragility = 3f; ChallengeTremor = 5f; ChallengeFailCraft = 0.5f; CurrentDifficulty = "hell"; break;
                default: Plugin.Print("Unknown difficulty."); return false;
            }
            ModifyRunSetting("itemdecayrate", ChallengeFragility);
            ModifyRunSetting("timebetweenearthquakes", 1f / ChallengeTremor);
            return true;
        }

        public static void ShowStatus()
        {
            Plugin.Print("[CHALLENGE] acmods challenge <type> [sub-type] <value>");
            Plugin.Print($"  Current Difficulty: {CurrentDifficulty.ToUpper()}");
            Plugin.Print($"  frailty  : Hunger:{ChallengeHunger} Thirst:{ChallengeThirst} Impact:{ChallengeImpact} Fall:{ChallengeFall}");
            Plugin.Print($"             Infect:{ChallengeInfection} Depress:{ChallengeDepression} Energy:{ChallengeEnergyDrain} Temp:{ChallengeTemperature}");
            Plugin.Print($"  leveling : STR:{ChallengeSTR} RES:{ChallengeRES} INT:{ChallengeINT}");
            Plugin.Print($"  failcraft: {ChallengeFailCraft} | fragility: {ChallengeFragility}x | tremor: {ChallengeTremor}x");
        }

        private static void ModifyRunSetting(string key, float value)
        {
            if (WorldGeneration.runSettings != null && WorldGeneration.runSettings.ContainsKey(key))
                WorldGeneration.runSettings[key] = value;
        }

        public static class ChallengePatches
        {
            [HarmonyPatch(typeof(Body), "HandleBody")]
            public class ChallengeMetabolismPatch
            {
                static void Postfix(Body __instance)
                {
                    if (ChallengeHunger > 1f)
                        __instance.hunger -= (ChallengeHunger - 1f) * (Time.deltaTime / 23f);

                    if (ChallengeThirst > 1f)
                        __instance.thirst -= (ChallengeThirst - 1f) * (Time.deltaTime / 17f);

                    if (ChallengeDepression > 1f)
                        __instance.happiness -= (ChallengeDepression - 1f) * Time.deltaTime * 0.05f;

                    if (ChallengeEnergyDrain > 1f)
                        __instance.energy -= (ChallengeEnergyDrain - 1f) * Time.deltaTime * 0.07f;

                    if (ChallengeTemperature > 1f && WorldGeneration.world != null && !AssistManager.IsHealthEnabled)
                    {
                        float ambient = WorldGeneration.world.ambientTemperature;
                        __instance.temperature = Mathf.MoveTowards(__instance.temperature, ambient, (ChallengeTemperature - 1f) * 0.05f * Time.deltaTime);
                    }
                }
            }

            [HarmonyPatch(typeof(Limb), "ImpactDamage")]
            public class ChallengeImpactPatch
            {
                static void Prefix(ref float force)
                {
                    if (ChallengeImpact > 1f && force <= 25f)
                        force *= ChallengeImpact;
                }
            }

            [HarmonyPatch(typeof(Body), "OnCollisionEnter2D")]
            public class ChallengeFallPatch
            {
                static void Prefix(Body __instance)
                {
                    if (ChallengeFall > 1f && !AssistManager.IsFallDamageEnabled)
                        __instance.lastTimeStepVelocity.y *= ChallengeFall;
                }
            }

            [HarmonyPatch(typeof(Limb), "GetInfectionSpeed")]
            public class ChallengeInfectionPatch
            {
                static void Postfix(ref float __result)
                {
                    if (ChallengeInfection > 1f)
                        __result *= ChallengeInfection;
                }
            }

            [HarmonyPatch(typeof(Skills), "AddExp")]
            public class ChallengeSkillsPatch
            {
                static void Prefix(int stat, ref float xp)
                {
                    float originalMult = Skills.xpGainMult;
                    if (originalMult > 0f) xp /= originalMult;

                    if (stat == 0) xp *= ChallengeSTR;
                    else if (stat == 1) xp *= ChallengeRES;
                    else if (stat == 2) xp *= ChallengeINT;
                }
            }

            [HarmonyPatch(typeof(Recipe), "TryMake")]
            public class ChallengeCraftFailPatch
            {
                static bool Prefix(Recipe __instance)
                {
                    if (ChallengeFailCraft > 0f && UnityEngine.Random.value < ChallengeFailCraft)
                    {
                        Sound.Play("fail", PlayerCamera.main.body.transform.position, false, true, null, 1f, 1f, false, false);
                        PlayerCamera.main.PlayUISound(PlayerCamera.UISoundType.Deny, 1f);
                        return false;
                    }
                    return true;
                }
            }
        }
    }
}