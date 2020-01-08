// -----------------------------------------------------------------------
// <copyright file="RelatedFindableConfiguration{TEntity,TParentKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configuration options a RelatedFindable Adapter using the parent key
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TParentKey">parent key type</typeparam>
    public class RelatedFindableConfiguration<TEntity, TParentKey> : IRelatedFindableConfiguration<TEntity, TParentKey>
        where TParentKey : IEquatable<TParentKey>, IComparable<TParentKey>, IComparable
    {
        /// <inheritdoc/>
        public Func<TParentKey, CancellationToken, Task<IEnumerable<TEntity>>> FindAsync { get; set; }
    }
}
