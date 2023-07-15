using System.Collections.Generic;
using AnimationSystem.Core;

namespace AnimationSystem
{
    public static class EcsAnimatorContainer
    {
        private static Dictionary<int, EcsAnimator> Data { get; } = new();

        public static void Register(int entity, EcsAnimator animator) => Data.Add(entity, animator);

        public static EcsAnimator Get(int entity) => Data[entity];
        
        public static void Dispose() => Data.Clear();
    }
}