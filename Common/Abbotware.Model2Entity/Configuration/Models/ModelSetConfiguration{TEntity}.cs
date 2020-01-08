// -----------------------------------------------------------------------
// <copyright file="ModelSetConfiguration{TEntity}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Configuration.Models
{
    using System;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Configuration options for the modelset query adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public class ModelSetConfiguration<TEntity> : IModelSetConfiguration<TEntity>
    {
        /// <inheritdoc/>
        public Func<CancellationToken, IQueryable<TEntity>> Queryable { get; set; }
    }
}
