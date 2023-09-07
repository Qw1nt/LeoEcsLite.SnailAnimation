using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core
{
    [Serializable]
    public class EcsAnimation
    {
        [SerializeField] private string _name;
        [SerializeField] private int _priority;
        [SerializeField] private float _transitionDuration;
        [SerializeField] private AnimationClip _animationClip;

        public EcsAnimation()
        {
        }

        public EcsAnimation(string name, int priority, float transitionDuration, AnimationClip animationClip)
        {
            _name = name;
            _priority = priority;
            _transitionDuration = transitionDuration;
            _animationClip = animationClip;
        }

        public string Name => _name;

        public int Priority => _priority;

        public float TransitionDuration => _transitionDuration;

        public AnimationClip AnimationClip => _animationClip;
    }
}