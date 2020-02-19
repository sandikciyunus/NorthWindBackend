using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private IProductDal _productDal;
       
      
        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
            
        }

        [ValidationAspect(typeof(ProductValidator),Priority =1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccesResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccesResult(Messages.ProductDeleted);
        }

        [PerformanceAspect(5)]
        public IDataResult<IList<Product>> GetAll()
        {
            Thread.Sleep(5000);
            return new SuccesDataResult<IList<Product>>(_productDal.GetList().ToList());   
        }

        public IDataResult<Product> GetById(int productId)
        {
           
            return new SuccesDataResult<Product>(_productDal.Get(p => p.ProductId == productId)); 
        }

       // [SecuredOperation("Product.List,Admin")]
        [CacheAspect(duration:10)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<IList<Product>> GetListByCategory(int categoryId)
        {
            return new SuccesDataResult<IList<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccesResult(Messages.ProductUpdated);
        }

        public IResult Update(Product product)
        {
             _productDal.Update(product);
            return new SuccesResult(Messages.ProductUpdated);
        }
    }
}
