using HarmonyLib;
using UnityEngine;

namespace Thicket
{
    [HarmonyPatch(typeof(PostProcessV2_Handler), "SetupRTs")]
    public class PostProcessFixer
    {
        [HarmonyPrefix]
        public static void Prefix(PostProcessV2_Handler __instance)
        {
            typeof(PostProcessV2_Handler).GetField("mainCam", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(__instance, MonoSingleton<CameraController>.Instance.cam);
        }
    }
}