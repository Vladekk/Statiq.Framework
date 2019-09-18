﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statiq.Common
{
    /// <summary>
    /// A common base class for synchronous modules.
    /// </summary>
    /// <remarks>
    /// Documents can either be processed one at a time by overriding
    /// <see cref="ExecuteInput(IDocument, IExecutionContext)"/> or all
    /// at once by overriding <see cref="ExecuteContext(IExecutionContext)"/>.
    /// </remarks>
    public abstract class SyncModule : Module
    {
        /// <inheritdoc />
        protected sealed override Task<IDisposable> BeforeExecutionAsync(IExecutionContext context) =>
            Task.FromResult(BeforeExecution(context));

        /// <summary>
        /// Called before the current module execution cycle and is typically used for configuring module state.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <returns>A disposable that is guaranteed to be disposed when the module finishes the current execution cycle (or <c>null</c>).</returns>
        protected virtual IDisposable BeforeExecution(IExecutionContext context) => null;

        /// <inheritdoc />
        protected sealed override Task<IEnumerable<IDocument>> AfterExecutionAsync(IExecutionContext context, IEnumerable<IDocument> results) =>
            Task.FromResult(AfterExecution(context, results));

        /// <summary>
        /// Called after the current module execution cycle and is typically used for cleaning up module state
        /// or transforming the execution results.
        /// </summary>
        /// <remarks>
        /// If an exception is thrown during module execution, this method is never called. Return an <see cref="IDisposable"/>
        /// from <see cref="BeforeExecution(IExecutionContext)"/> if resources should be disposed even if an exception is thrown.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        /// <param name="results">The results of module execution.</param>
        /// <returns>The final module results.</returns>
        protected virtual IEnumerable<IDocument> AfterExecution(IExecutionContext context, IEnumerable<IDocument> results) => results;

        /// <inheritdoc />
        protected override sealed Task<IEnumerable<IDocument>> ExecuteContextAsync(IExecutionContext context) =>
            Task.FromResult(ExecuteContext(context));

        /// <inheritdoc />
        // Unused, prevent overriding in derived classes
        protected sealed override Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context) =>
            throw new NotSupportedException();

        /// <summary>
        /// Executes the module once for all input documents.
        /// </summary>
        /// <remarks>
        /// Override this method to execute the module once for all input documents. The default behavior
        /// calls <see cref="ExecuteInput(IDocument, IExecutionContext)"/> for each input document
        /// and overriding this method will result in <see cref="ExecuteInput(IDocument, IExecutionContext)"/>
        /// not being called.
        /// </remarks>
        /// <param name="context">The execution context.</param>
        /// <returns>The result documents.</returns>
        protected virtual IEnumerable<IDocument> ExecuteContext(IExecutionContext context) =>
            context.Inputs
                .Select(input => ExecuteInputFunc(input, context, ExecuteInput))
                .Where(x => x != null)
                .SelectMany(x => x);

        /// <summary>
        /// Executes the module.
        /// </summary>
        /// <remarks>
        /// This method will be called for each document unless <see cref="ExecuteContext(IExecutionContext)"/>
        /// is overridden.
        /// </remarks>
        /// <param name="input">
        /// The input document this module is currently processing.
        /// </param>
        /// <param name="context">The execution context.</param>
        /// <returns>The result documents.</returns>
        protected virtual IEnumerable<IDocument> ExecuteInput(IDocument input, IExecutionContext context) => null;
    }
}