using System.Collections.Generic;
using Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core;
using UnityEngine.SceneManagement;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime
{
    public class EcsAnimatorContainer
    {
        private static EcsAnimatorContainer _instance;

        private EcsAnimatorContainer()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private Dictionary<int, EcsAnimator> Data { get; } = new();

        public static EcsAnimatorContainer Instance => _instance ??= new EcsAnimatorContainer();

        public void Register(int entity, EcsAnimator animator)
        {
            Data.Add(entity, animator);
        }

        public EcsAnimator SetAnimation(int entity, string animationName)
        {
            return Get(entity).SetAnimation(animationName);
        }
        
        public EcsAnimator Get(int entity)
        {
            return Data[entity];
        }

        private void OnSceneChanged(Scene scene, Scene mode)
        {
            Data.Clear();
        }
    }
}