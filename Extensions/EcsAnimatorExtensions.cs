using SnailBee.LeoEcsLite.SnailAnimation.Core;

namespace SnailBee.LeoEcsLite.SnailAnimation.Extensions
{
    public static class EcsAnimatorExtensions
    {
        public static void SetInitialAnimation(this EcsAnimator animator)
        {
            var animation = animator.GetAnimation(animator.Data.InitialAnimationName);
            animator.AnimationBuffer.SetInitial(animation);
        } 
    }
}