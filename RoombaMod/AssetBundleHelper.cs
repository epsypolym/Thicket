using System.IO;
using UnityEngine;

namespace Thicket {
    public static class AssetBundleHelper {
        public static AssetBundle FindOrCreate(string directory, string name) {
            return FindOrCreate(directory, name, out _);
        }
        
        public static AssetBundle FindOrCreate(string directory, string name, out bool hadToLoad) {
            foreach (var bundle in AssetBundle.GetAllLoadedAssetBundles()) {
                if (bundle.name == name) {
                    hadToLoad = true;
                    return bundle;
                }
            }

            hadToLoad = false;
            return AssetBundle.LoadFromFile(Path.Combine(directory, name));
        }
    }
}