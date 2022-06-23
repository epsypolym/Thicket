using System;

namespace Thicket {
    [Serializable]
    public struct ModDependency {
        public string guid;
        public string downloadLink;
        public string minimumVersion;
    }
}