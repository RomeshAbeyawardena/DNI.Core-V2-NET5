﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IListBuilder<T> : IEnumerable<T>, IReadOnlyList<T>
    {
        IListBuilder<T> Add(T item);
        IListBuilder<T> AddRange(IEnumerable<T> item);
    }
}
