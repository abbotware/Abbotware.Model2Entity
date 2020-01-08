// -----------------------------------------------------------------------
// <copyright file="PageRequestWithoutCount.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Models
{
    /// <summary>
    /// POCO model implementation for IPageRequest with total count set to false
    /// </summary>
    public class PageRequestWithoutCount : IPageRequest
    {
        /// <inheritdoc/>
        public virtual bool IncludeTotalCount { get; set; } = false;

        /// <inheritdoc/>
        public virtual uint PageNumber { get; set; }

        /// <inheritdoc/>
        public virtual uint PageSize { get; set; }
    }
}
