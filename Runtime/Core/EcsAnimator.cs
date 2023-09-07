﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core
{
    [Serializable]
    public class EcsAnimator
    {
        [SerializeField] private Animator _unityAnimator;
        [SerializeField] private EcsAnimatorData _animatorData;

        private AnimationHashMap _animationHashMap;

        public EcsAnimatorData Data => _animatorData;

        private HashedEcsAnimation PlayableAnimation { get; set; }

        private EcsAnimationBuffer EcsAnimationBuffer { get; } = new();

        public IEcsAnimationBuffer AnimationBuffer => EcsAnimationBuffer;

        private float _animationTimerDuration;
        private bool _forcePlay;

        internal void Init()
        {
            if (_unityAnimator is null)
                throw new NullReferenceException("Unity animator not installed");

            if (_animatorData is null)
                throw new NullReferenceException("Ecs animator data not set");

            _unityAnimator.runtimeAnimatorController = _animatorData.AnimatorController;
            _animationHashMap = new AnimationHashMap(_animatorData.Animations);
        }

        public HashedEcsAnimation GetAnimation(string animationName)
        {
            return _animationHashMap[animationName];
        }

        public EcsAnimator SetAnimation(string animationName)
        {
            var filledAnimation = _animationHashMap[animationName];

            if (filledAnimation.Priority > EcsAnimationBuffer.PlayableAnimation.Priority)
                EcsAnimationBuffer.Fill(filledAnimation);

            if (_animationTimerDuration <= 0f && filledAnimation > EcsAnimationBuffer.PlayableAnimation)
                EcsAnimationBuffer.Fill(filledAnimation);

            return this;
        }

        public void SetTimer(float duration)
        {
            _animationTimerDuration = duration;
            _forcePlay = true;
        }

        internal void ProcessTimer(float deltaTime)
        {
            _animationTimerDuration -= deltaTime;

            if (_animationTimerDuration <= 0f)
                _animationTimerDuration = 0f;
        }

        internal bool NeedPlayAnimation()
        {
            if (_forcePlay == true)
            {
                _forcePlay = false;
                return true;
            }

            if (_animationTimerDuration > 0f)
                return false;

            if (PlayableAnimation != EcsAnimationBuffer.PlayableAnimation)
                return true;

            return EcsAnimationBuffer.PlayableAnimation.Priority > PlayableAnimation.Priority;
        }

        internal void Play()
        {
            var animation = EcsAnimationBuffer.PlayableAnimation;

            if (PlayableAnimation == animation)
                _unityAnimator.Play(animation.Hash, -1, 0f);
            else
                _unityAnimator.CrossFade(animation.Hash, animation.TransitionDuration);

            PlayableAnimation = animation;
        }
    }
}