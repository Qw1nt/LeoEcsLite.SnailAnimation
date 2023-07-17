using System;
using UnityEngine;

namespace SnailBee.LeoEcsLite.SnailAnimation.Core
{
    //TODO Maybe need added animation priority
    [Serializable]
    public class EcsAnimator
    {
        [SerializeField] private Animator unityAnimator;
        [SerializeField] private EcsAnimatorData animatorData;

        private AnimationHashMap _animationHashMap;

        public EcsAnimatorData Data => animatorData;
        
        private HashedEcsAnimation PlayableAnimation { get; set; }

        private EcsAnimationBuffer EcsAnimationBuffer { get; } = new();

        public IEcsAnimationBuffer AnimationBuffer => EcsAnimationBuffer;

        internal void Init()
        {
            if (unityAnimator is null)
                throw new NullReferenceException("Unity animator not installed");
            
            if (animatorData is null)
                throw new NullReferenceException("Ecs animator data not set");

            unityAnimator.runtimeAnimatorController = animatorData.AnimatorController;
            _animationHashMap = new AnimationHashMap(animatorData.Animations);
        }

        public HashedEcsAnimation GetAnimation(string animationName) => _animationHashMap[animationName];
        
        public void SetAnimation(string animationName) => EcsAnimationBuffer.Fill(_animationHashMap[animationName]);

        public bool NeedPlayAnimation()
        {
            if (PlayableAnimation != EcsAnimationBuffer.PlayableAnimation)
                return true;

            return EcsAnimationBuffer.PlayableAnimation.Priority > PlayableAnimation.Priority;
        }

        internal void Play()
        {
            var animation = EcsAnimationBuffer.PlayableAnimation;
            unityAnimator.CrossFade(animation.Hash, animation.TransitionDuration);
            PlayableAnimation = animation;
        }
    }
}