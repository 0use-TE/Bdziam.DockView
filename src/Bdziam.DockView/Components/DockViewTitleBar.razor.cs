// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockViewTitle component
/// </summary>
public partial class DockViewTitleBar
{
    /// <summary>
    /// Get/set the title bar icon click callback method, default is null
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickBarCallback { get; set; }

    /// <summary>
    /// Get/set the title bar icon, default is null, use the default icon when not set
    /// </summary>
    [Parameter]
    public string? BarIcon { get; set; }

    /// <summary>
    /// Get/set the title bar icon URL, default is null, use the default icon when not set
    /// </summary>
    [Parameter]
    public string? BarIconUrl { get; set; }

    private async Task OnClickBar()
    {
        if (OnClickBarCallback != null)
        {
            await OnClickBarCallback();
        }
    }
}
