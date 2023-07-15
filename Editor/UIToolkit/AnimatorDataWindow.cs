using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AnimationSystem.Editor.UIToolkit
{
    public class AnimatorDataWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset tree;
        
        [MenuItem("Window/Ecs Animation System/Animator Data")]
        public static void Open()
        {
            var window = GetWindow<AnimatorDataWindow>();
            window.titleContent = new GUIContent("Animator Data");
        }

        public void CreateGUI()
        {
            tree.CloneTree(rootVisualElement);
        }
    }
}