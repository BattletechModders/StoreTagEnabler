using BattleTech;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

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

    [HarmonyPatch(typeof(Shop), "GetPrice")]
    public static class Shop_GetPrice_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            // nop out two codes that make it easy to skip a logic check
            for (int i = 0; i < codes.Count(); i++)
            {
                if (codes[i].opcode == OpCodes.Ldarg_2 && codes[i + 1].opcode == OpCodes.Brfalse)
                {
                    codes[i] = new CodeInstruction(OpCodes.Nop);
                    codes[i + 1] = new CodeInstruction(OpCodes.Nop);
                }
            }

            return codes.AsEnumerable();
        }
    }
}