// -----------------------------------------------------------------------
// <copyright file="IModelSetConfiguration{TEntity,TParameter}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Prototype.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configuration options for the ModelSet adapter
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TParameter">paramter type</typeparam>
    public interface IModelSetConfiguration<TEntity, TParameter>
    {
        /// <summary>
        /// Gets the include query
        /// </summary>
        Func<TParameter, CancellationToken, Task<IEnumerable<TEntity>>> DataSource { get; }
    }
}