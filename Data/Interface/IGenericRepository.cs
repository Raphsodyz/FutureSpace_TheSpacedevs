﻿using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IGenericRepository<T> : IRepository
    {
        IList<T> GetAll(
            IEnumerable<Expression<Func<T, bool>>> filters = null,
            Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null,
            string includedProperties = "");

        IList<T> GetMany(
            IEnumerable<Expression<Func<T, bool>>> filters = null,
            Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null,
            string includedProperties = "",
            int? howMany = null);

        IEnumerable<TResult> GetAllSelectedColumns<TResult>(
            IEnumerable<Expression<Func<T, bool>>> filters = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includedProperties = "",
            int? howMany = null,
            Func<T, TResult> selectColumns = null);

        Pagination<T> GetAllPaged(int page, int pageSize,
            IEnumerable<Expression<Func<T, bool>>> filters = null,
            Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null,
            string includedProperties = "");

        int EntityCount(Expression<Func<T, bool>> filter = null);

        T Get(Expression<Func<T, bool>> filter, string includedProperties = "");

        TResult GetSelected<TResult>(
            Expression<Func<T, bool>> filter = null,
            string includedProperties = "",
            Func<T, TResult> selectColumns = null);

        void Save(T entity);
        void SaveTransaction(T entity);

        void Delete(T entity);
        void DeleteTransaction(T entity);
    }
}