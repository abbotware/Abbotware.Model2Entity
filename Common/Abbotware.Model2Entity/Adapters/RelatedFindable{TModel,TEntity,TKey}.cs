// -----------------------------------------------------------------------
// <copyright file="RelatedFindable{TModel,TEntity,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Model2Entity.Configuration;
    using AutoMapper;

    /// <summary>
    /// Maps an Entity to a Model with limited query capabilties
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    /// <typeparam name="TKey">key Type</typeparam>
    public class RelatedFindable<TModel, TEntity, TKey> : BaseAdapter<TModel, TEntity, IRelatedFindableConfiguration<TEntity, TKey>>, IRelatedFindable<TModel, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedFindable{TModel, TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="configuration">config options</param>
        /// <param name="logger">injected logger</param>
        public RelatedFindable(IMapper dataMapper, IRelatedFindableConfiguration<TEntity, TKey> configuration, ILogger logger)
            : base(dataMapper, configuration, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TModel>> FindAsync(TKey key, CancellationToken ct)
        {
            var s = await this.Configuration.FindAsync(key, ct)
                .ConfigureAwait(false);

            return this.OnConvert(s);
        }
    }
}