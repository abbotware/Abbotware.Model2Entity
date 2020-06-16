// -----------------------------------------------------------------------
// <copyright file="DbSetConfiguration{TEntity}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration.Models
{
    using System;
    using System.Linq;

    /// <summary>
    /// Configuration options for the DbSet query adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public class DbSetConfiguration<TEntity> : IDbSetConfiguration<TEntity>
    {
        /// <inheritdoc/>
        public Func<IQueryable<TEntity>, IQueryable<TEntity>> PreQuery { get; set; }
    }
}
