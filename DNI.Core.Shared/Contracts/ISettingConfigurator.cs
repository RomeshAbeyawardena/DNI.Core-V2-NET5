using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface ISettingConfigurator<TSetting>
    {
        ISettingConfigurator<TSetting> ConfigureSection(Func<IConfiguration, IConfigurationSection> sectionConfiguration);
        ISettingConfigurator<TSetting> Configure<T>(Expression<Func<TSetting, T>> setting, Func<IConfigurationSection, T> sectionValue);
        IConfigurationSection Section { get; }
        IDictionary<Tuple<Expression<Func<TSetting, object>>, Func<IConfigurationSection, object>> Values { get; }
    }
}
