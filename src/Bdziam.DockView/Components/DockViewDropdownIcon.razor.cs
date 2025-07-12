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
    /// Get/set the icon name
    /// </summary>
    private string? ClassString => CssBuilder.Default("dropdown dropdown-center b-dockview-control-icon")
        .AddClass($"b-dockview-control-icon-{IconName}")
        .Build();
}
