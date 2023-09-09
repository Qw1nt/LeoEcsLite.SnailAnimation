using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Interfaces
{
    internal interface IAnimatorSlice
    {
        void Play();
        
        bool IsRequiresPlayback();

        HashedEcsAnimation GetAnimation(string animationName);

        void SetAnimation(string animationName);

        void ClearBuffer();
    }
}