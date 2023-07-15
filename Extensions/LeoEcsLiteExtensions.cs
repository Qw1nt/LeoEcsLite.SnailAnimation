using AnimationSystem.Systems;
using Leopotam.EcsLite;

namespace AnimationSystem.Extensions
{
    public static class LeoEcsLiteExtensions
    {
        /*public static IEcsSystems AddInitialAnimationSystem(this IEcsSystems systems)
        {
            systems.Add(new EcsAnimatorInitializeSystem());
            systems.Add(new SetInitialAnimationSystem());

            return systems;
        }*/
        
        public static IEcsSystems AddAnimationSystem(this IEcsSystems systems)
        {
            systems.Add(new EcsAnimatorInitializeSystem());
            systems.Add(new SetInitialAnimationSystem());
            systems.Add(new EcsAnimationSystem());

            return systems;
        }
    }
}