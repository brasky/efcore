// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Microsoft.EntityFrameworkCore.Diagnostics
{
    /// <summary>
    ///     The <see cref="DiagnosticSource" /> event payload base class for
    ///     <see cref="RelationalEventId" /> transaction events.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-diagnostics">Logging, events, and diagnostics</see> for more information.
    /// </remarks>
    public class TransactionStartingEventData : DbContextEventData
    {
        /// <summary>
        ///     Constructs the event payload.
        /// </summary>
        /// <param name="eventDefinition"> The event definition. </param>
        /// <param name="messageGenerator"> A delegate that generates a log message for this event. </param>
        /// <param name="context"> The <see cref="DbContext" /> currently in use, or <see langword="null" /> if not known. </param>
        /// <param name="isolationLevel"> The transaction isolation level. </param>
        /// <param name="transactionId"> A correlation ID that identifies the Entity Framework transaction being used. </param>
        /// <param name="connectionId"> A correlation ID that identifies the <see cref="DbConnection" /> instance being used. </param>
        /// <param name="async"> Indicates whether or not the transaction is being used asynchronously. </param>
        /// <param name="startTime"> The start time of this event. </param>
        public TransactionStartingEventData(
            EventDefinitionBase eventDefinition,
            Func<EventDefinitionBase, EventData, string> messageGenerator,
            DbContext? context,
            IsolationLevel isolationLevel,
            Guid transactionId,
            Guid connectionId,
            bool async,
            DateTimeOffset startTime)
            : base(eventDefinition, messageGenerator, context)
        {
            IsolationLevel = isolationLevel;
            TransactionId = transactionId;
            ConnectionId = connectionId;
            IsAsync = async;
            StartTime = startTime;
        }

        /// <summary>
        ///     The transaction isolation level.
        /// </summary>
        public virtual IsolationLevel IsolationLevel { get; }

        /// <summary>
        ///     A correlation ID that identifies the Entity Framework transaction being used.
        /// </summary>
        public virtual Guid TransactionId { get; }

        /// <summary>
        ///     A correlation ID that identifies the <see cref="DbConnection" /> instance being used.
        /// </summary>
        public virtual Guid ConnectionId { get; }

        /// <summary>
        ///     Indicates whether or not the transaction is being used asynchronously.
        /// </summary>
        public virtual bool IsAsync { get; }

        /// <summary>
        ///     The start time of this event.
        /// </summary>
        public virtual DateTimeOffset StartTime { get; }
    }
}
