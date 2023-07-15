using System;
using AnimationSystem.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimationSystem.Components
{
    [Serializable]
    public struct EcsAnimatorComponent
    {
        public EcsAnimator EcsAnimator;
    }
}