using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Thicket
{
    
    class FinalerPit : MonoBehaviour
    {
        public string targetbundle;
        public string targetlevel;
        public static bool userinput = false;
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Player")
            {
                Thicket.loadnewlevel = true;
            }
        }
        void Update()
        {
            if (userinput == true)
            {
                Thicket.LoadLevel(targetbundle, targetlevel);
            }
        }
    }
}
