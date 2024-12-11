//---------------------------------------------------------------------
// <copyright file="ExceptionUtils.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

using System.Diagnostics;

namespace NodeToStringBuilderBracketsBug;

internal class ExceptionUtils
{
    [DebuggerStepThrough]
    internal static T CheckArgumentNotNull<T>([ValidatedNotNull] T value, string parameterName) where T : class
    {
        Debug.Assert(!string.IsNullOrEmpty(parameterName), "!string.IsNullOrEmpty(parameterName)");

        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return value;
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
