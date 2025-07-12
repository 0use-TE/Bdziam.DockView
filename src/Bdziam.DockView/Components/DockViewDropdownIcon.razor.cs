// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using Bdziam.DockView.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockViewDropdownIcon component
/// </summary>
public partial class DockViewDropdownIcon
{
    /// <summary>
    /// Get/set the dropdown items for this specific dropdown
    /// </summary>
    [Parameter]
    public List<DockViewDropdownItem>? DropdownItems { get; set; } = new();

    /// <summary>
    /// Get/set the icon name - includes dropdown CSS class for DockView API integration
    /// </summary>
    private string? ClassString => CssBuilder.Default("b-dockview-control-icon b-dockview-control-icon-dropdown")
        .AddClass($"b-dockview-control-icon-{Function}")
        .Build();
}
