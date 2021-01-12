using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.ExpressionVisitors;
using DNI.Core.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DNI.Core.Abstractions
{
    internal class DefaultSettingConfigurator<TSetting> : ISettingConfigurator<TSetting>
    {
        public DefaultSettingConfigurator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDictionary<string, Func<IConfigurationSection, object>> Values { get; }
        public IConfigurationSection Section { get; private set; }

        public ISettingConfigurator<TSetting> Configure<T>(
            Expression<Func<TSetting, T>> settingExpression, 
            Func<IConfigurationSection, T> sectionValue)
        {
            var visitor = new ModelExpressionVisitor();

            var member = visitor.GetLastVisitedMember(settingExpression);
            Values.Add(member, section => sectionValue(section));
            return this;
        }

        public ISettingConfigurator<TSetting> ConfigureSection(Func<IConfiguration, IConfigurationSection> sectionConfiguration)
        {
            Section = sectionConfiguration(configuration);
            return this;
        }

        public void ConfigureSettings(TSetting settings)
        {
            var settingType = typeof(TSetting);

            foreach(var (key, value) in Values)
            {
                var property = settingType.GetProperty(key);
                var settingValue = value.Invoke(Section);
                if (property == null 
                    || settingValue.IsDefault())
                {
                    continue;
                }

                property.SetValue(settings, settingValue);
            }
        }

        private readonly IConfiguration configuration;
    }
}
