﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IDictionaryBuilder<TKey, TValue> : IDictionary<TKey, TValue>
    {
        new IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair);
        new IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);
    }
}
