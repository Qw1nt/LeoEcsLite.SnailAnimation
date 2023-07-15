using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AnimationSystem.Core;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AnimationSystem.Editor.UIToolkit.CreationWindow
{
    public class AnimatorDataCreationWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset tree;
        [SerializeField] private VisualTreeAsset animationPreview;
        private readonly List<EcsAnimation> _animations = new();
        private Elements _elements;
        private CreateAnimationElements _createElements;

        [MenuItem("Window/Ecs Animation System/Create Animator Data")]
        public static void Open()
        {
            var window = GetWindow<AnimatorDataCreationWindow>();
            window.titleContent = new GUIContent("Create Animator Data");
        }

        public void CreateGUI()
        {
            tree.CloneTree(rootVisualElement);
            _elements = new Elements(rootVisualElement);
            _createElements = new CreateAnimationElements(rootVisualElement);
            
            _createElements.AddAnimationButton.clicked += AddAnimation;
            _elements.CreateAnimationParent.SetEnabled(false);
            
            _elements.SourceAnimator.RegisterValueChangedCallback(InitAnimator);
            _createElements.ClipReferenceField.SetEnabled(false);
            _createElements.ClipName.RegisterValueChangedCallback(callback =>
            {
                var animatorController = (AnimatorController) _elements.SourceAnimator.value;
                var animation = animatorController.animationClips
                    .FirstOrDefault(x => x.name == callback.newValue);
                
                if (animation is null)
                    return;

                _createElements.ClipReferenceField.value = animation;
            });
            
            _elements.SaveAssetButton.clicked += Create;
        }

        private void InitAnimator(ChangeEvent<Object> callback)
        {
            if (callback.newValue is not AnimatorController animator)
            {
                _elements.CreateAnimationParent.SetEnabled(false);
                return;
            }

            var clips = new List<string>(animator.animationClips.Length);
            clips.AddRange(animator.animationClips.Select(clip => clip.name));
            _createElements.ClipName.choices = clips;
            _elements.InitialAnimationDropdown.choices = new List<string>();
            _elements.CreateAnimationParent.SetEnabled(true);
        }
        
        private void AddAnimation()
        {
            if (StringIsEmpty(_createElements.AnimationKey.value, "Animation key field is empty") == true)
                return;

            if (StringIsEmpty(_createElements.ClipName.value, "Clip name is not select") == true)
                return;

            VisualElement element = animationPreview.CloneTree();
            var ecsAnimation = SetupAddedAnimation(element);

            _createElements.AnimationKey.value = "";
            _createElements.ClipName.index = 1;
            _createElements.ClipReferenceField.value = null;
            _createElements.ClipName.choices.Remove(ecsAnimation.AnimationClip.name);
            _elements.InitialAnimationDropdown.choices.Add(ecsAnimation.Name);

            _animations.Add(ecsAnimation);
            _elements.ScrollView.Add(element);
        }

        private EcsAnimation SetupAddedAnimation(VisualElement element)
        {
            AnimationClip clip = (AnimationClip) _createElements.ClipReferenceField.value;
            string animationKey = _createElements.AnimationKey.value;
            float transitionDuration = _createElements.TransitionDuration.value;
            string clipName = _createElements.ClipName.text;
            int priority = _animations.Count + 1;
            
            var animationClipField = element.Q<ObjectField>("AnimationClip");
            animationClipField.value = clip;
            animationClipField.SetEnabled(false);

            var ecsAnimation = new EcsAnimation(animationKey, priority, transitionDuration, clip);

            element.Q<Button>("SelectAnimationClipButton").clicked += () =>
            {
                Selection.SetActiveObjectWithContext(animationClipField.value, null);
            };
            
            element.Q<Label>("AnimationKey").text = animationKey;
            element.Q<Label>("TransitionDuration").text = transitionDuration.ToString(CultureInfo.InvariantCulture);
            element.Q<Label>("ClipName").text = clipName;
            element.Q<Button>("DeleteAnimationButton").clicked += () =>
            {
                _animations.Remove(ecsAnimation);
                _elements.ScrollView.Remove(element);
                _createElements.ClipName.choices.Add(ecsAnimation.AnimationClip.name);
                _elements.InitialAnimationDropdown.choices.Remove(ecsAnimation.Name);
            };

            return ecsAnimation;
        }

        private void Create()
        {
            var assetPath = CreateEmptyAsset();
            if(string.IsNullOrEmpty(assetPath) == true)
                return;
            
            SetupAsset(assetPath);
        }
        
        private string CreateEmptyAsset()
        {
            string sourcePath = _elements.SavePath.value;
            string assetName = _elements.AssetName.value;

            if (StringIsEmpty(assetName, "Asset name is empty") == true)
                return null;
            
            string savePath = Path.Combine("Assets", sourcePath);
            string assetPath = Path.Combine(savePath, assetName);
            assetPath = Path.ChangeExtension(assetPath, "asset");

            if (Directory.Exists(savePath) == false)
            {
                EditorUtility.DisplayDialog("Error", savePath, "Ok");
                return null;
            }

            AssetDatabase.CreateAsset(CreateInstance<EcsAnimatorData>(), assetPath);
            return assetPath;
        }

        private void SetupAsset(string assetPath)
        {
            var asset = AssetDatabase.LoadAssetAtPath<EcsAnimatorData>(assetPath);
            SerializedObject serializedObject = new SerializedObject(asset);
            serializedObject.FindProperty("animatorController").objectReferenceValue = _elements.SourceAnimator.value;
            var animationsListProperty = serializedObject.FindProperty("animations");
            
            animationsListProperty.arraySize = _animations.Count;

            for (int i = 0; i < animationsListProperty.arraySize; i++)
            {
                var arrayElement = animationsListProperty.GetArrayElementAtIndex(i);
                var ecsAnimation = _animations[i];
                
                arrayElement.FindPropertyRelative("name").stringValue = ecsAnimation.Name;
                arrayElement.FindPropertyRelative("priority").intValue = ecsAnimation.Priority;
                arrayElement.FindPropertyRelative("transitionDuration").floatValue = ecsAnimation.TransitionDuration;
                arrayElement.FindPropertyRelative("animationClip").objectReferenceValue = ecsAnimation.AnimationClip;
            }

            serializedObject.ApplyModifiedProperties();

            AssetDatabase.SaveAssets();

            _elements.AssetName.value = "";
            _elements.SavePath.value = "";
            _elements.ScrollView.Clear();
        }

        private bool StringIsEmpty(string value, string errorMessage)
        {
            if (string.IsNullOrEmpty(value) == false)
                return false;

            EditorUtility.DisplayDialog("ERROR", errorMessage, "Ok");
            return true;
        }
        
        private class CreateAnimationElements
        {
            public CreateAnimationElements(VisualElement root)
            {
                AnimationKey = root.Q<TextField>("AnimationKey");
                TransitionDuration = root.Q<FloatField>("TransitionDurationField");
                ClipName = root.Q<DropdownField>("AnimationClipsDropdown");
                ClipReferenceField = root.Q<ObjectField>("ReferenceClipField");
                AddAnimationButton = root.Q<Button>("AddAnimationButton");
            }
            
            public TextField AnimationKey { get; }
            
            public FloatField TransitionDuration { get; }
            
            public DropdownField ClipName { get; }
            
            public ObjectField ClipReferenceField { get; }
            
            public Button AddAnimationButton { get; }
        }
        
        private class Elements
        {
            public Elements(VisualElement root)
            {
                SavePath= root.Q<TextField>("SavePathField");
                AssetName = root.Q<TextField>("AssetNameField");
                SourceAnimator = root.Q<ObjectField>("SourceAnimatorField");
                CreateAnimationParent = root.Q<VisualElement>("CreateAnimationFields");
                InitialAnimationDropdown = root.Q<DropdownField>("InitialAnimationDropdown");
                ScrollView = root.Q<ScrollView>("PreviewAnimationList");
                SaveAssetButton = root.Q<Button>("SaveAssetButton");
            }
            
            public TextField SavePath { get; }
            
            public TextField AssetName { get; }

            public ObjectField SourceAnimator { get; }
            
            public VisualElement CreateAnimationParent { get; }
            
            public DropdownField InitialAnimationDropdown { get; }
            
            public ScrollView ScrollView { get; }
            
            public Button SaveAssetButton { get; }
        }
    }
}
