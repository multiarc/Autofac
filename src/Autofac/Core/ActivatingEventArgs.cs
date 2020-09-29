﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Autofac.Core
{
    /// <summary>
    /// Fired after the construction of an instance but before that instance
    /// is shared with any other or any members are invoked on it.
    /// </summary>
    public class ActivatingEventArgs<T> : EventArgs, IActivatingEventArgs<T>
        where T : notnull
    {
        private T _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatingEventArgs{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="service">The service.</param>
        /// <param name="component">The component.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="instance">The instance.</param>
        public ActivatingEventArgs(IComponentContext context, Service service, IComponentRegistration component, IEnumerable<Parameter> parameters, T instance)
        {
            Service = service;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Component = component ?? throw new ArgumentNullException(nameof(component));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        /// <summary>
        /// Gets the service being resolved.
        /// </summary>
        public Service Service { get; }

        /// <summary>
        /// Gets the context in which the activation occurred.
        /// </summary>
        public IComponentContext Context { get; }

        /// <summary>
        /// Gets the component providing the instance.
        /// </summary>
        public IComponentRegistration Component { get; }

        /// <summary>
        /// Gets or sets the instance that will be used to satisfy the request.
        /// </summary>
        /// <remarks>
        /// The instance can be replaced if needed, e.g. by an interface proxy.
        /// </remarks>
        public T Instance
        {
            get
            {
                return _instance;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _instance = value;
            }
        }

        /// <summary>
        /// The instance can be replaced if needed, e.g. by an interface proxy.
        /// </summary>
        /// <param name="instance">The object to use instead of the activated instance.</param>
        public void ReplaceInstance(object instance)
        {
            Instance = (T)instance;
        }

        /// <summary>
        /// Gets the parameters supplied to the activator.
        /// </summary>
        public IEnumerable<Parameter> Parameters { get; }
    }
}
