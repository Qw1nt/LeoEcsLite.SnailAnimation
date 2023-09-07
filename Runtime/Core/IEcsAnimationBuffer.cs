namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core
{
    public interface IEcsAnimationBuffer
    {
        void SetInitial(HashedEcsAnimation ecsAnimation);

        void Clear();
    }
}