using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Qw1nt.LeoEcsLite.EaseAnimation.Runtime.Core.Common
{
    public class EcsAnimatorData : ScriptableObject
    {
        [SerializeField] private RuntimeAnimatorController _animatorController;

        [Space] [SerializeField] private string _initialAnimationName;
        [SerializeField] private List<AnimatorSliceData> _layerSlices;

        /*private void OnValidate()
        {
            var serializedObject = new SerializedObject(this);
            var animations = serializedObject.FindProperty("_animations");
            
            for (int i = 0; i < animations.arraySize; i++)
            {
                var animation = animations.GetArrayElementAtIndex(i);
                var nameProperty = animation.FindPropertyRelative("_name");
                
                if(string.IsNullOrEmpty(nameProperty.stringValue) == false)
                    continue;

                nameProperty.stringValue = animation.FindPropertyRelative("_animationClip").objectReferenceValue.name;
            }

            serializedObject.ApplyModifiedProperties();
        }*/

        public RuntimeAnimatorController AnimatorController => _animatorController;
        
        public IReadOnlyList<AnimatorSliceData> AnimationsSlice => _layerSlices;
    }
}