using BattleTech;
using Newtonsoft.Json;
using System;
using System.IO;

namespace StoreTagEnabler {
    public class Helper {

        public static Settings LoadSettings() {
            try {
                using (StreamReader r = new StreamReader("mods/InnerSphereMap/settings.json")) {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<Settings>(json);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return null;
            }
        }

        public static Faction getfaction(string faction) {
            switch (faction.ToLower()) {
                case "restoration":
                    return Faction.AuriganRestoration;
                case "outworlds":
                    return Faction.Betrayers;
                case "marian":
                    return Faction.AuriganDirectorate;
                case "illyrian":
                    return Faction.AuriganMercenaries;
                case "pirate":
                    return Faction.AuriganPirates;
                case "davion":
                    return Faction.Davion;
                case "kurita":
                    return Faction.Kurita;
                case "liao":
                    return Faction.Liao;
                case "oberon":
                    return Faction.MagistracyCentrella;
                case "magistracy":
                    return Faction.MagistracyOfCanopus;
                case "lothian":
                    return Faction.MajestyMetals;
                case "marik":
                    return Faction.Marik;
                case "circinus":
                    return Faction.Nautilus;
                case "steiner":
                    return Faction.Steiner;
                case "taurian":
                    return Faction.TaurianConcordat;
                default:
                    return Faction.NoFaction;
            }
        }
    }
}