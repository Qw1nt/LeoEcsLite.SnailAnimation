using SnailBee.LeoEcsLite.EntityConverter.src;
using SnailBee.LeoEcsLite.SnailAnimation.Runtime.Components;

namespace SnailBee.LeoEcsLite.SnailAnimation.Runtime.Converters
{
    public class EcsAnimatorComponentConverter : ComponentConverter<EcsAnimatorComponent>
    {
        private void Reset()
        {
            if(TryGetComponent(out InitEcsAnimationComponentConverter initEvent) == true)
                return;

            gameObject.AddComponent<InitEcsAnimationComponentConverter>();
        }
    }
}