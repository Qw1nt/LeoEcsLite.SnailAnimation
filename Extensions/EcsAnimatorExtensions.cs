using AnimationSystem.Core;

namespace AnimationSystem.Extensions
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