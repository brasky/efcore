// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    ///     <para>
    ///         A factory for creating <see cref="RelationalTransaction" /> instances.
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers It is generally not used in application code.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Singleton" />. This means a single instance
    ///         is used by many <see cref="DbContext" /> instances. The implementation must be thread-safe.
    ///         This service cannot depend on services registered as <see cref="ServiceLifetime.Scoped" />.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-providers">Implementation of database providers and extensions</see>
    ///     for more information.
    /// </remarks>
    public class RelationalTransactionFactory : IRelationalTransactionFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RelationalTransactionFactory" /> class.
        /// </summary>
        /// <param name="dependencies"> Parameter object containing dependencies for this service. </param>
        public RelationalTransactionFactory(RelationalTransactionFactoryDependencies dependencies)
        {
            Check.NotNull(dependencies, nameof(dependencies));

            Dependencies = dependencies;
        }

        /// <summary>
        ///     Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalTransactionFactoryDependencies Dependencies { get; }

        /// <summary>
        ///     Creates a <see cref="RelationalTransaction" /> instance.
        /// </summary>
        /// <param name="connection"> The connection to the database. </param>
        /// <param name="transaction"> The underlying <see cref="DbTransaction" />. </param>
        /// <param name="transactionId"> The unique correlation ID for this transaction. </param>
        /// <param name="logger"> The logger to write to. </param>
        /// <param name="transactionOwned">
        ///     A value indicating whether the transaction is owned by this class (i.e. if it can be disposed when this class is disposed).
        /// </param>
        /// <returns> A new <see cref="RelationalTransaction" /> instance. </returns>
        public virtual RelationalTransaction Create(
            IRelationalConnection connection,
            DbTransaction transaction,
            Guid transactionId,
            IDiagnosticsLogger<DbLoggerCategory.Database.Transaction> logger,
            bool transactionOwned)
            => new(connection, transaction, transactionId, logger, transactionOwned, Dependencies.SqlGenerationHelper);
    }
}
