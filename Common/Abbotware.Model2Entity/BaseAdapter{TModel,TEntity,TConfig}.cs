// -----------------------------------------------------------------------
// <copyright file="BaseAdapter{TModel,TEntity,TConfig}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using AutoMapper;

    /// <summary>
    /// Base model to entity mapping adapter
    /// </summary>
    /// <typeparam name="TModel">Model Type</typeparam>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    /// <typeparam name="TConfig">configuration Type</typeparam>
    public abstract class BaseAdapter<TModel, TEntity, TConfig> : BaseComponent<TConfig>
        where TEntity : class
        where TConfig : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAdapter{TModel, TEntity, TConfig}"/> class.
        /// </summary>
        /// <param name="dataMapper">automapper profile</param>
        /// <param name="configuration">config options</param>
        /// <param name="logger">injected logger</param>
        protected BaseAdapter(IMapper dataMapper, TConfig configuration, ILogger logger)
            : base(configuration, logger)
        {
            this.DataMapper = dataMapper;
        }

        /// <summary>
        /// Gets the auto mapper
        /// </summary>
        protected IMapper DataMapper { get; }

        /// <summary>
        /// converts the entites to models
        /// </summary>
        /// <param name="entites">list of entites</param>
        /// <returns>list of models</returns>
        protected virtual IEnumerable<TModel> OnConvert(IEnumerable<TEntity> entites)
        {
            return entites.Select(this.DataMapper.Map<TEntity, TModel>).ToList();
        }

        /// <summary>
        /// converts an entity to a model
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>model</returns>
        protected virtual TModel OnConvert(TEntity entity)
        {
            if (entity == null)
            {
                return default;
            }

            return this.DataMapper.Map<TEntity, TModel>(entity);
        }
    }
}