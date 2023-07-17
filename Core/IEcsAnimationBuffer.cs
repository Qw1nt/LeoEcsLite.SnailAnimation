namespace SnailBee.LeoEcsLite.SnailAnimation.Core
{
    public interface IEcsAnimationBuffer
    {
        void SetInitial(HashedEcsAnimation ecsAnimation);
        
        void Clear();
    }
}