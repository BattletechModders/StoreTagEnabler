using BattleTech;
using BattleTech.Data;
using BattleTech.Framework;
using BattleTech.UI;
using Harmony;
using HBS;
using HBS.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace StoreTagEnabler {

    [HarmonyPatch(typeof(SimGameState), "MeetsTagRequirements")]
    public static class SimGameState_MeetsTagRequirements {

        static void Postfix(SimGameState __instance, ref bool __result, TagSet reqTags, TagSet exTags, TagSet curTags, SimGameReport.ReportEntry log = null) {
            try {
                if (!__result && !curTags.ContainsAny(exTags, true)) {
                    foreach(string item in reqTags) {
                        if (!curTags.Contains(item)) {
                            if (item.StartsWith("time")) {
                                string[] times = item.Split('_');
                                if(!(__instance.DaysPassed >= int.Parse(times[1]))) {
                                    return;
                                } 
                            }
                            else if (item.StartsWith("rep")) {
                                string[] reps = item.Split('_');
                                if (!(__instance.GetRawReputation(Helper.getfaction(reps[1])) >= int.Parse(reps[2]))) {
                                    return;
                                } 
                            } else {
                                return;
                            } 
                        }
                    }
                    __result = true;
                }
            }
            catch (Exception e) {
                Logger.LogError(e);

            }
        }
    }
}