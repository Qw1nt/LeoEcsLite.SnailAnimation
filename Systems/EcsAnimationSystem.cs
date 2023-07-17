using Leopotam.EcsLite;
using SnailBee.LeoEcsLite.SnailAnimation.Components;
using UnityEngine;

namespace SnailBee.LeoEcsLite.SnailAnimation.Systems
{
    internal class EcsAnimationSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<EcsAnimatorComponent> _pool;

        public void PreInit(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<EcsAnimatorComponent>().End();
            _pool = world.GetPool<EcsAnimatorComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var component = ref _pool.Get(entity);
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