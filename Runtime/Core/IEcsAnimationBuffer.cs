namespace SnailBee.LeoEcsLite.SnailAnimation.Runtime.Core
{
    public interface IEcsAnimationBuffer
    {
        void SetInitial(HashedEcsAnimation ecsAnimation);

        void Clear();
    }
}