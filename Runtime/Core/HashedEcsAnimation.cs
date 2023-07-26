using System;

namespace SnailBee.LeoEcsLite.SnailAnimation.Runtime.Core
{
    //TODO Maybe need added animation priority
    public readonly struct HashedEcsAnimation
    {
        public HashedEcsAnimation(int animationHash, EcsAnimation animation)
        {
            Name = animation.Name;
            Hash = animationHash;
            Priority = animation.Priority;
            TransitionDuration = animation.TransitionDuration;
            ClipDuration = animation.AnimationClip.length;
        }

        public HashedEcsAnimation(string name, int hash, int priority, float transitionDuration, float clipDuration)
        {
            Name = name;
            Hash = hash;
            Priority = priority;
            TransitionDuration = transitionDuration;
            ClipDuration = clipDuration;
        }

        public string Name { get; }

        public int Hash { get; }

        public int Priority { get; }

        public float TransitionDuration { get; }

        public float ClipDuration { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj.GetType() == GetType()
                   && Equals((HashedEcsAnimation) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hash, TransitionDuration);
        }

        public static bool operator ==(HashedEcsAnimation left, HashedEcsAnimation right)
        {
            return left.Hash == right.Hash;
        }

        public static bool operator !=(HashedEcsAnimation left, HashedEcsAnimation right)
        {
            return left.Hash != right.Hash;
        }

        public static bool operator >(HashedEcsAnimation left, HashedEcsAnimation right)
        {
            return left.Priority > right.Priority;
        }

        public static bool operator <(HashedEcsAnimation left, HashedEcsAnimation right)
        {
            return left.Priority < right.Priority;
        }

        public static HashedEcsAnimation Null => new(null, 0, 0, 0f, 0f);
    }
}