// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    ///     Supports queryable Include/ThenInclude chaining operators.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-providers">Implementation of database providers and extensions</see>
    ///     and <see href="https://aka.ms/efcore-how-queries-work">How EF Core queries work</see> for more information.
    /// </remarks>
    /// <typeparam name="TEntity"> The entity type. </typeparam>
    /// <typeparam name="TProperty"> The property type. </typeparam>
    // ReSharper disable once UnusedTypeParameter
    public interface IIncludableQueryable<out TEntity, out TProperty> : IQueryable<TEntity>
    {
    }
}
