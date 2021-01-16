using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    internal class DefaultFileWriter : IFileWriter
    {
        public static IFileWriter Create()
        {
            return new DefaultFileWriter();
        }

        public Task<IAttempt> Save(string content, string fileName, bool discardExistingData)
        {
            return WriteFile(fileName, async(fs, sw) => await sw.WriteAsync(content), discardExistingData);
        }

        public Task<IAttempt> Save(IEnumerable<byte> byteData, string fileName, bool discardExistingData)
        {
            return WriteFile(fileName, async(fs, sw) => await fs.WriteAsync(byteData.ToArray()), discardExistingData);
        }

        private async Task<IAttempt> WriteFile(string fileName, Func<FileStream, StreamWriter, ValueTask> action, bool discardExistingData = false)
        {
            using var fileStream = File.OpenWrite(fileName);

            if(discardExistingData)
            { 
                fileStream.SetLength(0);
            }

            using var streamWriter = new StreamWriter(fileStream);
            
            await action(fileStream, streamWriter);


            return Attempt.Success();
        }

        private DefaultFileWriter()
        {

        }
    }
}
