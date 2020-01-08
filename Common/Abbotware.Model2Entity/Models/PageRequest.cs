// -----------------------------------------------------------------------
// <copyright file="PageRequest.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Models
{
    /// <summary>
    /// POCO model implementation for IPageRequest (includes total count)
    /// </summary>
    public class PageRequest : IPageRequest
    {
        /// <inheritdoc/>
        public virtual bool IncludeTotalCount { get; set; } = true;

        /// <inheritdoc/>
        public virtual uint PageNumber { get; set; }

        /// <inheritdoc/>
        public virtual uint PageSize { get; set; }
    }
}
