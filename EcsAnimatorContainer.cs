using System.Collections.Generic;
using SnailBee.LeoEcsLite.SnailAnimation.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnailBee.LeoEcsLite.SnailAnimation
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

        public void SetAnimation(int entity, string animationName)
        {
            Get(entity).SetAnimation(animationName);
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