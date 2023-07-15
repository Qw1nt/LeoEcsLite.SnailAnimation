using AnimationSystem.Components;
using Leopotam.EcsLite;

namespace AnimationSystem.Systems
{
    internal class EcsAnimationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<EcsAnimatorComponent>().End();
            var pool = world.GetPool<EcsAnimatorComponent>();

            foreach (var entity in filter)
            {
                ref var component = ref pool.Get(entity);
                var ecsAnimator = component.EcsAnimator;

                if (ecsAnimator.NeedPlayAnimation() == false)
                {
                    ecsAnimator.AnimationBuffer.Clear();
                    continue;
                }

                ecsAnimator.Play();
                ecsAnimator.AnimationBuffer.Clear();
            }
        }
    }
}