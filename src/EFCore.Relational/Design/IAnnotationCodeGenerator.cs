// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore.Design
{
    /// <summary>
    ///     Implemented by database providers to control which annotations need to
    ///     have code generated (as opposed to being handled by convention) and then to generate
    ///     the code if needed.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-providers">Implementation of database providers and extensions</see>
    ///     for more information.
    /// </remarks>
    public interface IAnnotationCodeGenerator
    {
        /// <summary>
        ///     Filters out annotations in <paramref name="annotations" /> for which code should never be generated.
        /// </summary>
        /// <param name="annotations"> The annotations from which to filter the ignored ones. </param>
        /// <returns> The filtered annotations. </returns>
        IEnumerable<IAnnotation> FilterIgnoredAnnotations(IEnumerable<IAnnotation> annotations);

        /// <summary>
        ///     Removes annotation whose configuration is already applied by convention, and do not need to be
        ///     specified explicitly.
        /// </summary>
        /// <param name="model"> The model to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to remove the conventional ones. </param>
        void RemoveAnnotationsHandledByConventions(IModel model, IDictionary<string, IAnnotation> annotations) { }

        /// <summary>
        ///     Removes annotation whose configuration is already applied by convention, and do not need to be
        ///     specified explicitly.
        /// </summary>
        /// <param name="entity"> The entity to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to remove the conventional ones. </param>
        void RemoveAnnotationsHandledByConventions(IEntityType entity, IDictionary<string, IAnnotation> annotations) { }

        /// <summary>
        ///     Removes annotation whose configuration is already applied by convention, and do not need to be
        ///     specified explicitly.
        /// </summary>
        /// <param name="property"> The property to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to remove the conventional ones. </param>
        void RemoveAnnotationsHandledByConventions(IProperty property, IDictionary<string, IAnnotation> annotations) { }

        /// <summary>
        ///     Removes annotation whose configuration is already applied by convention, and do not need to be
        ///     specified explicitly.
        /// </summary>
        /// <param name="key"> The key to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to remove the conventional ones. </param>
        void RemoveAnnotationsHandledByConventions(IKey key, IDictionary<string, IAnnotation> annotations) { }

        /// <summary>
        ///     Removes annotation whose configuration is already applied by convention, and do not need to be
        ///     specified explicitly.
        /// </summary>
        /// <param name="foreignKey"> The foreign key to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to remove the conventional ones. </param>
        void RemoveAnnotationsHandledByConventions(IForeignKey foreignKey, IDictionary<string, IAnnotation> annotations) { }

        /// <summary>
        ///     Removes annotation whose configuration is already applied by convention, and do not need to be
        ///     specified explicitly.
        /// </summary>
        /// <param name="index"> The index to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to remove the conventional ones. </param>
        void RemoveAnnotationsHandledByConventions(IIndex index, IDictionary<string, IAnnotation> annotations) { }

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="annotatable"> The annotatable to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        void RemoveAnnotationsHandledByConventions(IAnnotatable annotatable, IDictionary<string, IAnnotation> annotations)
        {
            switch (annotatable)
            {
                    case IModel model:
                        RemoveAnnotationsHandledByConventions(model, annotations);
                        return;

                    case IEntityType entityType:
                        RemoveAnnotationsHandledByConventions(entityType, annotations);
                        return;

                    case IProperty property:
                        RemoveAnnotationsHandledByConventions(property, annotations);
                        return;

                    case IKey key:
                        RemoveAnnotationsHandledByConventions(key, annotations);
                        return;

                    case IForeignKey foreignKey:
                        RemoveAnnotationsHandledByConventions(foreignKey, annotations);
                        return;

                    case INavigation navigation:
                        RemoveAnnotationsHandledByConventions(navigation, annotations);
                        return;

                    case ISkipNavigation skipNavigation:
                        RemoveAnnotationsHandledByConventions(skipNavigation, annotations);
                        return;

                    case IIndex index:
                        RemoveAnnotationsHandledByConventions(index, annotations);
                        return;

                    default:
                        throw new ArgumentException(RelationalStrings.UnhandledAnnotatableType(annotatable.GetType()));
            }
        }

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="model"> The model to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            IModel model,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="entityType"> The entity type to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            IEntityType entityType,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="property"> The property to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            IProperty property,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="key"> The key to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            IKey key,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="foreignKey"> The foreign key to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            IForeignKey foreignKey,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="navigation"> The navigation to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            INavigation navigation,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="navigation"> The skip navigation to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            ISkipNavigation navigation,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="index"> The index to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(
            IIndex index,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<MethodCallCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding fluent API calls, returns those fluent API calls
        ///     and removes the annotations.
        /// </summary>
        /// <param name="annotatable"> The annotatable to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<MethodCallCodeFragment> GenerateFluentApiCalls(IAnnotatable annotatable, IDictionary<string, IAnnotation> annotations)
            => annotatable switch
            {
                IModel model => GenerateFluentApiCalls(model, annotations),
                IEntityType entityType => GenerateFluentApiCalls(entityType, annotations),
                IProperty property => GenerateFluentApiCalls(property, annotations),
                IKey key => GenerateFluentApiCalls(key, annotations),
                IForeignKey foreignKey => GenerateFluentApiCalls(foreignKey, annotations),
                INavigation navigation => GenerateFluentApiCalls(navigation, annotations),
                ISkipNavigation skipNavigation => GenerateFluentApiCalls(skipNavigation, annotations),
                IIndex index => GenerateFluentApiCalls(index, annotations),
                _ => throw new ArgumentException(RelationalStrings.UnhandledAnnotatableType(annotatable.GetType()))
            };

        /// <summary>
        ///     For the given annotations which have corresponding data annotation attributes, returns those attribute code fragments
        ///     and removes the annotations.
        /// </summary>
        /// <param name="entityType"> The entity type to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<AttributeCodeFragment> GenerateDataAnnotationAttributes(
            IEntityType entityType,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<AttributeCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding data annotation attributes, returns those attribute code fragments
        ///     and removes the annotations.
        /// </summary>
        /// <param name="property"> The property to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<AttributeCodeFragment> GenerateDataAnnotationAttributes(
            IProperty property,
            IDictionary<string, IAnnotation> annotations)
            => Array.Empty<AttributeCodeFragment>();

        /// <summary>
        ///     For the given annotations which have corresponding data annotation attributes, returns those attribute code fragments
        ///     and removes the annotations.
        /// </summary>
        /// <param name="annotatable"> The annotatable to which the annotations are applied. </param>
        /// <param name="annotations"> The set of annotations from which to generate fluent API calls. </param>
        IReadOnlyList<AttributeCodeFragment> GenerateDataAnnotationAttributes(
            IAnnotatable annotatable,
            IDictionary<string, IAnnotation> annotations)
            => annotatable switch
            {
                IEntityType entityType => GenerateDataAnnotationAttributes(entityType, annotations),
                IProperty property => GenerateDataAnnotationAttributes(property, annotations),
                _ => throw new ArgumentException(RelationalStrings.UnhandledAnnotatableType(annotatable.GetType()))
            };
    }
}
