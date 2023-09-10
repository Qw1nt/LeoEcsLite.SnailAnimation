using System;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Interfaces;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common
{
    public class EcsAnimator
    {
        private readonly Animator _unityAnimator;
        private readonly EcsAnimatorData _animatorData;
        private readonly EcsAnimatorSlice[] _slices;

        private AnimationHashMap _animationHashMap;
        private bool _forcePlay;
        
        public EcsAnimator(Animator unityAnimator, EcsAnimatorData animatorData)
        {
            _unityAnimator = unityAnimator;
            _animatorData = animatorData;
            _slices = new EcsAnimatorSlice[unityAnimator.layerCount];

            Init();
        }
        
        private void Init()
        {
            if (_unityAnimator is null)
                throw new NullReferenceException("Unity animator not installed");

            if (_animatorData is null)
                throw new NullReferenceException("Ecs animator data not set");
            
            for (int i = 0; i < _slices.Length; i++)
                _slices[i] = new EcsAnimatorSlice(_unityAnimator, _animatorData.AnimationsSlice[i]);
            
            _unityAnimator.runtimeAnimatorController = _animatorData.AnimatorController;
        }

        public HashedEcsAnimation GetAnimation(string animationName, int layerIndex = 0)
        {
            return _slices[layerIndex].GetAnimation(animationName);
        }
        
        public EcsAnimator SetAnimation(string animationName, int layerIndex = 0)
        {
            _slices[layerIndex].SetAnimation(animationName);
            return this;
        }

        public void SetInitial()
        {
            foreach (var slice in _slices)
                slice.SetAnimation(slice.InitialAnimationName);
        }

        public bool IsRequiresPlayback()
        {
            if (_forcePlay == true)
            {
                _forcePlay = false;
                return true;
            }

            foreach (var slice in _slices)
            {
                if (slice.IsRequiresPlayback() == true)
                    return true;
            }

            return false;
        }
        
        public void Play()
        {
            foreach (var slice in _slices)
            {
                if (slice.IsRequiresPlayback() == true)
                    slice.Play();
            }
        }

        public void ClearBuffer()
        {
            foreach (var slice in _slices)
            {
                slice.ClearBuffer();
            }
        }
    }
}
