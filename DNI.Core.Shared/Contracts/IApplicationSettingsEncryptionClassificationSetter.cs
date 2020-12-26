using DNI.Core.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings>
    {
        IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> SetEncryptionClassification(IServiceProvider serviceProvider, 
            ref EncryptionOptions encryptionOptions, 
            Func<TApplicationSettings, EncryptionOptions> selector);
    }
}
