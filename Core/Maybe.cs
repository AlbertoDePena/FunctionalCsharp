using System;

namespace FunctionalCsharp
{
    public sealed class Maybe<T>
    {
        private readonly bool hasItem;
        private readonly T item;

        private Maybe()
        {
            hasItem = false;
        }

        private Maybe(T item)
        {
            this.item = item ?? throw new ArgumentNullException(nameof(item));
            hasItem = true;
        }

        public static Maybe<T> Nothing() => new Maybe<T>();

        public static Maybe<T> Something(T item) => new Maybe<T>(item);

        public TResult Match<TResult>(TResult nothing, Func<T, TResult> something)
        {
            if (nothing == null)
                throw new ArgumentNullException(nameof(nothing));
            if (something == null)
                throw new ArgumentNullException(nameof(something));

            return hasItem ? something(item) : nothing;
        }

        public bool IsNothing => Match(nothing: true, something: _ => false);

        public bool IsSomething => !IsNothing;

        public override bool Equals(object obj)
        {
            if (!(obj is Maybe<T> other))
                return false;

            return Match(
                nothing: !other.hasItem,
                something: x => other.Match(
                    nothing: !hasItem,
                    something: y => Equals(x, y)));
        }

        public override int GetHashCode() => Match(nothing: 0, something: x => x.GetHashCode());
    }
}
