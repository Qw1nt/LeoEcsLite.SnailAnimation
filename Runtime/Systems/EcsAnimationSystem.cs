using Leopotam.EcsLite;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Components;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Systems
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
                var ecsAnimator = component.Source;

                if (ecsAnimator.IsRequiresPlayback() == true)
                    ecsAnimator.Play();

                ecsAnimator.ClearBuffer();
            }
        }
    }
}