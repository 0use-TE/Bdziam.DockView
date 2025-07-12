// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockView dropdown menu item
/// </summary>
public class DockViewDropdownItem
{
    /// <summary>
    /// Get/set the text to display
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// Get/set the icon name (Material Icons)
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Get/set the click event callback
    /// </summary>
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Get/set whether the item is disabled
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Get/set additional CSS classes
    /// </summary>
    public string? CssClass { get; set; }
}
