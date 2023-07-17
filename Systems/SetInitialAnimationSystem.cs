using Leopotam.EcsLite;
using SnailBee.LeoEcsLite.SnailAnimation.Components;
using SnailBee.LeoEcsLite.SnailAnimation.Extensions;

namespace SnailBee.LeoEcsLite.SnailAnimation.Systems
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