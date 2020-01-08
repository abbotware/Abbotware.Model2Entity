// -----------------------------------------------------------------------
// <copyright file="EmptyModelSetFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Plugins
{
    using System;

    /// <summary>
    /// Empty Model Set Factory (for swagger docs)
    /// </summary>
    public class EmptyModelSetFactory : IModelSetFactory
    {
        /// <inheritdoc/>
        public ISearchableModelSet<TModel> CreateReadOnly<TModel>()
        {
            return new EmptySearchableModelSet<TModel>();
        }

        /// <inheritdoc/>
        public ISearchableModelSet<TModel, TKey> CreateReadOnly<TModel, TKey>()
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return new EmptySearchableModelSet<TModel, TKey>();
        }

        /// <inheritdoc/>
        public IModelSet<TModel> Create<TModel>()
        {
            return new EmptyModelSet<TModel>();
        }

        /// <inheritdoc/>
        public IFindable<TModel, TKey> CreateFindable<TModel, TKey>()
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return new EmptyFindable<TModel, TKey>();
        }

        /// <inheritdoc/>
        public IRelatedFindable<TModel, TParentKey> CreateRelatedFindable<TModel, TParentKey>()
            where TParentKey : IEquatable<TParentKey>, IComparable<TParentKey>, IComparable
        {
            return new EmptyRelatedFindable<TModel, TParentKey>();
        }
    }
}