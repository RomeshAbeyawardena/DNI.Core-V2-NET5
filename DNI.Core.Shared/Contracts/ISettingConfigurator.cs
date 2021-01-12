﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DNI.Core.Shared.Contracts
{
    public interface ISettingConfigurator<TSetting>
    {
        ISettingConfigurator<TSetting> ConfigureSection(Func<IConfiguration, IConfigurationSection> sectionConfiguration);
        ISettingConfigurator<TSetting> Configure<T>(Expression<Func<TSetting, T>> setting, Func<IConfigurationSection, T> sectionValue);
        IConfigurationSection Section { get; }
        IDictionary<string, Func<IConfigurationSection, object>> Values { get; }
    }
}
