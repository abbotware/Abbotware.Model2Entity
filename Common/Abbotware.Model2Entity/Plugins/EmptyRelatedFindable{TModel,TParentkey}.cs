// -----------------------------------------------------------------------
// <copyright file="EmptyRelatedFindable{TModel,TParentkey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Plugins
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Empty Findable Set
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <typeparam name="TParentkey">parent key type</typeparam>
    public sealed class EmptyRelatedFindable<TModel, TParentkey> : IRelatedFindable<TModel, TParentkey>
    {
        /// <inheritdoc/>
        public void Dispose()
        {
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TModel>> FindAsync(TParentkey key, CancellationToken ct)
        {
            return Task.FromResult(Enumerable.Empty<TModel>());
        }
    }
}