// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockView component configuration options
/// </summary>
public sealed class DockViewOptions
{
    /// <summary>
    /// Configuration section name for DockView options
    /// </summary>
    public const string SectionName = "DockView";
    /// <summary>
    /// Get/set the component version number
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Get/set whether to enable local storage, default is null (not set)
    /// </summary>
    public bool? EnableLocalStorage { get; set; }

    /// <summary>
    /// Get/set the local storage key prefix
    /// </summary>
    public string? LocalStoragePrefix { get; set; }
}
