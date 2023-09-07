using Leopotam.EcsLite;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Components;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Systems
{
    public class EcsAnimationTimerSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<EcsAnimatorComponent> _pool;

        public void PreInit(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world
                .Filter<EcsAnimatorComponent>()
                .End();
            
            _pool = world.GetPool<EcsAnimatorComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var entity in _filter)
            {
                ref var component = ref _pool.Get(entity);
                component.EcsAnimator.ProcessTimer(deltaTime);
            }
        }
    }
}