// -----------------------------------------------------------------------
// <copyright file="ModelSetConfiguration{TEntity,TParameter}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Prototype.Configuration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configuration class for the async  adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TParameter">paramter type</typeparam>
    public class ModelSetConfiguration<TEntity, TParameter> : IModelSetConfiguration<TEntity, TParameter>
    {
        /// <inheritdoc/>
        public Func<TParameter, CancellationToken, Task<IEnumerable<TEntity>>> DataSource { get; set; }
    }
}
