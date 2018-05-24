using Harmony;
using System.Reflection;

namespace StoreTagEnabler
{
    public class StoreTagEnabler
    {
        public static void Init() {
            var harmony = HarmonyInstance.Create("de.morphyum.StoreTagEnabler");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
