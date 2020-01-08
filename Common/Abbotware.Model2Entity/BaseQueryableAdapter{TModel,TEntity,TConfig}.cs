// -----------------------------------------------------------------------
// <copyright file="BaseQueryableAdapter{TModel,TEntity,TConfig}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Model2Entity.Extensions;
    using Abbotware.Model2Entity.Models;
    using AutoMapper;
    using AutoMapper.Extensions.ExpressionMapping;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// base adapater for mapping to IQueryable
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    /// <typeparam name="TConfig">configuration Type</typeparam>
    public abstract class BaseQueryableAdapter<TModel, TEntity, TConfig> : BaseAdapter<TModel, TEntity, TConfig>, ISearchableModelSet<TModel>
        where TEntity : class
        where TConfig : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryableAdapter{TModel, TEntity, TConfig}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="configuration">config options</param>
        /// <param name="logger">injected logger</param>
        public BaseQueryableAdapter(IMapper dataMapper, TConfig configuration, ILogger logger)
            : base(dataMapper, configuration, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TModel>> AllAsync(CancellationToken ct)
        {
            var pr = await this.OnGeneralizedSearch(null, null, ct)
               .ConfigureAwait(false);

            return pr.Items;
        }

        /// <inheritdoc/>
        public Task<IPageResult<TModel>> AllAsync(IPageRequest pageRequest, CancellationToken ct)
        {
            return this.OnGeneralizedSearch(null, pageRequest, ct);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TModel>> WhereAsync(Expression<Func<TModel, bool>> filter, CancellationToken ct)
        {
            var pr = await this.OnGeneralizedSearch(filter, null, ct)
                .ConfigureAwait(false);

            return pr.Items;
        }

        /// <inheritdoc/>
        public Task<IPageResult<TModel>> WhereAsync(Expression<Func<TModel, bool>> filter, IPageRequest pageRequest, CancellationToken ct)
        {
            return this.OnGeneralizedSearch(filter, pageRequest, ct);
        }

        /// <summary>
        /// Generalized Queryable search that supports filtering and paging
        /// </summary>
        /// <param name="filter">filter expression</param>
        /// <param name="pageRequest">page request</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search results</returns>
        protected async Task<IPageResult<TModel>> OnGeneralizedSearch(Expression<Func<TModel, bool>> filter, IPageRequest pageRequest, CancellationToken ct)
        {
            var result = new PageResult<TModel>
            {
                PageNumber = 1,
            };

            var query = this.OnQueryable(ct);

            // add filter expression to query
            if (filter != null)
            {
                var expression = this.DataMapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);
                query = query.Where(expression);
            }

            // add paging expression to query
            if (pageRequest != null)
            {
                result.PageNumber = pageRequest.PageNumber;

                query = query.Skip(pageRequest.SkipAmount())
                    .Take(pageRequest.TakeAmount());

                if (pageRequest.IncludeTotalCount)
                {
                    await this.CountEntityAsync(result, query, ct)
                        .ConfigureAwait(false);
                }
            }

            var entitySet = query.ToList();

            result.Items = this.OnConvert(entitySet);

            return result;
        }

        /// <summary>
        /// Stores the count for the queryable in the page result
        /// </summary>
        /// <param name="result">page result</param>
        /// <param name="query">query</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        protected async Task CountEntityAsync(PageResult<TModel> result, IQueryable<TEntity> query, CancellationToken ct)
        {
            var r = Arguments.EnsureNotNull(result, nameof(result));

            r.TotalRecordCount = (ulong)await query.LongCountAsync(ct)
                .ConfigureAwait(false);

            r.PageCount = (uint)(r.TotalRecordCount / r.PageSize);
        }

        /// <summary>
        /// Gets the base search query
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>iqueryable</returns>
        protected abstract IQueryable<TEntity> OnQueryable(CancellationToken ct);
    }
}