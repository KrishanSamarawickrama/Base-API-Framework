using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Application.Commands;
using FluentValidation;

namespace Base.Application.Validators
{
    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
        {
            RuleFor(v => v.dto.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .NotNull().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name must not exceed 100 characters.");

            RuleFor(v => v.dto.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .NotNull().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name must not exceed 100 characters.");
        }
    }
}
