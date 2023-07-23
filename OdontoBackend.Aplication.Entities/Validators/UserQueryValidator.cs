using FluentValidation;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Aplication.Entities.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Validators
{
    public class UserQueryValidator : AbstractValidator<UserByCiPasQuery>
    {

        public UserQueryValidator()
        {


            //RuleFor(x => x.descripcion).NotNull().WithMessage(Messages.NotNull);
            //RuleFor(x => x.descripcion).NotEmpty().WithMessage(Messages.NotEmpty);
            //RuleFor(x => x.descripcion).Length(1, 250).WithMessage(String.Format(Messages.Length, 1, 250));
            //RuleFor(x => x.descripcion).Matches("^[^<>]*$").WithMessage(Messages.SpecialChar);

            //RuleFor(x => x.estado).NotNull().WithMessage(Messages.NotNull);
            //RuleFor(x => x.estado).NotEmpty().WithMessage(Messages.NotEmpty);
            //RuleFor(x => x.estado).GreaterThanOrEqualTo(1);

        }


    }
}
