using Leopotam.EcsLite;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Systems;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Extensions
{
    public static class LeoEcsLiteExtensions
    {
        public static IEcsSystems AddAnimationSystem(this IEcsSystems systems)
        {
            systems.Add(new InitEcsAnimatorSystem());
            systems.Add(new DelInitRequestSystem());
            systems.Add(new SetInitialAnimationSystem());
            systems.Add(new EcsAnimationSystem());
            systems.Add(new EcsAnimationTimerSystem());

            return systems;
        }
    }
}