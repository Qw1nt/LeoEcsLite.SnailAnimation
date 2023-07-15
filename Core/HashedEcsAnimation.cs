using System;

namespace AnimationSystem.Core
{
    //TODO Maybe need added animation priority
    public readonly struct HashedEcsAnimation
    {
        public HashedEcsAnimation(int animationHash, int priority, float transitionDuration)
        {
            Hash = animationHash;
            Priority = priority;
            TransitionDuration = transitionDuration;
        }

        public int Hash { get; }

        public int Priority { get; }

        public float TransitionDuration { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj.GetType() == GetType()
                   && Equals((HashedEcsAnimation)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hash, TransitionDuration);
        }

        public static bool operator ==(HashedEcsAnimation left, HashedEcsAnimation right) => left.Hash == right.Hash;

        public static bool operator !=(HashedEcsAnimation left, HashedEcsAnimation right) => left.Hash != right.Hash;

        public static HashedEcsAnimation Null => new(0, -1, 0f);
    }
}