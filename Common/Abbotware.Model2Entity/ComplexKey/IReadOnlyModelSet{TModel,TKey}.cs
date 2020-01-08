// -----------------------------------------------------------------------
// <copyright file="IReadOnlyModelSet{TModel,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.ComplexKey
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Entity;

    /// <summary>
    /// Interface for querying a model with a complex key type
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public interface IReadOnlyModelSet<TModel, TKey> : ISearchable<TModel>, IPagedSearchable<TModel>, IDisposable
        where TKey : IPrimaryKey
    {
        /// <summary>
        /// Gets a single entity based on its key
        /// </summary>
        /// <param name="keyValue">key of the  model to get</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - entity if it was found</returns>
        Task<TModel> SingleOrDefault(TKey keyValue, CancellationToken ct);
    }
}
