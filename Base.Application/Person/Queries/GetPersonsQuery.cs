using Base.Application.Common;
using Base.Application.Common.Interfaces;
using Base.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Queries
{
    public record GetPersonsQuery(PaginationAndSort dto) : IRequestWrapper<GetAdvanceResult<PersonEntity>>;

    public class GetPersonsHandler : BaseRequestHandler<GetPersonsQuery, GetAdvanceResult<PersonEntity>, PersonEntity>
    {
        public GetPersonsHandler(IMongoRepository<PersonEntity> repo, ILogger<PersonEntity> logger) : base(repo, logger)
        {
        }

        public override async Task<ServiceResult<GetAdvanceResult<PersonEntity>>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("All person loading.");
            var output = await repo.GetAdvance(request.dto);
            return output.DataCollection.Count > 0 ? ServiceResult.Success(output) : ServiceResult.Failed<GetAdvanceResult<PersonEntity>>(ServiceError.NotFound);
        }
    }
}
