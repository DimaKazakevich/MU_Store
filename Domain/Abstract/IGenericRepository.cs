﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Abstract
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
