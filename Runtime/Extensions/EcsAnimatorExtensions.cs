using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Extensions
{
    public static class EcsAnimatorExtensions
    {
        public static void SetInitialAnimation(this EcsAnimator animator, int layerIndex = 0)
        {
            animator.SetAnimation(animator.Data.InitialAnimationName, layerIndex);
        } 
    }
}