using BepInEx;
using HarmonyLib;

namespace ValheimDLCBypass
{
    [BepInPlugin("jdodger415.VDLCB", "Valheim DLC Bypass", "1.0")]
    public class ValheimDLCBypass : BaseUnityPlugin
    {
        [HarmonyPostfix]
        public static void Override(ref bool __result)
        {
            __result = true;
            return;
        }

        [HarmonyPostfix]
        public static void ReplaceSteam(ref DLCMan __instance)
        {
            foreach (DLCMan.DLCInfo dlc in __instance.m_dlcs)
            {
                dlc.m_installed = true;
            }
            return;
        }

        public void Awake()
        {
            System.Type[] uint_ = { typeof(uint) };
            System.Type[] string_ = { typeof(string) };
            System.Type[] DLCInfo_ = { typeof(DLCMan.DLCInfo) };

            Harmony harmony = new Harmony("jdodger415.VDLCB");
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "IsDLCInstalled", uint_), null, new HarmonyMethod(typeof(ValheimDLCBypass), "Override"));
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "IsDLCInstalled", string_), null, new HarmonyMethod(typeof(ValheimDLCBypass), "Override"));
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "IsDLCInstalled", DLCInfo_), null, new HarmonyMethod(typeof(ValheimDLCBypass), "Override"));
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "CheckDLCsSTEAM"), null, new HarmonyMethod(typeof(ValheimDLCBypass), "ReplaceSteam"));
        }
    }
}
