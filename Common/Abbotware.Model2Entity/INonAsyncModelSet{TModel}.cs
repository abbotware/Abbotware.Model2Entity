// -----------------------------------------------------------------------
// <copyright file="INonAsyncModelSet{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface for querying a model set
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public interface INonAsyncModelSet<TModel> : IDisposable
    {
        /// <summary>
        /// Gets all models
        /// </summary>
        /// <returns>async task - list of all models</returns>
        IEnumerable<TModel> All();

        /// <summary>
        /// Finds models based on the supplied filter
        /// </summary>
        /// <param name="filter">filter expression</param>
        /// <returns>async task - list of models matching filter</returns>
        IEnumerable<TModel> Where(Expression<Func<TModel, bool>> filter);
    }
}
