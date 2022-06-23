using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Thicket {
    [Serializable]
    public struct ModDependency {
        [SerializeField] public string guid;
        [SerializeField]public string downloadLink;
        [SerializeField] public string minimumVersion;
    }
}