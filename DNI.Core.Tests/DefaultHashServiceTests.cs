using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Services;
using NUnit.Framework;
using System.Security.Cryptography;
using System.Text;

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

        [TestCase("12345!MySecureString!", "Salt45435435654", 16)]
        [TestCase("MySecure!String123456", "My_4543535_Salt", 32)]
        [TestCase("MySecureStringA56754", "878678678_Salt", 64)]
        [TestCase("$MySecure_String54358!", "!tergret5nght", 128)]
        public void HashTest(string expectedValue, string salt, int totalNumberOfBytes)
        {
            var hashedString = hashService.HashString(expectedValue, salt, 10000, totalNumberOfBytes, Encoding.ASCII);
            var comparedHashString = hashService.HashString(expectedValue, salt, 10000, totalNumberOfBytes, Encoding.ASCII);

            Assert.AreEqual(comparedHashString, hashedString);
        }

        private IHashService hashService;
    }
}
