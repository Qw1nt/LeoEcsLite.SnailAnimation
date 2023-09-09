using Leopotam.EcsLite;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Components;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core;
using UnityEngine;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Systems
{
    internal class InitEcsAnimatorSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<InitEcsAnimatorRequest> _initRequestPool;
        private EcsPool<EcsAnimatorComponent> _animatorPool;

        public void PreInit(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _filter = world.Filter<InitEcsAnimatorRequest>().End();
            _initRequestPool = world.GetPool<InitEcsAnimatorRequest>();
            _animatorPool = world.GetPool<EcsAnimatorComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var request = ref _initRequestPool.Get(entity);
                if (request.Target.Unpack(out var world, out var targetEntity) == false)
                    continue;

                ref var ecsAnimator = ref _animatorPool.Add(targetEntity);
                ecsAnimator.Source = new EcsAnimator(request.UnityAnimator, request.EcsAnimatorData);
                
                EcsAnimatorContainer.Instance.Register(targetEntity, ecsAnimator.Source);
            }
        }
    }
}