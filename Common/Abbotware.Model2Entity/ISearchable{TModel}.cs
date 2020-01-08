// -----------------------------------------------------------------------
// <copyright file="ISearchable{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for querying a model set
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public interface ISearchable<TModel>
    {
        /// <summary>
        /// Gets all models
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - list of all models</returns>
        Task<IEnumerable<TModel>> AllAsync(CancellationToken ct);

        /// <summary>
        /// Finds models based on the supplied filter
        /// </summary>
        /// <param name="filter">filter expression</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - list of models matching filter</returns>
        Task<IEnumerable<TModel>> WhereAsync(Expression<Func<TModel, bool>> filter, CancellationToken ct);
    }
}
