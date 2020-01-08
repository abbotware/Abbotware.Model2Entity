// -----------------------------------------------------------------------
// <copyright file="IFindable{TModel,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// interface for finding via a key
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public interface IFindable<TModel, TKey> : IDisposable
    {
        /// <summary>
        /// Gets a single entity based on its key
        /// </summary>
        /// <param name="key">primayr key of the  model to get</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - entity if it was found</returns>
        Task<TModel> SingleOrDefaultAsync(TKey key, CancellationToken ct);
    }
}
