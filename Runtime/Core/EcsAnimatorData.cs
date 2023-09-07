using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core
{
    public class EcsAnimatorData : ScriptableObject
    {
        [SerializeField] private RuntimeAnimatorController _animatorController;

        [Space]
        [SerializeField] private string _initialAnimationName; 
        [SerializeField] private List<EcsAnimation> _animations;

        public RuntimeAnimatorController AnimatorController => _animatorController;

        public string InitialAnimationName => _initialAnimationName;
        
        public IReadOnlyList<EcsAnimation> Animations => _animations;
    }
}