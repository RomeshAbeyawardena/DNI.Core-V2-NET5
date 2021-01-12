using DNI.Core.Shared.Options;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace DNI.Core.Abstractions
{
    public abstract class ApplicationSettingsBase
    {
        protected static EncryptionOptions GetEncryptionOptions(IConfiguration configuration, 
            string sectionName, string encryptionSetting,
            string hashAlgorithNameKey = "HashAlgorithName",
            string encodingKey = "Encoding")
        {
            var settings = configuration
                .GetSection(sectionName)
                .GetSection(encryptionSetting);
            var encryptionOptions = settings
                .Get<EncryptionOptions>();
            encryptionOptions.HashAlgorithName = new HashAlgorithmName(settings.GetValue<string>(hashAlgorithNameKey));
            encryptionOptions.Encoding = Encoding.GetEncoding(settings.GetValue<string>(encodingKey)); 

            return encryptionOptions;
        }
    }
}
