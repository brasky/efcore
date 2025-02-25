// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions
{
    /// <summary>
    ///     A convention which ensures that all sequences in the model have unique names
    ///     within a schema when truncated to the maximum identifier length for the model.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-conventions">Model building conventions</see> and
    ///     <see href="https://aka.ms/efcore-docs-sequences">Database sequences</see> for more information.
    /// </remarks>
    public class SequenceUniquificationConvention : IModelFinalizingConvention
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SequenceUniquificationConvention" />.
        /// </summary>
        /// <param name="dependencies"> Parameter object containing dependencies for this convention. </param>
        /// <param name="relationalDependencies"> Parameter object containing relational dependencies for this convention. </param>
        public SequenceUniquificationConvention(
            ProviderConventionSetBuilderDependencies dependencies,
            RelationalConventionSetBuilderDependencies relationalDependencies)
        {
            Check.NotNull(relationalDependencies, nameof(relationalDependencies));

            Dependencies = dependencies;
            RelationalDependencies = relationalDependencies;
        }

        /// <summary>
        ///     Dependencies for this service.
        /// </summary>
        protected virtual ProviderConventionSetBuilderDependencies Dependencies { get; }

        /// <summary>
        ///     Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalConventionSetBuilderDependencies RelationalDependencies { get; }

        /// <inheritdoc />
        public virtual void ProcessModelFinalizing(
            IConventionModelBuilder modelBuilder,
            IConventionContext<IConventionModelBuilder> context)
        {
            var model = modelBuilder.Metadata;
            var modelSequences =
                (SortedDictionary<(string Name, string? Schema), ISequence>?)model[RelationalAnnotationNames.Sequences];

            if (modelSequences != null)
            {
                var maxLength = model.GetMaxIdentifierLength();
                var toReplace = modelSequences
                    .Where(s => s.Key.Name.Length > maxLength).ToList();

                foreach (var sequence in toReplace)
                {
                    var schemaName = sequence.Key.Schema;
                    var newSequenceName = Uniquifier.Uniquify(
                        sequence.Key.Name, modelSequences,
                        sequenceName => (sequenceName, schemaName), maxLength);
                    Sequence.SetName((IMutableModel)model, (Sequence)sequence.Value, newSequenceName);
                }
            }
        }
    }
}
