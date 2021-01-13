﻿using DNI.Core.Shared.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public interface IFileWriter
    {
        Task<IAttempt> Save(string content, string fileName, bool discardExistingData = false);
        Task<IAttempt> Save(IEnumerable<byte> byteData, string fileName, bool discardExistingData = false);
    }
}
