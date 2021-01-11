using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public interface IFileWriter
    {
        Task<IAttempt> Save(string content, string fileName);
        Task<IAttempt> Save(IEnumerable<byte> byteData, string fileName);
    }
}
