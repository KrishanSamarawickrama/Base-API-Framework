using Base.Application.Common.Interfaces;
using Base.Application.Dto;
using Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MongoDB.Bson;

namespace Base.Application.Commands
{
    public record EditPersonCommand(string id, AddPersonDto dto) : IRequestWrapper<PersonEntity>;

    public class EditPersonHandler : IRequestHandlerWrapper<EditPersonCommand, PersonEntity>
    {
        private readonly IMongoRepository<PersonEntity> personRepository;

        public EditPersonHandler(IMongoRepository<PersonEntity> personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<ServiceResult<PersonEntity>> Handle(EditPersonCommand request, CancellationToken cancellationToken)
        {
            var person = request.dto.Adapt<PersonEntity>();
            person.Id = new ObjectId(request.id);
            await personRepository.ReplaceOneAsync(person);

            return ServiceResult.Success(person);
        }
    }
}
