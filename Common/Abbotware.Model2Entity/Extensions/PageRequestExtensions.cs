// -----------------------------------------------------------------------
// <copyright file="PageRequestExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Model2Entity.Extensions
{
    using Abbotware.Core;

    /// <summary>
    /// Extension method for IPageRequest
    /// </summary>
    public static class PageRequestExtensions
    {
        /// <summary>
        /// Computes the Linq 'Skip' amount
        /// </summary>
        /// <param name="pageRequest">extended</param>
        /// <returns>skip amount</returns>
        public static int SkipAmount(this IPageRequest pageRequest)
        {
            Arguments.NotNull(pageRequest, nameof(pageRequest));

#pragma warning disable CA1062 // Validate arguments of public methods
            return (int)(pageRequest.PageNumber * pageRequest.PageSize);
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        /// <summary>
        /// Computes the Linq 'Take' amount
        /// </summary>
        /// <param name="pageRequest">extended</param>
        /// <returns>skip amount</returns>
        public static int TakeAmount(this IPageRequest pageRequest)
        {
            Arguments.NotNull(pageRequest, nameof(pageRequest));

#pragma warning disable CA1062 // Validate arguments of public methods
            return (int)pageRequest.PageSize;
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}