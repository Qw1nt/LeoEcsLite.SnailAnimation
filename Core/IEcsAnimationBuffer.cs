namespace AnimationSystem.Core
{
    public interface IEcsAnimationBuffer
    {
        void SetInitial(HashedEcsAnimation ecsAnimation);
        
        void Clear();
    }
}