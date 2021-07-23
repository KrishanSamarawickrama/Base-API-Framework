using Base.Application.Common.Interfaces;
using Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Commands
{
    public record DeletePersonCommand(string id) : IRequestWrapper<bool>;

    public class DeletePersonHandler : IRequestHandlerWrapper<DeletePersonCommand, bool>
    {
        private readonly IMongoRepository<PersonEntity> personRepository;

        public DeletePersonHandler(IMongoRepository<PersonEntity> personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<ServiceResult<bool>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            await personRepository.DeleteByIdAsync(request.id);
            return ServiceResult.Success(true);
        }
    }
}
