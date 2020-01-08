// -----------------------------------------------------------------------
// <copyright file="IPageRequest.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    /// <summary>
    /// Interface for a page request
    /// </summary>
    public interface IPageRequest
    {
        /// <summary>
        /// Gets a value indicating whether or not to include the total count
        /// </summary>
        /// <remarks>asking for the total count requires an extra database call</remarks>
        bool IncludeTotalCount { get;  }

        /// <summary>
        /// Gets the page number
        /// </summary>
        uint PageNumber { get;  }

        /// <summary>
        /// Gets the page size
        /// </summary>
        uint PageSize { get; }
    }
}