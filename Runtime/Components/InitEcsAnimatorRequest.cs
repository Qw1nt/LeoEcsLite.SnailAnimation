using Leopotam.EcsLite;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Components
{
    public struct InitEcsAnimatorRequest
    {
        public EcsPackedEntityWithWorld Target;
        public Animator UnityAnimator;
        public EcsAnimatorData EcsAnimatorData;
    }
}