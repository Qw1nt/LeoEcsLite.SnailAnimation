using SnailBee.LeoEcsLite.SnailAnimation.Runtime.Core;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Extensions
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