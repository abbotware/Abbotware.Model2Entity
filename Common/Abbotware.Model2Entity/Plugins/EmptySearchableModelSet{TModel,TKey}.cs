// -----------------------------------------------------------------------
// <copyright file="EmptySearchableModelSet{TModel,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Plugins
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Empty Model Set with a key
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public class EmptySearchableModelSet<TModel, TKey> : EmptySearchableModelSet<TModel>, ISearchableModelSet<TModel, TKey>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <inheritdoc/>
        public Task<TModel> SingleOrDefaultAsync(TKey keyValue, CancellationToken ct)
        {
            return Task.FromResult(default(TModel));
        }
    }
}