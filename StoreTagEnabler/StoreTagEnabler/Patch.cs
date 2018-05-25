using BattleTech;
using Harmony;
using HBS.Collections;
using System;
using System.Collections.Generic;

namespace StoreTagEnabler {

    [HarmonyPatch(typeof(StarSystem), "InitializeShop")]
    public static class StarSystem_InitializeShop {
        static bool Prefix(StarSystem __instance) {
            try {

                List<ShopDef> list = new List<ShopDef>();
                foreach (string id in __instance.Sim.DataManager.Shops.Keys) {
                    ShopDef shopDef = __instance.Sim.DataManager.Shops.Get(id);
                    if (SimGameState.MeetsTagRequirements(shopDef.RequirementTags, shopDef.ExclusionTags, __instance.Tags, null)) {
                        list.Add(shopDef);
                    }
                    else if (Helper.meetsNewReqs(__instance, shopDef.RequirementTags, shopDef.ExclusionTags, __instance.Tags)) {
                        list.Add(shopDef);
                    }
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "set_Shop", new object[] { new Shop(__instance, list) });

                return false;
            }
            catch (Exception e) {
                Logger.LogError(e);
                return false;
            }
        }
    }
}