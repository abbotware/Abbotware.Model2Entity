// -----------------------------------------------------------------------
// <copyright file="PageResult{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// POCO model implementation for IPageResult
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    public class PageResult<TModel> : IPageResult<TModel>
    {
        /// <inheritdoc/>
        public virtual uint PageNumber { get; set; }

        /// <inheritdoc/>
        public virtual uint PageSize { get; set; }

        /// <inheritdoc/>
        public virtual uint? PageCount { get; set; }

        /// <inheritdoc/>
        public virtual ulong? TotalRecordCount { get; set; }

        /// <inheritdoc/>
        public virtual IEnumerable<TModel> Items { get; set; }
    }
}
