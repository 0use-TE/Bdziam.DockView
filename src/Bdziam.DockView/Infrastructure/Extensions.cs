// Copyright (c) Bdziam. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Bdziam.DockView.Components;

/// <summary>
/// Extension methods for various types
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Gets the description string for an enum value using the Description attribute
    /// </summary>
    /// <param name="value">The enum value</param>
    /// <returns>The description string or the enum value name if no description is found</returns>
    public static string ToDescriptionString(this Enum value)
    {
        if (value == null)
            return string.Empty;

        var field = value.GetType().GetField(value.ToString());
        if (field == null)
            return value.ToString();

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString().ToLowerInvariant();
    }
}
