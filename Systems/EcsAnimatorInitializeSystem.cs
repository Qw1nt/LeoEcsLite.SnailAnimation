using AnimationSystem.Components;
using Leopotam.EcsLite;

namespace AnimationSystem.Systems
{
    internal class EcsAnimatorInitializeSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<InitEcsAnimatorComponent>().Inc<EcsAnimatorComponent>().End();
            
            var initEventsPool = world.GetPool<InitEcsAnimatorComponent>();
            var animatorsPool = world.GetPool<EcsAnimatorComponent>();
            
            foreach (var entity in filter)
            {
                initEventsPool.Del(entity);
                ref var ecsAnimator = ref animatorsPool.Get(entity);
                ecsAnimator.EcsAnimator.Init();
                EcsAnimatorContainer.Register(entity, ecsAnimator.EcsAnimator);
            }
        }
    }
}