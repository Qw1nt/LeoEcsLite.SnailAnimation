using AB_Utility.FromSceneToEntityConverter;
using AnimationSystem.Components;

namespace AnimationSystem.Converters
{
    public class EcsAnimationComponentConverter : ComponentConverter<EcsAnimatorComponent>
    {
        private void Reset()
        {
            if(TryGetComponent(out InitEcsAnimationComponentConverter initEvent) == true)
                return;

            gameObject.AddComponent<InitEcsAnimationComponentConverter>();
        }
    }
}