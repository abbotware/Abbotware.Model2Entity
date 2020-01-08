// -----------------------------------------------------------------------
// <copyright file="Findable{TModel,TEntity,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Adapters
{
    using System;
    using System.Linq;
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
    public class Findable<TModel, TEntity, TKey> : BaseAdapter<TModel, TEntity, IFindableConfiguration<TEntity, TKey>>, IFindable<TModel, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Findable{TModel, TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="configuration">config options</param>
        /// <param name="logger">injected logger</param>
        public Findable(IMapper dataMapper, IFindableConfiguration<TEntity, TKey> configuration, ILogger logger)
            : base(dataMapper, configuration, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<TModel> SingleOrDefaultAsync(TKey key, CancellationToken ct)
        {
            var s = await this.Configuration.SingleOrDefaultAsync(key, ct)
                .ConfigureAwait(false);

            return this.OnConvert(s);
        }
    }
}