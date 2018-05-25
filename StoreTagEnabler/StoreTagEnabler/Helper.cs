using BattleTech;
using HBS.Collections;
using System;

namespace StoreTagEnabler {
    public class Helper {

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

        public static bool meetsNewReqs(StarSystem instance, TagSet reqTags, TagSet exTags, TagSet curTags) {
            try {
                Logger.LogLine("Tags question?");
                if (!curTags.ContainsAny(exTags, true)) {
                    Logger.LogLine("start aditional Tags");
                    foreach (string item in reqTags) {
                        if (!curTags.Contains(item)) {
                            Logger.LogLine("Reg not in starsystem");
                            if (item.StartsWith("time")) {
                                Logger.LogLine("Starts with time");
                                string[] times = item.Split('_');
                                Logger.LogLine("time: " + times[1] + "days");
                                if (!(instance.Sim.DaysPassed >= int.Parse(times[1]))) {
                                    Logger.LogLine("Time not high enough");
                                    return false;
                                }
                            }
                            else if (item.StartsWith("rep")) {
                                Logger.LogLine("Starts with rep");
                                string[] reps = item.Split('_');
                                Logger.LogLine("faction: " + reps[1]);
                                Logger.LogLine("rep needed: " + reps[2]);
                                int test = instance.Sim.GetRawReputation(Helper.getfaction(reps[1]));
                                Logger.LogLine("RAW gotten");
                                if (!(test >= int.Parse(reps[2]))) {
                                    Logger.LogLine("Rep not high enough");
                                    return false;
                                }
                            }
                            else {
                                return false;
                            }
                        }
                    }
                    Logger.LogLine("Return true");
                    return true;
                }
                return false;
            }
            catch (Exception e) {
                Logger.LogError(e);
                return false;
            }
        }
    }
}

