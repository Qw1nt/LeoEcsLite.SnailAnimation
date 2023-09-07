using System;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core
{
    public class EcsAnimationBuffer : IEcsAnimationBuffer
    {
        private HashedEcsAnimation InitialAnimation { get; set; } = HashedEcsAnimation.Null;
        
        private HashedEcsAnimation LoopAnimation { get; set; } = HashedEcsAnimation.Null;

        public HashedEcsAnimation PlayableAnimation => LoopAnimation == HashedEcsAnimation.Null ? InitialAnimation : LoopAnimation;
        
        public void SetInitial(HashedEcsAnimation animation)
        {
            if (InitialAnimation != HashedEcsAnimation.Null)
                throw new InvalidOperationException();
            
            InitialAnimation = animation;
        }
        
        internal void Fill(HashedEcsAnimation animation)
        {
            LoopAnimation = animation;
        }

        public void Clear()
        {
            InitialAnimation = HashedEcsAnimation.Null;
            LoopAnimation = HashedEcsAnimation.Null;
        }
    }
}