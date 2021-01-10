using DNI.Core.Shared.Options;
using System;

namespace DNI.Core.Shared.Contracts
{
    public interface IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings>
    {
        IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> SetEncryptionClassification(IServiceProvider serviceProvider, 
            ref EncryptionOptions encryptionOptions, 
            Func<TApplicationSettings, EncryptionOptions> selector);
    }
}
