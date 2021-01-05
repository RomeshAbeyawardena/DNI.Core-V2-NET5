using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    internal class DefaultConfigurationBuilder : IConfigurationBuilder
    {
        public IDictionary<string, object> Properties => throw new NotImplementedException();

        public IList<IConfigurationSource> Sources => throw new NotImplementedException();

        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            throw new NotImplementedException();
        }

        public IConfigurationRoot Build()
        {
            throw new NotImplementedException();
        }
    }
}
