// -----------------------------------------------------------------------
// <copyright file="DbSetAdapter{TModel,TEntity,TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Adapters
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Entity;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Model2Entity.Configuration;
    using Abbotware.Model2Entity.Configuration.Models;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    //// https://github.com/AutoMapper/AutoMapper.Extensions.ExpressionMapping

    /// <summary>
    /// Maps an Entity to a Model with limited query capabilties
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    /// <typeparam name="TContext">Context Type</typeparam>
    public class DbSetAdapter<TModel, TEntity, TContext> : BaseQueryableAdapter<TModel, TEntity, IDbSetConfiguration<TEntity>>, IDbSetFindable<TModel>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly Lazy<DbContext> context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSetAdapter{TEntity, TContext, TModel}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="factory">context factory</param>
        /// <param name="logger">injected logger</param>
        public DbSetAdapter(IMapper dataMapper, IDbContextFactory factory, ILogger logger)
            : this(dataMapper, factory, new DbSetConfiguration<TEntity>(), logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSetAdapter{TEntity, TContext, TModel}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="factory">context factory</param>
        /// <param name="configuration">config options</param>
        /// <param name="logger">injected logger</param>
        public DbSetAdapter(IMapper dataMapper, IDbContextFactory factory, IDbSetConfiguration<TEntity> configuration, ILogger logger)
            : base(dataMapper, configuration, logger)
        {
            var f = Arguments.EnsureNotNull(factory, nameof(factory));

            this.context = new Lazy<DbContext>(f.Create<TContext>, LazyThreadSafetyMode.PublicationOnly);
        }

        /// <inheritdoc/>
        public Task<TModel> SingleOrDefaultAsync(IPrimaryKey key, CancellationToken ct)
        {
            var k = Arguments.EnsureNotNull(key, nameof(key));

            return this.SingleOrDefaultAsync(k.ToEntityFindKeyValues(), ct);
        }

        /// <inheritdoc/>
        public Task<TModel> SingleOrDefaultAsync(object key, CancellationToken ct)
        {
            var k = Arguments.EnsureNotNull(key, nameof(key));

            return this.SingleOrDefaultAsync(new object[] { k }, ct);
        }

        /// <inheritdoc/>
        public async Task<TModel> SingleOrDefaultAsync(object[] keyValues, CancellationToken ct)
        {
            var findExpression = EntityFrameworkHelper.FindExpression<TEntity>(keyValues);

            var item = await this.OnBaseEntityFindQuery(ct)
                .SingleOrDefaultAsync(findExpression, ct)
                .ConfigureAwait(false);

            return this.OnConvert(item);
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            if (this.context.IsValueCreated)
            {
                this.context.Value.Dispose();
            }
        }

        /// <summary>
        /// Gets the base search query
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>iqueryable</returns>
        protected override IQueryable<TEntity> OnQueryable(CancellationToken ct)
        {
            var query = this.context.Value.Set<TEntity>()
                .AsNoTracking();

            if (this.Configuration.PreQuery != null)
            {
                query = this.Configuration.PreQuery(query);
            }

            return query;
        }

        /// <summary>
        /// Gets the base find query
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>dbset</returns>
        protected virtual IQueryable<TEntity> OnBaseEntityFindQuery(CancellationToken ct)
        {
            return this.OnQueryable(ct);
        }
    }
}