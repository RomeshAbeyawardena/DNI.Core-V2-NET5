using System.Collections.Generic;
using System.Text;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IHashService
    {
        /// <summary>
        /// <para>Hashes a value using specified parameters</para>
        /// <para>Use <see cref="HashString(string, Encoding)" /> to use default values</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="totalNumberOfBytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        IEnumerable<byte> Hash(string value, string salt, int iterations, int totalNumberOfBytes, Encoding encoding);

        /// <summary>
        /// <para>Hashes a value using specified parameters</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        string HashString(string value, Encoding encoding);

        /// <summary>
        /// <para>Hashes a value using specified parameters</para>
        /// <para>Use <see cref="HashString(string, Encoding)" /> to use default values</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="totalNumberOfBytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        string HashString(string value, string salt, int iterations, int totalNumberOfBytes, Encoding encoding);

        /// <summary>
        /// Gets the algorithm name used to hash values
        /// </summary>
        string AlgorithmName { get; }
    }
}
