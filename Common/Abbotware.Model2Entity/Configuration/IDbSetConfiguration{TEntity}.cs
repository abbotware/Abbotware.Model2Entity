// -----------------------------------------------------------------------
// <copyright file="IDbSetConfiguration{TEntity}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration
{
    using System;
    using System.Linq;

    /// <summary>
    /// Configuration interface for the DbSet query adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IDbSetConfiguration<TEntity>
    {
        /// <summary>
        /// Gets the include query
        /// </summary>
        Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludesQuery { get; }
    }
}