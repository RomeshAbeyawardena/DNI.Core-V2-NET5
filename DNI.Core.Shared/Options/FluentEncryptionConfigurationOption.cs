﻿using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Options
{
    public class FluentEncryptionConfigurationOption<T> : IFluentEncryptionConfigurationOption<T>
    {
        public Expression<Func<T, object>> PropertyExpression { get; set; }

        public EncryptionPolicy Policy { get; set; }

        public Func<T, string> GetPropertyString { get; set; }
    }
}
