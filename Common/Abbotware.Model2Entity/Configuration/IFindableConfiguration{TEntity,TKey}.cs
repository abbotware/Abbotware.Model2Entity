// -----------------------------------------------------------------------
// <copyright file="IFindableConfiguration{TEntity,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configuration options for the Findable adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public interface IFindableConfiguration<TEntity, TKey>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <summary>
        /// gets a function to retrieve a single entity
        /// </summary>
        Func<TKey, CancellationToken, Task<TEntity>> SingleOrDefaultAsync { get; }
    }
}