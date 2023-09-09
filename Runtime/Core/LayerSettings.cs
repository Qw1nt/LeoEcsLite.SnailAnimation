using System;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core
{
    [Serializable]
    public struct LayerSettings
    {
        public LayerSettings(int index, float weight)
        {
            Index = index;
            Weight = weight;
        }

        public int Index { get; }

        public float Weight { get; }
    }
}