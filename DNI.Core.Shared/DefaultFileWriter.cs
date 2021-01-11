using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    internal class DefaultFileWriter : IFileWriter
    {
        public static IFileWriter Create()
        {
            return new DefaultFileWriter();
        }

        public Task<IAttempt> Save(string content, string fileName)
        {
            return WriteFile(fileName, async(fs,sw) => await sw.WriteAsync(content));
        }

        public Task<IAttempt> Save(IEnumerable<byte> byteData, string fileName)
        {
            return WriteFile(fileName, async(fs, sw) => await fs.WriteAsync(byteData.ToArray()));
        }

        private async Task<IAttempt> WriteFile(string fileName, Func<FileStream, StreamWriter, Task> action)
        {
            using var fileStream = File.OpenWrite(fileName);

            using var streamWriter = new StreamWriter(fileStream);
            await action(fileStream, streamWriter);

            return Attempt.Success();
        }

        private DefaultFileWriter()
        {

        }
    }
}
