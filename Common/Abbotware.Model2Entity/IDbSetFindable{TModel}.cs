// -----------------------------------------------------------------------
// <copyright file="IDbSetFindable{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using Abbotware.Core.Entity;

    /// <summary>
    /// Interface for performing finds via DbSet
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    public interface IDbSetFindable<TModel> : IFindable<TModel, object>, IFindable<TModel, object[]>, IFindable<TModel, IPrimaryKey>
    {
    }
}
