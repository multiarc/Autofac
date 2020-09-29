using System.Collections.Generic;
using System.Diagnostics;
using Autofac.Features.Decorators;

namespace Autofac.Core.Resolving.Pipeline
{
    /// <inheritdoc />
    public interface IResolveContext : IComponentContext
    {
        /// <summary>
        /// Gets a reference to the owning resolve operation (which might emcompass multiple nested requests).
        /// </summary>
        IResolveOperation Operation { get; }

        /// <summary>
        /// Gets or sets the lifetime scope that will be used for the activation of any components later in the pipeline.
        /// Avoid resolving instances directly from this scope; they will not be traced as part of the same operation.
        /// </summary>
        ISharingLifetimeScope ActivationScope { get; }

        /// <summary>
        /// Gets the component registration that is being resolved in the current request.
        /// </summary>
        IComponentRegistration Registration { get; }

        /// <summary>
        /// Gets the service that is being resolved in the current request.
        /// </summary>
        Service Service { get; }

        /// <summary>
        /// Gets the target registration for decorator requests.
        /// </summary>
        IComponentRegistration? DecoratorTarget { get; }

        /// <summary>
        /// Gets or sets the instance that will be returned as the result of the resolve request.
        /// On the way back up the pipeline, after calling next(ctxt), this value will be populated
        /// with the resolved instance. Check the <see cref="NewInstanceActivated"/> property to determine
        /// whether the object here was a newly activated instance, or a shared instance previously activated.
        /// </summary>
        object? Instance { get; set; }

        /// <summary>
        /// Gets a value indicating whether the resolved <see cref="Instance"/> is a new instance of a component has been activated during this request,
        /// or an existing shared instance that has been retrieved.
        /// </summary>
        bool NewInstanceActivated { get; }

        /// <summary>
        /// Gets the <see cref="System.Diagnostics.DiagnosticListener"/> to which trace events should be written.
        /// </summary>
        DiagnosticListener DiagnosticSource { get; }

        /// <summary>
        /// Gets or sets the current resolve parameters. These can be changed using the <see cref="ChangeParameters(IEnumerable{Parameter})"/> method.
        /// </summary>
        IEnumerable<Parameter> Parameters { get; }

        /// <summary>
        /// Gets or sets the phase of the pipeline reached by this request.
        /// </summary>
        PipelinePhase PhaseReached { get; }

        /// <summary>
        /// Gets or sets the active decorator context for the request.
        /// </summary>
        DecoratorContext? DecoratorContext { get; }
    }
}
