using HarmonyLib;
using UnityEngine;

namespace Thicket {
    [HarmonyPatch(typeof(AudioMixerController), "Awake")]
    public class AudioFixer {
        [HarmonyPrefix]
        public static void Prefix(AudioMixerController __instance)
        {
            Debug.Log("Don't mind me i'm just a debug text");
            MonoSingleton<AudioMixerController>.SetPrivate("instance", __instance);
        }
    }
}