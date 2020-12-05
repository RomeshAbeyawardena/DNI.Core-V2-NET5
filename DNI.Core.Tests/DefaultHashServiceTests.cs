using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class DefaultHashServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            hashService = new DefaultHashService(HashAlgorithmName.SHA512);
        }

        [TestCase("12345!MySecureString!")]
        [TestCase("MySecure!String123456")]
        [TestCase("MySecureStringA56754")]
        [TestCase("$MySecure_String54358!")]
        public void HashTest(string expectedValue)
        {
            var hashedString = hashService.HashString(expectedValue, Encoding.ASCII);
            var comparedHashString = hashService.HashString(expectedValue, Encoding.ASCII);

            Assert.AreEqual(comparedHashString, hashedString);
        }

        private IHashService hashService;
    }
}
