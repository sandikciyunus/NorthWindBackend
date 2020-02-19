using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
   public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.CategoryName).NotEmpty();
            RuleFor(p => p.CategoryName).Length(2, 30);
        }

    }
}
