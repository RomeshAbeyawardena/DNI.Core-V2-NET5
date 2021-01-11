using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public class FileCacheDependency : ICacheDependency
    {
        public ICacheDependencyOptions Options { get; }
        public FileCacheDependencyOptions FileOptions => Options as FileCacheDependencyOptions;
        public async Task<bool> Verify(string key, CancellationToken cancellationToken)
        {
            var dictionary = await GetDictionary();

            return dictionary.TryGetValue(key, out var keyElapsedDate) 
                && keyElapsedDate < systemClock.Now;
        }

        public async Task Update(string key, CancellationToken cancellationToken)
        {
            var dictionary = await GetDictionary();
            
            dictionary.TryAddOrUpdate(key, systemClock.Now.Add(Options.ElapsedPeriod));

            var attempt = await fileWriter.Save(
                JsonConvert.SerializeObject(dictionary), 
                FileOptions.DependencyFile);
        }

        public FileCacheDependency(
            ICacheDependencyOptions options, 
            ISystemClock systemClock, 
            IFileProvider fileProvider,
            IFileWriter fileWriter)
        {
            Options = options;
            this.systemClock = systemClock;
            this.fileProvider = fileProvider;
            this.fileWriter = fileWriter;
        }

        private async Task<IDictionary<string, DateTimeOffset>> GetDictionary()
        {
            using var fileStream = fileProvider
                .GetFileInfo(FileOptions.DependencyFile)
                .CreateReadStream();

            using var streamReader = new StreamReader(fileStream);

            var content = await streamReader.ReadToEndAsync();

            if(string.IsNullOrEmpty(content))
            {
               return new Dictionary<string, DateTimeOffset>();
            }

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, DateTimeOffset>>(content);

            return dictionary ?? new Dictionary<string, DateTimeOffset>();
        }

        private readonly ISystemClock systemClock;
        private readonly IFileProvider fileProvider;
        private readonly IFileWriter fileWriter;
    }
}
