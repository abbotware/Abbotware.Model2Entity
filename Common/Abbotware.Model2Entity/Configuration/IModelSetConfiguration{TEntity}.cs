// -----------------------------------------------------------------------
// <copyright file="IModelSetConfiguration{TEntity}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration
{
    using System;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Configuration options for the ModelSet adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IModelSetConfiguration<TEntity>
    {
        /// <summary>
        /// Gets the queryable for data searches
        /// </summary>
        Func<CancellationToken, IQueryable<TEntity>> Queryable { get; }
    }
}