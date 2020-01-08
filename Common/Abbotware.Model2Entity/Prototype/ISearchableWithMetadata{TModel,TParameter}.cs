// -----------------------------------------------------------------------
// <copyright file="ISearchableWithMetadata{TModel,TParameter}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Prototype
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
    /// <typeparam name="TParameter">parameter type</typeparam>
    public interface ISearchableWithParameter<TModel, TParameter>
    {
        /// <summary>
        /// Gets all models
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - list of all models</returns>
        Task<IEnumerable<TModel>> AllAsync(TParameter parameter, CancellationToken ct);

        /// <summary>
        /// Finds models based on the supplied filter
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <param name="filter">filter expression</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - list of models matching filter</returns>
        Task<IEnumerable<TModel>> WhereAsync(TParameter parameter, Expression<Func<TModel, bool>> filter, CancellationToken ct);
    }
}
