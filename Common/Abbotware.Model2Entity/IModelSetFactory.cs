// -----------------------------------------------------------------------
// <copyright file="IModelSetFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System;

    /// <summary>
    /// Factory for creating model sets dynamically
    /// </summary>
    public interface IModelSetFactory
    {
        /// <summary>
        /// Creates a readonly model set
        /// </summary>
        /// <typeparam name="TModel">model type</typeparam>
        /// <returns>model set for querying</returns>
        ISearchableModelSet<TModel> CreateReadOnly<TModel>();

        /// <summary>
        /// Creates a model set
        /// </summary>
        /// <typeparam name="TModel">model type</typeparam>
        /// <returns>model set for querying</returns>
        IModelSet<TModel> Create<TModel>();

        /// <summary>
        /// Creates a readonly model set with a strongly typed key
        /// </summary>
        /// <typeparam name="TModel">model type</typeparam>
        /// <typeparam name="TKey">key type</typeparam>
        /// <returns>model set for querying</returns>
        ISearchableModelSet<TModel, TKey> CreateReadOnly<TModel, TKey>()
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

        /// <summary>
        /// Creates a findable model set with a strongly typed key
        /// </summary>
        /// <typeparam name="TModel">model type</typeparam>
        /// <typeparam name="TKey">key type</typeparam>
        /// <returns>model set for querying</returns>
        IFindable<TModel, TKey> CreateFindable<TModel, TKey>()
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

        /// <summary>
        /// Creates a related findable model set using a foreign key
        /// </summary>
        /// <typeparam name="TModel">model type</typeparam>
        /// <typeparam name="TParentKey">parent key type</typeparam>
        /// <returns>model set for querying</returns>
        IRelatedFindable<TModel, TParentKey> CreateRelatedFindable<TModel, TParentKey>()
            where TParentKey : IEquatable<TParentKey>, IComparable<TParentKey>, IComparable;
    }
}