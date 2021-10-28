﻿using MelonLoader;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Harmony;

namespace Thicket
{
    public static class BuildInfo
    {
        public const string Name = "Thicket"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "epsypolym"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class Thicket : MelonMod
    {
        public static string modsdir;
        public static string commondir;
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

        public override void OnApplicationStart()
        {
            modsdir = Directory.GetParent(Application.dataPath).ToString() + "\\Mods";
            commondir = Directory.GetParent(Application.dataPath).ToString() + "\\ULTRAKILL_Data\\StreamingAssets";
        }


        public override void OnLevelWasLoaded(int level)
        {
            //nuke all bundles because unity (works fine though c: )
            try { AssetBundle.UnloadAllAssetBundles(false); }
            catch { }
            if (SceneManager.GetActiveScene().name == "dreamedzone")
            {
                GameObject.Find("FirstRoomLoader").AddComponent<SceneConstructor>();
                //i have to do this otherwise the engine wholeheartedly refuses to add scene constructor
                //just trust me dude this is some arcane bullshit
                //i spent 2 hours trying to find a better way to do this, from getting the first object in the scene,
                //different execution orders, fucking everything and this is the only way it decides to work
                //unity is some weird, weird shit man.
            }
        }

        public override void OnLevelWasInitialized(int level)
        {
            psxunlit = Shader.Find("psx/unlit/unlit");
            loadnewlevel = false;
            FinalerPit.userinput = false;
        }

        public static void LoadLevel(string bundlename, string levelname)
        {
            targetbundle = bundlename;
            targetlevel = levelname;
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
        }


        public override void OnUpdate()
        {

            if (UnityEngine.Input.GetKeyDown(KeyCode.End))
            {
                var joe = GameObject.Instantiate(roombaprefab);
                var player = MonoSingleton<NewMovement>.Instance.gameObject;
                joe.transform.position = player.transform.position + new Vector3(0, 0, 2);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.PageDown))
            {
                LoadLevel("roomba.unity3d", "dust2prefab.prefab");
            }
            if (loadnewlevel && UnityEngine.Input.GetKeyDown(KeyCode.Mouse0) && levelstatthing.activeSelf)
            {
                FinalerPit.userinput = true;
            }
        }
    }
}
