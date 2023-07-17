﻿using Leopotam.EcsLite;
using SnailBee.LeoEcsLite.SnailAnimation.Systems;

namespace SnailBee.LeoEcsLite.SnailAnimation.Extensions
{
    public static class LeoEcsLiteExtensions
    {
        /*public static IEcsSystems AddInitialAnimationSystem(this IEcsSystems systems)
        {
            systems.Add(new EcsAnimatorInitializeSystem());
            systems.Add(new SetInitialAnimationSystem());

            return systems;
        }*/
        
        public static IEcsSystems AddAnimationSystem(this IEcsSystems systems)
        {
            systems.Add(new EcsAnimatorInitializeSystem());
            systems.Add(new SetInitialAnimationSystem());
            systems.Add(new EcsAnimationSystem());

            return systems;
        }
    }
}