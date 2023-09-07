using Leopotam.EcsLite;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Components;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Systems
{
    internal class EcsAnimatorInitializeSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<InitEcsAnimatorComponent> _initEventsPool;
        private EcsPool<EcsAnimatorComponent> _animatorPool;

        public void PreInit(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _filter = world.Filter<InitEcsAnimatorComponent>().Inc<EcsAnimatorComponent>().End();
            _initEventsPool = world.GetPool<InitEcsAnimatorComponent>();
            _animatorPool = world.GetPool<EcsAnimatorComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                _initEventsPool.Del(entity);
                ref var ecsAnimator = ref _animatorPool.Get(entity);
                ecsAnimator.EcsAnimator.Init();
                EcsAnimatorContainer.Instance.Register(entity, ecsAnimator.EcsAnimator);
            }
        }
    }
}