using Harmony;
using System.Reflection;

namespace StoreTagEnabler
{
    public class StoreTagEnabler
    {
        internal static string ModDirectory;
        public static void Init(string directory, string settingsJSON) {
            var harmony = HarmonyInstance.Create("de.morphyum.StoreTagEnabler");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            ModDirectory = directory;
        }
    }
}
