// -----------------------------------------------------------------------
// <copyright file="FindableConfiguration{TEntity,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration.Models
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configuration options for the findable adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public class FindableConfiguration<TEntity, TKey> : IFindableConfiguration<TEntity, TKey>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <inheritdoc/>
        public Func<TKey, CancellationToken, Task<TEntity>> SingleOrDefaultAsync { get; set; }
    }
}
