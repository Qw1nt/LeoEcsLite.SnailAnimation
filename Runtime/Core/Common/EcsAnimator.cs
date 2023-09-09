using System;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Interfaces;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common
{
    public class EcsAnimator : IAnimatorSlice
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

        public EcsAnimatorData Data => _animatorData;

        private void Init()
        {
            if (_unityAnimator is null)
                throw new NullReferenceException("Unity animator not installed");

            if (_animatorData is null)
                throw new NullReferenceException("Ecs animator data not set");
            
            for (int i = 0; i < _slices.Length; i++)
                _slices[i] = new EcsAnimatorSlice(_unityAnimator, _animatorData);
            
            _unityAnimator.runtimeAnimatorController = _animatorData.AnimatorController;
        }

        public HashedEcsAnimation GetAnimation(string animationName)
        {
            return GetAnimation(animationName, 0);
        }
        
        public HashedEcsAnimation GetAnimation(string animationName, int layerIndex)
        {
            return _slices[layerIndex].GetAnimation(animationName);
        }

        public void SetAnimation(string animationName)
        {
            SetAnimation(animationName, 0);
        }
        
        public EcsAnimator SetAnimation(string animationName, int layerIndex)
        {
            _slices[layerIndex].SetAnimation(animationName);
            return this;
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
