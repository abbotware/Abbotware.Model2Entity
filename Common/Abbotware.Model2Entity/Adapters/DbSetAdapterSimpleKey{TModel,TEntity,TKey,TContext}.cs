// -----------------------------------------------------------------------
// <copyright file="DbSetAdapterSimpleKey{TModel,TEntity,TKey,TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Adapters
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Model2Entity.Configuration;
    using Abbotware.Model2Entity.Configuration.Models;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Maps an Entity to a Model with limited query capabilties using a key type
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    /// <typeparam name="TKey">Key Type</typeparam>
    /// <typeparam name="TContext">Context Type</typeparam>
    public class DbSetAdapterSimpleKey<TModel, TEntity, TKey, TContext> : DbSetAdapter<TModel, TEntity, TContext>, ISearchableModelSet<TModel, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbSetAdapterSimpleKey{TEntity, TContext, TKey, TModel}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="factory">context factory</param>
        /// <param name="logger">injected logger</param>
        public DbSetAdapterSimpleKey(IMapper dataMapper, IDbContextFactory factory, ILogger logger)
            : base(dataMapper, factory, new DbSetConfiguration<TEntity>(), logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSetAdapterSimpleKey{TEntity, TContext, TKey, TModel}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="factory">context factory</param>
        /// <param name="configuration">config options</param>
        /// <param name="logger">injected logger</param>
        public DbSetAdapterSimpleKey(IMapper dataMapper, IDbContextFactory factory, IDbSetConfiguration<TEntity> configuration, ILogger logger)
            : base(dataMapper, factory, configuration, logger)
        {
        }

        /// <inheritdoc/>
        public Task<TModel> SingleOrDefaultAsync(TKey key, CancellationToken ct)
        {
            return this.SingleOrDefaultAsync((object)key, ct);
        }
    }
}