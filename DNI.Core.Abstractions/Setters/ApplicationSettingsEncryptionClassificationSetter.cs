using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Core.Abstractions.Setters
{
    public static class ApplicationSettingsEncryptionClassificationSetter
    {
        public static IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> Create<TApplicationSettings>()
        {
            return new ApplicationSettingsEncryptionClassificationSetter<TApplicationSettings>();
        }
    }

    internal class ApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> : IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings>
    {
        public IApplicationSettingsEncryptionClassificationSetter<TApplicationSettings> SetEncryptionClassification(IServiceProvider serviceProvider, ref EncryptionOptions encryptionOptions, Func<TApplicationSettings, EncryptionOptions> selector)
        {
            var applicationSettings = serviceProvider.GetRequiredService<TApplicationSettings>(); 
            var options = selector(applicationSettings); 
            encryptionOptions.Set(options); 

            return this;
        }

        internal ApplicationSettingsEncryptionClassificationSetter()
        {

        }
    }
}
