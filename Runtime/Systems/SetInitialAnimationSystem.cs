using Leopotam.EcsLite;
using SnailBee.LeoEcsLite.SnailAnimation.Runtime.Components;
using SnailBee.LeoEcsLite.SnailAnimation.Runtime.Extensions;

namespace SnailBee.LeoEcsLite.SnailAnimation.Runtime.Systems
{
    internal class SetInitialAnimationSystem : IEcsPreInitSystem, IEcsRunSystem
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
                component.EcsAnimator.SetInitialAnimation();
            }
        }
    }
}