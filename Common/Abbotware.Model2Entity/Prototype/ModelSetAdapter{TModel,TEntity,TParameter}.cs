// -----------------------------------------------------------------------
// <copyright file="ModelSetAdapter{TModel,TEntity,TParameter}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Prototype
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Model2Entity.Configuration;
    using AutoMapper;
    using AutoMapper.Extensions.ExpressionMapping;

    /// <summary>
    /// Maps an Entity to a Model with limited query capabilties
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    /// <typeparam name="TParameter">parameter type</typeparam>
    public class ModelSetAdapter<TModel, TEntity, TParameter> : BaseQueryableAdapter<TModel, TEntity, IModelSetConfiguration<TEntity,TParameter>>
        where TEntity : class
    {
        private readonly IMapper dataMapper;

        private readonly IModelSetConfiguration<TEntity, TParameter> configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSetAdapter{TEntity, TModel,TMeta}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="configuration">model set configuration</param>
        public ModelSetAdapter(IMapper dataMapper, IModelSetConfiguration<TEntity, TParameter> configuration)
        {
            this.dataMapper = dataMapper;
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TModel>> AllAsync(TParameter meta, CancellationToken ct)
        {
            var dbSet = await this.OnBaseSearchQuery(meta, ct)
                .ConfigureAwait(false);

            var modelSet = dbSet.Select(this.dataMapper.Map<TEntity, TModel>)
                .ToList();

            return modelSet;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TModel>> WhereAsync(TParameter meta, Expression<Func<TModel, bool>> filter, CancellationToken ct)
        {
            var expression = this.dataMapper.MapExpression<Expression<Func<TEntity, bool>>>(filter)
                .Compile();

            var dbSet = await this.OnBaseSearchQuery(meta, ct)
                .ConfigureAwait(false);

            var modelSet = dbSet
                .Where(expression)
                .Select(this.dataMapper.Map<TEntity, TModel>)
                .ToList();

            return modelSet;
        }

        /// <summary>
        /// Gets the base search query
        /// </summary>
        /// <param name="meta">meta data</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>iqueryable</returns>
        protected virtual Task<IEnumerable<TEntity>> OnBaseSearchQuery(TParameter meta, CancellationToken ct)
        {
            return this.configuration.DataSource(meta, ct);
        }
    }
}