using Base.Application.Common.Interfaces;
using Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Queries
{
    public record GetPersonByIdQuery(string id) : IRequestWrapper<PersonEntity>;

    public class GetPersonByIdHandler : IRequestHandlerWrapper<GetPersonByIdQuery, PersonEntity>
    {
        private readonly IMongoRepository<PersonEntity> personRepository;

        public GetPersonByIdHandler(IMongoRepository<PersonEntity> personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<ServiceResult<PersonEntity>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await personRepository.FindByIdAsync(request.id);
            return (result != null) ? ServiceResult.Success(result) : ServiceResult.Failed<PersonEntity>(ServiceError.NotFound);
        }
    }
}
