// -----------------------------------------------------------------------
// <copyright file="IPagedSearchable{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for querying a model set
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public interface IPagedSearchable<TModel>
    {
        /// <summary>
        /// Gets all models via paginging
        /// </summary>
        /// <param name="pageRequest">page request</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - paged list of all models</returns>
        Task<IPageResult<TModel>> AllAsync(IPageRequest pageRequest, CancellationToken ct);

        /// <summary>
        /// Finds models based on the supplied filter via paging
        /// </summary>
        /// <param name="filter">filter expression</param>
        /// <param name="pageRequest">page request</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - paged list of models matching filter</returns>
        Task<IPageResult<TModel>> WhereAsync(Expression<Func<TModel, bool>> filter, IPageRequest pageRequest, CancellationToken ct);
    }
}
