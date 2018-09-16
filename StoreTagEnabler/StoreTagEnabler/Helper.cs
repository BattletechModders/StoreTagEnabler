using BattleTech;
using HBS.Collections;
using System;

namespace StoreTagEnabler {
    public class Helper {

        public static Faction getfaction(string faction) {
            return (Faction) Enum.Parse(typeof(Faction), faction, true);
        }

        public static bool meetsNewReqs(StarSystem instance, TagSet reqTags, TagSet exTags, TagSet curTags) {
            try {
                if (!curTags.ContainsAny(exTags, true)) {

                    //Check exclution for time and rep
                    foreach (string item in exTags) {
                        if (item.StartsWith("time")) {
                            string[] times = item.Split('_');
                            if ((instance.Sim.DaysPassed >= int.Parse(times[1]))) {
                                return false;
                            }
                        }
                        else if (item.StartsWith("rep")) {
                            string[] reps = item.Split('_');
                            int test = instance.Sim.GetRawReputation(Helper.getfaction(reps[1]));
                            if ((test >= int.Parse(reps[2]))) {
                                return false;
                            }
                        }
                    }
                    
                    //Check requirements for time and rep
                    foreach (string item in reqTags) {
                        if (!curTags.Contains(item)) {
                            if (item.StartsWith("time")) {
                                string[] times = item.Split('_');
                                if (!(instance.Sim.DaysPassed >= int.Parse(times[1]))) {
                                    return false;
                                }
                            }
                            else if (item.StartsWith("rep")) {
                                string[] reps = item.Split('_');
                                int test = instance.Sim.GetRawReputation(Helper.getfaction(reps[1]));
                                if (!(test >= int.Parse(reps[2]))) {
                                    return false;
                                }
                            }
                            else {
                                return false;
                            }
                        }
                    }
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

