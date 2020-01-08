// -----------------------------------------------------------------------
// <copyright file="IRelatedFindableConfiguration{TEntity,TParentKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configuration options for the related findable adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TParentKey">parent key type</typeparam>
    public interface IRelatedFindableConfiguration<TEntity, TParentKey>
        where TParentKey : IEquatable<TParentKey>, IComparable<TParentKey>, IComparable
    {
        /// <summary>
        /// gets a function to retrieve entites with a common parent
        /// </summary>
        Func<TParentKey, CancellationToken, Task<IEnumerable<TEntity>>> FindAsync { get; }
    }
}