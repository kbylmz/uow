using System;
using System.Collections;
using System.Collections.Generic;

namespace ManagementService.Domain
{
    public struct Optional<T> : IEnumerable<T>, IEquatable<Optional<T>>
    {
        /// <summary>
        ///     The empty instance.
        /// </summary>
        public static readonly Optional<T> Empty = new Optional<T>();

        readonly bool hasValue;
        readonly T value;


        /// <summary>
        ///     Initializes a new <see cref="Optional{T}" /> instance.
        /// </summary>
        /// <param name="value">The value to initialize with.</param>
        public Optional(T value)
        {
            hasValue = true;
            this.value = value;
        }

        /// <summary>
        ///     Gets an indication if this instance has a value.
        /// </summary>
        public bool HasValue
        {
            get { return hasValue; }
        }

        /// <summary>
        ///     Gets the value associated with this instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this instance has no value.</exception>
        public T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException("Optional object must have a value.");
                }
                return value;
            }
        }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (HasValue)
            {
                yield return value;
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Determines whether the specified <see cref="Optional{T}" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Optional{T}" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="Optional{T}" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Optional<T> other)
        {
            return hasValue.Equals(other.hasValue) &&
                   EqualityComparer<T>.Default.Equals(value, other.value);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Optional<T>)obj);
        }

        /// <summary>
        ///     Determines whether <see cref="Optional{T}">instance 1</see> is equal to <see cref="Optional{T}">instance 2</see>.
        /// </summary>
        /// <param name="instance1">The first instance.</param>
        /// <param name="instance2">The second instance.</param>
        /// <returns>
        ///     <c>true</c> if <see cref="Optional{T}">instance 1</see> is equal to <see cref="Optional{T}">instance 2</see>;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Optional<T> instance1, Optional<T> instance2)
        {
            return instance1.Equals(instance2);
        }

        /// <summary>
        ///     Determines whether <see cref="Optional{T}">instance 1</see> is not equal to
        ///     <see cref="Optional{T}">instance 2</see>.
        /// </summary>
        /// <param name="instance1">The first instance.</param>
        /// <param name="instance2">The second instance.</param>
        /// <returns>
        ///     <c>true</c> if <see cref="Optional{T}">instance 1</see> is not equal to
        ///     <see cref="Optional{T}">instance 2</see>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Optional<T> instance1, Optional<T> instance2)
        {
            return !instance1.Equals(instance2);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return hasValue.GetHashCode() ^ EqualityComparer<T>.Default.GetHashCode(value) ^ typeof(T).GetHashCode();
        }
    }
}