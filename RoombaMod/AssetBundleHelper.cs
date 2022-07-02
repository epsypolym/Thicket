using System.IO;
using UnityEngine;

namespace Thicket {
    public static class AssetBundleHelper {
        public static AssetBundle FindOrCreate(string directory, string name) {
            foreach (var bundle in AssetBundle.GetAllLoadedAssetBundles()) {
                if (bundle.name == name) {
                    return bundle;
                }
            }
            return AssetBundle.LoadFromFile(Path.Combine(directory, name));
        }
    }
}