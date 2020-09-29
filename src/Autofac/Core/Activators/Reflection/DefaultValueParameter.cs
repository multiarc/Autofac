﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Autofac.Core.Activators.Reflection
{
    /// <summary>
    /// Provides parameters that have a default value, set with an optional parameter
    /// declaration in C# or VB.
    /// </summary>
    public class DefaultValueParameter : Parameter
    {
        /// <summary>
        /// Returns true if the parameter is able to provide a value to a particular site.
        /// </summary>
        /// <param name="pi">Constructor, method, or property-mutator parameter.</param>
        /// <param name="context">The component context in which the value is being provided.</param>
        /// <param name="valueProvider">If the result is true, the <paramref name="valueProvider" /> parameter will
        /// be set to a function that will lazily retrieve the parameter value. If the result is <see langword="false" />,
        /// will be set to <see langword="null" />.</param>
        /// <returns><see langword="true" /> if a value can be supplied; otherwise, <see langword="false" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="pi" /> is <see langword="null" />.
        /// </exception>
        public override bool CanSupplyValue(ParameterInfo pi, IComponentContext context, [NotNullWhen(returnValue: true)] out Func<object?>? valueProvider)
        {
            if (pi == null)
            {
                throw new ArgumentNullException(nameof(pi));
            }

            bool hasDefaultValue;
            var tryToGetDefaultValue = true;
            try
            {
                // Workaround for https://github.com/dotnet/corefx/issues/17943
                if (pi.Member.DeclaringType?.Assembly.IsDynamic ?? true)
                {
                    hasDefaultValue = pi.DefaultValue != null && pi.HasDefaultValue;
                }
                else
                {
                    hasDefaultValue = pi.HasDefaultValue;
                }
            }
            catch (FormatException) when (pi.ParameterType == typeof(DateTime))
            {
                // Workaround for https://github.com/dotnet/corefx/issues/12338
                // If HasDefaultValue throws FormatException for DateTime
                // we expect it to have default value
                hasDefaultValue = true;
                tryToGetDefaultValue = false;
            }

            if (hasDefaultValue)
            {
                valueProvider = () =>
                {
                    if (!tryToGetDefaultValue)
                    {
                        return default(DateTime);
                    }

                    var defaultValue = pi.DefaultValue;

                    // Workaround for https://github.com/dotnet/corefx/issues/11797
                    if (defaultValue == null && pi.ParameterType.IsValueType)
                    {
                        defaultValue = Activator.CreateInstance(pi.ParameterType);
                    }

                    return defaultValue;
                };

                return true;
            }

            valueProvider = null;
            return false;
        }
    }
}
