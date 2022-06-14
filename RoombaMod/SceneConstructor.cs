using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Thicket
{
    class SceneConstructor : MonoBehaviour
    {
        public GameObject lsc;
        public AssetBundle levelbundle;
        public AssetBundle common;
        public AssetBundle ggm;
        public GameObject preload;
        public GameObject firstroom;
        public GameObject finalroom;
        public FinalerPit finalerpit;
        public ThicketSceneInfo tsi;
        public static SpawnableObject[] bestiary;

        public void FetchPrefabs(string bundlename)
        { 
            //MapLoader.Instance.EnsureCommonIsLoaded();
            /*foreach (var bundle in AssetBundle.GetAllLoadedAssetBundles()) {
                if (bundle.name == "common") {
                    bundle.Unload(false);
                    break;
                }
            }*/
            common = AssetBundle.LoadFromFile(Path.Combine(Thicket.commondir, "common"));
            Debug.Log("Null?" + (common == null));
            levelbundle = AssetBundle.LoadFromFile(Path.Combine(Thicket.modsdir, bundlename));
        }


        public void SpawnRequirements(string levelname)
        {
            GameObject go = new GameObject("bandaid");
            go.SetActive(false);
            tsi = levelbundle.LoadAsset<GameObject>(levelname).GetComponent<ThicketSceneInfo>();

            var preload = new GameObject("preload");
            var mib = preload.AddComponent<MapInfoBase>();
            var smi = preload.AddComponent<StockMapInfo>();

            mib.layerName = tsi.layername;
            mib.levelName = tsi.levelname;

            smi.layerName = tsi.layername;
            smi.levelName = tsi.levelname;

            var sman = GameObject.Instantiate(common.LoadAsset<GameObject>("StatsManager"), go.transform);
            var statman = sman.GetComponent<StatsManager>();
            statman.secretObjects = new GameObject[] { };

            statman.killRanks = tsi.killRanks;
            statman.styleRanks = tsi.styleRanks;
            statman.timeRanks = tsi.timeRanks;

            statman.enabled = true; // enable stat manager because it's off by default for some weird reason

            bestiary = Resources.FindObjectsOfTypeAll<SpawnableObjectsDatabase>().First(x => x.name == "Bestiary Database").enemies;

            firstroom = GameObject.Instantiate(common.LoadAsset<GameObject>("FirstRoom"), go.transform);
            firstroom.transform.position = tsi.firstroomtransformposition;
            firstroom.transform.rotation = Quaternion.Euler(tsi.firstroomtransformrotation);
            //AudioMixerController shit = firstroom.transform.GetChild(4).GetComponent<AudioMixerController>();
            // set value of AudioMixerController.Instance to shit TODO

            // FINAL ROOM SPAWN, POSITION AND RECONFIG
            finalroom = GameObject.Instantiate(common.LoadAsset<GameObject>("FinalRoom"), go.transform);
            finalroom.transform.position = tsi.finalroomtransformposition;
            finalroom.transform.rotation = Quaternion.Euler(tsi.finalroomtransformrotation);
            var fp = finalroom.transform.GetChild(5).GetChild(8).gameObject.GetComponent<FinalPit>();//disable level saving
            ReflectionExtensions.SetPrivate(fp, "levelNumber", 1337);
            fp = finalroom.transform.GetChild(3).GetChild(1).gameObject.GetComponent<FinalPit>();
            ReflectionExtensions.SetPrivate(fp, "levelNumber", 1337);
            finalerpit = finalroom.transform.GetChild(5).GetChild(8).gameObject.AddComponent<FinalerPit>(); // add finaler pit level transitioner
            
            go.SetActive(true);
            
            StartCoroutine(CallShit());

            lsc = GameObject.Find("Canvas/Level Stats Controller");
            var ls = lsc.transform.GetChild(0).GetComponent<LevelStats>();
            ls.SetPrivate("ready", true);

            var audioMixerController = FindObjectOfType<AudioMixerController>();
            var audioMixerControllerField = typeof(AudioMixerController).GetField("Instance", BindingFlags.NonPublic | BindingFlags.Static);
            audioMixerControllerField.SetValue(null, audioMixerController);

            Component.Destroy(GameObject.Find("Player/Main Camera/HUD Camera/HUD/FinishCanvas/Panel/Title/Text").GetComponent<LevelNameFinder>()); // destroy this so level end text is correct
            Thicket.levelstatthing = GameObject.Find("Player/Main Camera/HUD Camera/HUD/FinishCanvas/Panel/Challenge - Title"); // reference if this exists
            GameObject.Find("Player/Main Camera/HUD Camera/HUD/FinishCanvas/Panel/Title/Text").GetComponent<UnityEngine.UI.Text>().text = tsi.levelname;
            lsc.SetActive(true);
            ls.levelName.text = tsi.levelname;
        }

        IEnumerator CallShit() // fix the stupid fucking camera manager FUCK YOU!!!!!!!!!!!!!!!!!!!!!!!!!! ih ate camera manager!@!!!!!!!!!!!!!!!!!!!!!!
        {
            GameObject.Find("Player/Main Camera").GetComponent<CameraController>().enabled = true;
            GameObject.Find("Player/Main Camera").GetComponent<CameraController>().SendMessage("Awake");
            GameObject.Find("GameController").GetComponent<PostProcessV2_Handler>().SendMessage("Start");

            yield return new WaitForEndOfFrame();
        }

        public void ConstructLevel(string levelname)
        {
            //all preload operations
            MonoSingleton<MapLoader>.Instance.isCustomLoaded = true;
            SpawnRequirements(levelname);
            var scene = GameObject.Instantiate(levelbundle.LoadAsset<GameObject>(levelname));
            var pmc = GameObject.Find("Player/Main Camera");
            StartCoroutine(enablethelevelshit());

            if (tsi.skyboxtexture == null)
            {
                pmc.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            }
            else
            {
                Material skyboxmat = common.LoadAsset<Material>("assets/materials/skyboxes/daysky.mat");
                pmc.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
                var psky = pmc.AddComponent<Skybox>();
                skyboxmat.mainTexture = tsi.skyboxtexture;
                psky.material = skyboxmat;
            }



            levelbundle.Unload(false);
            common.Unload(false);
            //MapLoader.Instance.isCommonLoaded = false;

            finalerpit.targetlevel = tsi.nextlevel;
            finalerpit.targetbundle = tsi.nextbundle;

            StartCoroutine(openthestupidfuckingdoor());
        }

        public void Start()
        {
            FetchPrefabs(Thicket.targetbundle);
            ConstructLevel(Thicket.targetlevel);

            var Harmony = new Harmony("ah! gabiel");
            Harmony.PatchAll();
        }


        
        IEnumerator openthestupidfuckingdoor()
        {
            yield return new WaitForSeconds(2);
            finalroom.transform.GetChild(0).GetChild(4).gameObject.SetActive(true); // open final room doors (handle via trigger later)
        }

        IEnumerator enablethelevelshit()
        {
            //lsc.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            //lsc.SetActive(true);
        }
    }
    [HarmonyPatch(typeof(CameraController))]
    [HarmonyPatch("Awake")]
    class sexwithmen
    {
        static bool Prefix(CameraController __instance)
        {
            Debug.Log(AudioMixerController.Instance);
            return true;
        }
    }
}
