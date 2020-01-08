// -----------------------------------------------------------------------
// <copyright file="ISearchableModelSet{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;

    /// <summary>
    /// Interface for querying a model set
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public interface ISearchableModelSet<TModel> : ISearchable<TModel>, IPagedSearchable<TModel>, IDisposable
    {
    }
}
