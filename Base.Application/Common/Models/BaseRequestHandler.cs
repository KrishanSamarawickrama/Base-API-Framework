using Base.Application.Common.Interfaces;
using Base.Application.Queries;
using Base.Domain.Common;
using Base.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Common
{
    public abstract class BaseRequestHandler<TIn, TOut, TRepositoryObj> : IRequestHandlerWrapper<TIn, TOut> 
        where TIn : IRequestWrapper<TOut>
        where TRepositoryObj : IDocument
    {
        protected readonly IMongoRepository<TRepositoryObj> repo;
        protected readonly ILogger<TRepositoryObj> logger;

        protected BaseRequestHandler(
            IMongoRepository<TRepositoryObj> repo, 
            ILogger<TRepositoryObj> logger)
        {
            this.repo = repo;
            this.logger = logger;
        }

        public abstract Task<ServiceResult<TOut>> Handle(TIn request, CancellationToken cancellationToken);

    }
}
