using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
    