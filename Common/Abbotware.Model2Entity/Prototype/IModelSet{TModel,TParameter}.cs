// -----------------------------------------------------------------------
// <copyright file="IModelSet{TModel,TMeta}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Prototype
{
    using System;

    /// <summary>
    /// Interface for querying a model set
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    /// <typeparam name="TParameter">Metadat type</typeparam>
    public interface IModelSet<TModel, TParameter> : ISearchableWithParameter<TModel, TParameter>, IDisposable
    {
    }
}
