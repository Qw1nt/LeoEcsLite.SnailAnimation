using System;
using System.Collections.Generic;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common
{
    [Serializable]
    public class AnimatorSliceData
    {
        [SerializeField] private int _layerIndex;
        [Space] [SerializeField] private string _initialAnimationName;
        [SerializeField] private List<EcsAnimation> _animations;

        public int LayerIndex => _layerIndex;

        public string InitialAnimationName => _initialAnimationName;

        public IReadOnlyList<EcsAnimation> Animations => _animations;
    }
}