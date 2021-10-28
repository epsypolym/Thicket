using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Thicket
{
    [DefaultExecutionOrder(-1)]
    class Orphanise : MonoBehaviour
    {
        public void Start()
        {
            gameObject.transform.SetParent(null);
        }
    }
}
