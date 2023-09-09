using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Interfaces;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common
{
    internal class EcsAnimatorSlice : IAnimatorSlice
    {
        private readonly AnimationHashMap _animationHashMap;
        private readonly Animator _unityAnimator;
        
        internal EcsAnimatorSlice(Animator animator, EcsAnimatorData animatorData)
        {
            _unityAnimator = animator;
            _animationHashMap = new AnimationHashMap(animatorData.Animations);
        }
        
        private HashedEcsAnimation PlayableAnimation { get; set; }

        private EcsAnimationBuffer EcsAnimationBuffer { get; } = new();

        private IEcsAnimationBuffer AnimationBuffer => EcsAnimationBuffer;

        public void Play()
        {
            var animation = EcsAnimationBuffer.PlayableAnimation;

            if (PlayableAnimation == animation)
                _unityAnimator.Play(animation.Hash, -1, 0f);
            else
                _unityAnimator.CrossFade(animation.Hash, animation.TransitionDuration);

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