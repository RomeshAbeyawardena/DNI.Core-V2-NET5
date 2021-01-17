using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class SanitizeInputPreprocessorBase<TRequest> : MediatR.Pipeline.IRequestPreProcessor<TRequest>
    {
        public SanitizeInputPreprocessorBase(IModelSanitizer modelSanitizer)
        {
            this.modelSanitizer = modelSanitizer;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            modelSanitizer.SanitizeModel(request);
            return Task.CompletedTask;
        }

        private readonly IModelSanitizer modelSanitizer;
    }
}
