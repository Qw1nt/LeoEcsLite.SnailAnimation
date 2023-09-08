using Leopotam.EcsLite;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Components;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Systems
{
    internal class DelInitRequestSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<InitEcsAnimatorRequest> _requestPool;

        public void PreInit(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<InitEcsAnimatorRequest>().End();
            _requestPool = world.GetPool<InitEcsAnimatorRequest>();
        } 
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
                _requestPool.Del(entity);
        }
    }
}