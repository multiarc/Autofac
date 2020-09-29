﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Reflection;

namespace Autofac.Core
{
    /// <summary>
    /// Provides a property selector that applies a filter defined by a delegate.
    /// </summary>
    public sealed class DelegatePropertySelector : IPropertySelector
    {
        private readonly Func<PropertyInfo, object, bool> _finder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegatePropertySelector"/> class
        /// that invokes a delegate to determine selection.
        /// </summary>
        /// <param name="finder">Delegate to determine whether a property should be injected.</param>
        public DelegatePropertySelector(Func<PropertyInfo, object, bool> finder)
        {
            _finder = finder ?? throw new ArgumentNullException(nameof(finder));
        }

        /// <inheritdoc/>
        public bool InjectProperty(PropertyInfo property, object instance)
        {
            return _finder(property, instance);
        }
    }
}
