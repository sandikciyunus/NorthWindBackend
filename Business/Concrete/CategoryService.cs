using Business.Abstract;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
   public class CategoryService:ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [ValidationAspect(typeof(CategoryValidator),Priority =1)]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccesResult(Messages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccesResult(Messages.CatgeoryDeleted);
        }

        public IDataResult<IList<Category>> GetAll()
        {
            return new SuccesDataResult<IList<Category>>(_categoryDal.GetList().ToList());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccesDataResult<Category>(_categoryDal.Get(p => p.CategoryId == categoryId));
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccesResult(Messages.CategoryUpdated);
        }
    }
}
