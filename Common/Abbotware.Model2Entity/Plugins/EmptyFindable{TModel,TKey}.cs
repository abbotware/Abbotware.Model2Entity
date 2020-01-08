// -----------------------------------------------------------------------
// <copyright file="EmptyFindable{TModel,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Plugins
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Empty Findable Set
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public sealed class EmptyFindable<TModel, TKey> : IFindable<TModel, TKey>
    {
        /// <inheritdoc/>
        public void Dispose()
        {
        }

        /// <inheritdoc/>
        public Task<TModel> SingleOrDefaultAsync(TKey key, CancellationToken ct)
        {
            return Task.FromResult<TModel>(default);
        }
    }
}