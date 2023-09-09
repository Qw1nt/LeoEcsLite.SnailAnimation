using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Interfaces
{
    public interface IEcsAnimationBuffer
    {
        void SetInitial(HashedEcsAnimation ecsAnimation);

        void Clear();
    }
}