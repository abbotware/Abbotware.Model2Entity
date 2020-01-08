// -----------------------------------------------------------------------
// <copyright file="EmptySearchableModelSet{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Objects;
    using Abbotware.Model2Entity.Models;

    /// <summary>
    /// Empty Model Set
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    public class EmptySearchableModelSet<TModel> : BaseComponent, ISearchableModelSet<TModel>
    {
        /// <inheritdoc/>
        public Task<IEnumerable<TModel>> AllAsync(CancellationToken ct)
        {
            return Task.FromResult(Enumerable.Empty<TModel>());
        }

        /// <inheritdoc/>
        public Task<IPageResult<TModel>> AllAsync(IPageRequest pageRequest, CancellationToken ct)
        {
            var result = new PageResult<TModel>
            {
                Items = Enumerable.Empty<TModel>(),
            };

            return Task.FromResult<IPageResult<TModel>>(result);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TModel>> WhereAsync(Expression<Func<TModel, bool>> filter, CancellationToken ct)
        {
            return Task.FromResult(Enumerable.Empty<TModel>());
        }

        /// <inheritdoc/>
        public Task<IPageResult<TModel>> WhereAsync(Expression<Func<TModel, bool>> filter, IPageRequest pageRequest, CancellationToken ct)
        {
            var result = new PageResult<TModel>
            {
                Items = Enumerable.Empty<TModel>(),
            };

            return Task.FromResult<IPageResult<TModel>>(result);
        }
    }
}