// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace Bdziam.DockView.Data;

/// <summary>
/// DockView component theme
/// </summary>
public enum DockViewTheme
{
    /// <summary>
    /// dockview-theme-light theme
    /// </summary>
    [Description("dockview-theme-light")]
    Light,

    /// <summary>
    /// dockview-theme-dark theme
    /// </summary>
    [Description("dockview-theme-dark")]
    Dark,

    /// <summary>
    /// dockview-theme-vs theme
    /// </summary>
    [Description("dockview-theme-vs")]
    VS,

    /// <summary>
    /// dockview-theme-abyss theme
    /// </summary>
    [Description("dockview-theme-abyss")]
    Abyss,

    /// <summary>
    /// dockview-theme-dracula theme
    /// </summary>
    [Description("dockview-theme-dracula")]
    Dracula,

    /// <summary>
    /// dockview-theme-replit theme
    /// </summary>
    [Description("dockview-theme-replit")]
    Replit
}
