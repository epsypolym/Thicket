using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Thicket
{
    public static class BuildInfo
    {
        public const string Name = "Thicket";
        public const string Author = "epsypolym";
        public const string Company = null;
        public const string Guid = "epsypolym.ultrakill.thicket";
        public const string Version = "1.0.0";
    }

    [BepInPlugin(BuildInfo.Guid, BuildInfo.Name, BuildInfo.Version)]
    public class Thicket : BaseUnityPlugin
    {
        public static string modsdir;
        public static string commondir;
        public static string ggmdir;
        public GameObject roombaprefab;
        public GameObject preload;
        public GameObject dust2;
        public Shader psxunlit;
        public AssetBundle leveltest;
        public GameObject firstroom;
        public GameObject sman;
        public GameObject eventman;
        public GameObject checkpointcontroller;
        public static Material water3calm;
        public static GameObject bubblesparticle;
        public static GameObject splash;
        public static GameObject smallsplash;
        public GameObject finalroom;
        public static AssetBundle common;
        public static AssetBundle dreamed;
        public string[] scenePath;
        public static bool loadnewlevel;
        public static GameObject levelstatthing;
        public GameObject frl;
        public static string targetbundle;
        public static string targetlevel;
        public static Dictionary<string, Version> loadedMods;


        internal void Awake() {
            var harmony = new Harmony(BuildInfo.Guid); // rename "author" and "project"
            harmony.PatchAll();
        }

        public void Start() {
            loadedMods = new Dictionary<string, Version>();
            modsdir = Directory.GetParent(Application.dataPath).ToString() + "\\BepInEx\\plugins";
            commondir = Directory.GetParent(Application.dataPath).ToString() + "\\ULTRAKILL_Data\\StreamingAssets";

            SceneManager.sceneLoaded += OnLevelLoaded;
            SceneManager.activeSceneChanged += OnLevelLoad;
            
            // print loaded mods
            foreach (var keyValuePair in Chainloader.PluginInfos) {
                string key = keyValuePair.Key;
                PluginInfo mod = keyValuePair.Value;
                loadedMods.Add(mod.Metadata.GUID, mod.Metadata.Version);
            }
        }

        private void OnLevelLoaded(Scene level, LoadSceneMode mode) {
            Debug.Log("AAAAAAAAAAH");
            //nuke all bundles because unity (works fine though c: )
            if (SceneManager.GetActiveScene().name == "dreamedzone")
            {
                //try { AssetBundle.UnloadAllAssetBundles(false); }
                //catch { }

                GameObject.Find("FirstRoomLoader").AddComponent<SceneConstructor>();
                //i have to do this otherwise the engine wholeheartedly refuses to add scene constructor
                //just trust me dude this is some arcane bullshit
                //i spent 2 hours trying to find a better way to do this, from getting the first object in the scene,
                //different execution orders, fucking everything and this is the only way it decides to work
                //unity is some weird, weird shit man.
            }
        }


        public void OnLevelLoad(Scene a, Scene b)
        {
            Debug.Log("NOOOOOOOOOO");
            psxunlit = Shader.Find("psx/unlit/unlit");
            loadnewlevel = false;
            FinalerPit.userinput = false;
        }

        public static bool LoadLevel(string bundlename, string levelname)
        {
            //try { AssetBundle.UnloadAllAssetBundles(false); }
            //catch { }
            targetbundle = bundlename;
            targetlevel = levelname;

            Debug.Log("Initing load");
            
            List<string> errorLog = new List<string>();
            AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(modsdir, bundlename));
            Debug.Log("Bundle loaded");
            ThicketSceneInfo tsi = bundle.LoadAsset<GameObject>(levelname).GetComponent<ThicketSceneInfo>();
            Debug.Log("Loaded ThicketSceneInfo");
            if (tsi.DependencyModGuids != null) {
                foreach (ModDependency dependency in tsi.DependencyModGuids) {
                    if (!loadedMods.ContainsKey(dependency.guid)) {
                        errorLog.Add($"Missing mod {dependency.guid}! Please download: {dependency.downloadLink}");
                    } else {
                        Version currentVersion = loadedMods[dependency.guid];
                        Version minimumVersion = new Version(dependency.minimumVersion);
                        if (minimumVersion > currentVersion) {
                            errorLog.Add(
                                $"Out of date mod {dependency.guid}! Please update: {dependency.downloadLink}. Current version: {currentVersion}, Required version: {minimumVersion}");
                        }
                    }
                }

                Debug.Log("Dependency checked");
            } else {
                Debug.Log("Dependencies not found");
            }

            if (errorLog.Count > 0) {
                Debug.LogError($"Some mods are missing or are outdated for the level {bundlename}! Please resolve the issue.");
                foreach (string s in errorLog) {
                    Debug.LogError(s);
                }
                Debug.LogError("");
                Debug.LogError("BE CAUTIOUS WHEN DOWNLOADING MODS FROM THE INTERNET. ALWAYS CHECK IF IT'S OPEN SOURCE. WE DO NOT TAKE RESPONSIBILITY.");
                bundle.Unload(false);
                return false;
            }
            
            try
            { // unload empty level prefab just in case
                dreamed.Unload(true);
            } 
            catch { }

            loadnewlevel = false;
            FinalerPit.userinput = false;

            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                GameObject.Find("Canvas").transform.GetChild(31).gameObject.SetActive(true);
            }
            dreamed = AssetBundle.LoadFromFile(Path.Combine(modsdir, "dreamed.unity3d"));
            var scenePath = dreamed.GetAllScenePaths();
            SceneManager.LoadScene(scenePath[0]);
            return true;
        }


        public void Update()
        {

            if (UnityEngine.Input.GetKeyDown(KeyCode.End))
            {
                var joe = GameObject.Instantiate(roombaprefab);
                var player = MonoSingleton<NewMovement>.Instance.gameObject;
                joe.transform.position = player.transform.position + new Vector3(0, 0, 2);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.PageDown))
            {
                LoadLevel("test.unity3d", "testlevel.prefab");
            }
            if (loadnewlevel && UnityEngine.Input.GetKeyDown(KeyCode.Mouse0) && levelstatthing.activeSelf)
            {
                FinalerPit.userinput = true;
            }
        }
    }
}
