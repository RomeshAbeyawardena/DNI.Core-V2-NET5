namespace DNI.Core.Shared
{
    /// <summary>
    /// Represents a range of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Range<T>
        where T : struct
    {
        /// <summary>
        /// Creates an instance of <see cref="Range{T}"/>
        /// </summary>
        /// <param name="minimum">The minimum value for this range</param>
        /// <param name="maximum">The maximum value for this range</param>
        public Range(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Gets the minimum value for this range
        /// </summary>
        public T Minimum { get; }

        /// <summary>
        /// Gets the maximum value for this range
        /// </summary>
        public T? Maximum { get; }
        
        public abstract bool IsInRange(T value);
    }
}
