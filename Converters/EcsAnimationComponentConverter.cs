using SnailBee.LeoEcsLite.EntityConverter.src;
using SnailBee.LeoEcsLite.SnailAnimation.Components;

namespace SnailBee.LeoEcsLite.SnailAnimation.Converters
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