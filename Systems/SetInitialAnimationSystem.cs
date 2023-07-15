using AnimationSystem.Components;
using AnimationSystem.Extensions;
using Leopotam.EcsLite;

namespace AnimationSystem.Systems
{
    internal class SetInitialAnimationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<EcsAnimatorComponent>().End();
            var pool = world.GetPool<EcsAnimatorComponent>();
            
            foreach (var entity in filter)
            {
                ref var component = ref pool.Get(entity);
                component.EcsAnimator.SetInitialAnimation();
            }
        }
    }
}