using System;
using UnityEngine;

namespace AnimationSystem.Core
{
    [Serializable]
    public class EcsAnimation
    {
        [SerializeField] private string name;
        [SerializeField] private int priority;
        [SerializeField] private float transitionDuration;
        [SerializeField] private AnimationClip animationClip;

        public EcsAnimation()
        {
            
        }

        public EcsAnimation(string name, int priority, float transitionDuration, AnimationClip animationClip)
        {
            this.name = name;
            this.priority = priority;
            this.transitionDuration = transitionDuration;
            this.animationClip = animationClip;
        }
        
        public string Name => name;

        public int Priority => priority;
        
        public float TransitionDuration => transitionDuration;

        public AnimationClip AnimationClip => animationClip;
    }
}