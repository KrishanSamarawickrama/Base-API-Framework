using Base.Application.Common;
using Base.Application.Common.Interfaces;
using Base.Application.Dto;
using Base.Domain.Entities;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Commands
{
    public record AddPersonCommand(AddPersonDto dto) : IRequestWrapper<PersonEntity>;

    public class AddPersonHandler : BaseRequestHandler<AddPersonCommand, PersonEntity, PersonEntity>
    {
        private readonly IValidator<AddPersonCommand> validator;

        public AddPersonHandler(IMongoRepository<PersonEntity> repo, ILogger<PersonEntity> logger, IValidator<AddPersonCommand> validator) : base(repo, logger)
        {
            this.validator = validator;
        }

        public override async Task<ServiceResult<PersonEntity>> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            var valid = validator.Validate(request);
            if (valid.IsValid)
            {
                var person = request.dto.Adapt<PersonEntity>();
                await repo.InsertOneAsync(person);
                return ServiceResult.Success(person);
            }

            return ServiceResult.Failed<PersonEntity>(ServiceError.CustomMessage(valid.ToString()));
        }

    }
}
