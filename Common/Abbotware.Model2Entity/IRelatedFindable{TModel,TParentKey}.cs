// -----------------------------------------------------------------------
// <copyright file="IRelatedFindable{TModel,TParentKey}.cs" company="Abbotware, LLC">
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
    /// interface for finding realted data via its parent key
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <typeparam name="TParentKey">parent key type</typeparam>
    public interface IRelatedFindable<TModel, TParentKey> : IDisposable
    {
        /// <summary>
        /// Gets entities based on its parent
        /// </summary>
        /// <param name="key">primayr key of the  model to get</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task - entity if it was found</returns>
        Task<IEnumerable<TModel>> FindAsync(TParentKey key, CancellationToken ct);
    }
}
