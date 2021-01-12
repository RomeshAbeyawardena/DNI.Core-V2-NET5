using DNI.Core.Abstractions.Extensions;
using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DNI.Core.Abstractions
{
    internal class RandomStringGenerator : IRandomStringGenerator
    {
        public static IRandomStringGenerator Create(RandomNumberGenerator randomNumberGenerator)
        {
            return new RandomStringGenerator(randomNumberGenerator ?? RandomNumberGenerator.Create());
        }

        public string GenerateString(int length)
        {
            var characters = GetRandomBytes(length,
                new Range(48, 57), //numerics
                new Range(65, 90), //upper case alphanumerics
                new Range(97, 122)); //lower case alphanumerics
            
            return new string(characters.Select(c => (char)c).ToArray());
        }

        public IEnumerable<byte> GetRandomBytes(int length, params Range[] ranges)
        {
            var buffer = new byte[length * 6];

            randomNumberGenerator.GetNonZeroBytes(buffer);

            var inRangeValues = buffer.Where(b => ranges.Any(range => b >= range.Start.Value && b <= range.End.Value));

            var inRangeValuesLength = inRangeValues.Count();
            if (inRangeValuesLength < length)
            {
                inRangeValues = inRangeValues.Append(GetRandomBytes(length - inRangeValuesLength, ranges));
            }

            if(inRangeValuesLength > length)
            {
                return inRangeValues.Take(length);
            }

            return inRangeValues;
        }

        private RandomStringGenerator(RandomNumberGenerator randomNumberGenerator)
        {
            this.randomNumberGenerator = randomNumberGenerator;
        }

        private readonly RandomNumberGenerator randomNumberGenerator;
    }
}
