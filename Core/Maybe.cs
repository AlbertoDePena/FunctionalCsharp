using System;

namespace CSharp.Functional
{
    /// <inheritdocs/>
    public sealed class Maybe<T>
    {
        private readonly bool hasItem;
        private readonly T item;

        private Maybe()
        {
            hasItem = false;
        }

        private Maybe(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            item = value;
            hasItem = true;
        }

        /// <summary>
        /// Create a Maybe of nothing
        /// </summary>
        public static Maybe<T> Nothing() => new Maybe<T>();

        /// <summary>
        /// Create a Maybe of something
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Maybe<T> Something(T item) => new Maybe<T>(item);

        /// <summary>
        /// Map a function on something or return default on nothing
        /// </summary>
        /// <param name="nothing"></param>
        /// <param name="something"></param>
        /// <typeparam name="TResult"></typeparam>
        public TResult Match<TResult>(TResult nothing, Func<T, TResult> something)
        {
            if (nothing == null)
                throw new ArgumentNullException(nameof(nothing));
            if (something == null)
                throw new ArgumentNullException(nameof(something));

            return hasItem ? something(item) : nothing;
        }

        /// <summary>
        /// Is the Maybe nothing?
        /// </summary>
        public bool IsNothing => Match(nothing: true, something: _ => false);

        /// <summary>
        /// Is the Maybe something?
        /// </summary>
        public bool IsSomething => !IsNothing;

        /// <summary>
        /// Compare two Maybe objects
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// The Maybe's hash code
        /// </summary>
        public override int GetHashCode() => Match(nothing: 0, something: x => x.GetHashCode());
    }
}
