// -----------------------------------------------------------------------
// <copyright file="IPageResult{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity
{
    using System.Collections.Generic;

    /// <summary>
    /// interface for page result
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    public interface IPageResult<TModel>
    {
        /// <summary>
        /// Gets the page number
        /// </summary>
        uint PageNumber { get;  }

        /// <summary>
        /// Gets the page size
        /// </summary>
        uint PageSize { get;  }

        /// <summary>
        /// Gets the page count
        /// </summary>
        /// <remarks>will be null if the request did not include total count = true</remarks>
        uint? PageCount { get;  }

        /// <summary>
        /// Gets the total record count
        /// </summary>
        /// <remarks>will be null if the request did not include total count = true</remarks>
        ulong? TotalRecordCount { get;  }

        /// <summary>
        /// Gets the list of items associated with this page request
        /// </summary>
        IEnumerable<TModel> Items { get;  }
    }
}