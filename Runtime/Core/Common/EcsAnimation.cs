using System;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common
{
    [Serializable]
    public class EcsAnimation
    {
        [SerializeField, Range(0f, 1f)] private float _layerWeight;

        [Space] [SerializeField] private string _name;
        [SerializeField] private int _priority;

        [Space] [SerializeField] private float _transitionDuration;
        [SerializeField] private AnimationClip _animationClip;

        public EcsAnimation()
        {
        }

        public EcsAnimation(string name, int priority, float transitionDuration, AnimationClip clip, float layerWeight)
        {
            _name = name;
            _priority = priority;
            _transitionDuration = transitionDuration;
            _animationClip = clip;
            _layerWeight = layerWeight;
        }

        public string Name => _name;

        public int Priority => _priority;

        public float TransitionDuration => _transitionDuration;

        public AnimationClip AnimationClip => _animationClip;

        public float LayerWeight => _layerWeight;
    }
}