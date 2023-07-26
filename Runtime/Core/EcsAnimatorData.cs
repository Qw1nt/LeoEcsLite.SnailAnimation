using System.Collections.Generic;
using UnityEngine;

namespace SnailBee.LeoEcsLite.SnailAnimation.Runtime.Core
{
    public class EcsAnimatorData : ScriptableObject
    {
        [SerializeField] private RuntimeAnimatorController animatorController;

        [Space]
        [SerializeField] private string initialAnimationName; 
        [SerializeField] private List<EcsAnimation> animations;

        public RuntimeAnimatorController AnimatorController => animatorController;

        public string InitialAnimationName => initialAnimationName;
        
        public IReadOnlyList<EcsAnimation> Animations => animations;
    }
}