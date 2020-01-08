// -----------------------------------------------------------------------
// <copyright file="ModelSetAdapter{TModel,TEntity}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Adapters
{
    using System.Linq;
    using System.Threading;
    using Abbotware.Core.Logging;
    using Abbotware.Model2Entity.Configuration;
    using AutoMapper;

    /// <summary>
    /// Maps an Entity to a Model with limited query capabilties
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    public class ModelSetAdapter<TModel, TEntity> : BaseQueryableAdapter<TModel, TEntity, IModelSetConfiguration<TEntity>>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelSetAdapter{TModel, TEntity}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="configuration">config options</param>
        /// <param name="logger">injected logger</param>
        public ModelSetAdapter(IMapper dataMapper, IModelSetConfiguration<TEntity> configuration, ILogger logger)
            : base(dataMapper, configuration, logger)
        {
        }

        /// <inheritdoc/>
        protected override IQueryable<TEntity> OnQueryable(CancellationToken ct)
        {
            return this.Configuration.Queryable(ct);
        }
    }
}