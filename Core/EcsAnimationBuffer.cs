using System;

namespace SnailBee.LeoEcsLite.SnailAnimation.Core
{
    public class EcsAnimationBuffer : IEcsAnimationBuffer
    {
        private HashedEcsAnimation InitialAnimation { get; set; } = HashedEcsAnimation.Null;
        
        private HashedEcsAnimation LoopAnimation { get; set; } = HashedEcsAnimation.Null;

        public bool LoopIsEmpty => LoopAnimation == HashedEcsAnimation.Null;

        public HashedEcsAnimation PlayableAnimation => LoopIsEmpty ? InitialAnimation : LoopAnimation;

        public void SetInitial(HashedEcsAnimation animation)
        {
            if (InitialAnimation != HashedEcsAnimation.Null)
                throw new InvalidOperationException();
            
            InitialAnimation = animation;
        }
        
        public void Fill(HashedEcsAnimation animation)
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