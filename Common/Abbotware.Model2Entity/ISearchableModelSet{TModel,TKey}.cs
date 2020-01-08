// -----------------------------------------------------------------------
// <copyright file="ISearchableModelSet{TModel,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;

    /// <summary>
    /// Interface for querying a model with a strong key
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public interface ISearchableModelSet<TModel, TKey> : ISearchableModelSet<TModel>, IFindable<TModel, TKey>, IDisposable
           where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
    }
}
