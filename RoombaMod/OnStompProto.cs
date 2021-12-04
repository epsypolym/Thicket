using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Thicket
{
    public class OnStompProto : MonoBehaviour
    {
        public Collider coll;
        public EnemyIdentifier eid;
        public GameObject player;
        public GameObject[] enableonStomp;
        public GameObject destroyObj;
        private NewMovement nm;
        public bool stomped = false;

        void Start()
        {
            player = MonoSingleton<NewMovement>.Instance.gameObject;
            nm = MonoSingleton<NewMovement>.Instance;
        }

        void OnTriggerEnter(Collider target)
        {
            if (!stomped)
            {
                if (target.gameObject.name == "Player" && nm.slamForce > 0)
                {
                    foreach (GameObject i in enableonStomp)
                    {
                        i.SetActive(true);
                        eid.Explode();
                        MonoSingleton<StyleHUD>.Instance.AddPoints(150, "<color=cyan>WHOOMPA</color>");
                    }
                }
            }

        }
    }
}
