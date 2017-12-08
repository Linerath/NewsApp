using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace NewsApp.Models
{
    public class NewsValidator : AbstractValidator<News>
    {
        public NewsValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Heading).NotNull().Length(5, 30);
            RuleFor(x => x.Text).NotNull().MinimumLength(5);
        }
    }
}
