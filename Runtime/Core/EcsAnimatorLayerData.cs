namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core
{
    public class EcsAnimatorLayerData
    {
        private readonly AnimationHashMap _animationHashMap;
        
        private HashedEcsAnimation PlayableAnimation { get; set; }

        private EcsAnimationBuffer EcsAnimationBuffer { get; } = new();

        public IEcsAnimationBuffer AnimationBuffer => EcsAnimationBuffer;
      
    }
}