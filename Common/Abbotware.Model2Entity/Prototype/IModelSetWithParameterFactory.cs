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
    public interface IModelSetWithParameterFactory : IModelSetFactory
    {
        /// <summary>
        /// Creates a model set
        /// </summary>
        /// <typeparam name="TModel">model type</typeparam>
        /// <typeparam name="TParameter">parameter type</typeparam>
        /// <returns>model set for querying</returns>
        IModelSet<TModel, TParameter> CreateWithParameter<TModel, TParameter>();
    }
}