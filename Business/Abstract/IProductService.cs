using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<IList<Product>> GetAll();
        IDataResult<IList<Product>> GetListByCategory(int categoryId);
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Delete(Product product);
        
        IResult Update(Product product);

        IResult TransactionalOperation(Product product);


    }
}
