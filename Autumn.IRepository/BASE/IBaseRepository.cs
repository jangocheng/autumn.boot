﻿using Autumn.FrameWork;
using SqlDbLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        Task<TEntity> QueryById(object objId);

        Task<TEntity> QueryById(object objId, bool blnUseCache = false);

        Task<List<TEntity>> QueryByIDs(object[] lstIds);

        Task<int> Insert(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> Delete(TEntity model);

        Task<bool> DeleteByIds(object[] ids);

        Task<bool> Update(TEntity model);

        Task<bool> Update(TEntity entity, string strWhere);

        Task<List<TEntity>> Query();

        Task<List<TEntity>> Query(string strWhere);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);

        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);

        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);

        Task<List<TEntity>> QueryPage(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds, RefAsync<int> intTotalCount);

        Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null);

        void BeginTran(IsolationLevel iso = IsolationLevel.ReadCommitted);

        void CommitTran();

        void RollbackTran();

        DbContext Context();
    }
}
