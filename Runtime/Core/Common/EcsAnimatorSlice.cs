using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Interfaces;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common
{
    internal class EcsAnimatorSlice : IAnimatorSlice
    {
        private readonly AnimationHashMap _animationHashMap;
        private readonly Animator _unityAnimator;
        private readonly AnimatorSliceData _data;
        
        internal EcsAnimatorSlice(Animator animator, AnimatorSliceData data)
        {
            _unityAnimator = animator;
            _data = data;
            _animationHashMap = new AnimationHashMap(_data.Animations);
        }
        
        private HashedEcsAnimation PlayableAnimation { get; set; }

        private EcsAnimationBuffer EcsAnimationBuffer { get; } = new();

        private IEcsAnimationBuffer AnimationBuffer => EcsAnimationBuffer;

        public string InitialAnimationName => _data.InitialAnimationName;

        public void Play()
        {
            var animation = EcsAnimationBuffer.PlayableAnimation;
            _unityAnimator.SetLayerWeight(_data.LayerIndex, animation.LayerWeight);
            
            if (PlayableAnimation == animation)
                _unityAnimator.Play(animation.Hash, -1, 0f);
            else
                _unityAnimator.CrossFade(animation.Hash, animation.TransitionDuration, _data.LayerIndex);

            PlayableAnimation = animation;
        }
        
        public HashedEcsAnimation GetAnimation(string animationName)
        {
            return _animationHashMap[animationName];
        }

        public void SetAnimation(string animationName)
        {
            var filledAnimation = GetAnimation(animationName);

            if (filledAnimation?.Priority > EcsAnimationBuffer.PlayableAnimation?.Priority)
                EcsAnimationBuffer.Fill(filledAnimation);

            if (filledAnimation > EcsAnimationBuffer.PlayableAnimation)
                EcsAnimationBuffer?.Fill(filledAnimation);
        }

        public bool IsRequiresPlayback()
        {
            if (PlayableAnimation != EcsAnimationBuffer.PlayableAnimation)
                return true;
            
            return EcsAnimationBuffer.PlayableAnimation?.Priority > PlayableAnimation?.Priority;
        }

        public void ClearBuffer()
        {
            AnimationBuffer.Clear();
        }
    }
}