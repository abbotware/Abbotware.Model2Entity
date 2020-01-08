// -----------------------------------------------------------------------
// <copyright file="EmptyModelSet{TModel,TMeta}.cs" company="Abbotware, LLC">
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

    /// <summary>
    /// Empty Model Set
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <typeparam name="TMeta">meta type</typeparam>
    public class EmptyModelSet<TModel, TMeta> : BaseComponent, IModelSet<TModel, TMeta>
    {
        /// <inheritdoc/>
        public Task<IEnumerable<TModel>> AllAsync(TMeta meta, CancellationToken ct)
        {
            return Task.FromResult(Enumerable.Empty<TModel>());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TModel>> WhereAsync(TMeta meta, Expression<Func<TModel, bool>> filter, CancellationToken ct)
        {
            return Task.FromResult(Enumerable.Empty<TModel>());
        }
    }
}