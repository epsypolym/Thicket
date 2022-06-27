using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Thicket
{
    class ThicketSceneInfo : MonoBehaviour
    {
        public string layername;
        public string levelname;
        public string nextlevel;
        public string nextbundle;
        public Vector3 firstroomtransformposition;
        public Vector3 firstroomtransformrotation;
        public Vector3 finalroomtransformposition;
        public Vector3 finalroomtransformrotation;
        public int[] killRanks;
        public int[] styleRanks;
        public int[] timeRanks;
        public GameObject[] secretObjects;
        public Texture2D skyboxtexture;

        public AudioClip calmMusic;
        public AudioClip battleMusic;
        public AudioClip bossMusic;

        [SerializeField] private string[] _dependencyMods;

        public ModDependency[] DependencyModGuids {
            get {
                if (_dependencyMods == null) return new ModDependency[] { };
                ModDependency[] modDependencies = new ModDependency[_dependencyMods.Length];
                for (int i = 0; i < _dependencyMods.Length; i++) {
                    string[] split = _dependencyMods[i].Split('|');
                    modDependencies[i] = new ModDependency() {
                        guid = split[0],
                        downloadLink = split[1],
                        minimumVersion = split[2]
                    };
                }

                return modDependencies;
            }
        }
    }
}
    