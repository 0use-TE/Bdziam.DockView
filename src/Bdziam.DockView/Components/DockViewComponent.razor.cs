// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bdziam.DockView.Components;

/// <summary>
/// The DockContentItem configuration sub-item corresponds to the internal content configuration of the content configuration item
/// </summary>
public partial class DockViewComponent
{
    /// <summary>
    /// Get/set whether the component displays the Header, the default is true to display
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// Get/set component Title
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Get/set component Title width default null not set
    /// </summary>
    [Parameter]
    public int? TitleWidth { get; set; }

    /// <summary>
    /// Get/set component Title style default null not set
    /// </summary>
    [Parameter]
    public string? TitleClass { get; set; }

    /// <summary>
    /// Get/set Title template default null not set
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public RenderFragment? TitleTemplate { get; set; }

    /// <summary>
    /// Get/set component Class default null not set
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Get/set whether the component is visible, the default is true
    /// </summary>
    [Parameter]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Get/set whether the component is allowed to be closed. The default is null to use the configuration of DockView
    /// </summary>
    [Parameter]
    public bool? ShowClose { get; set; }

    /// <summary>
    /// Get/set component unique identifier value default null not set, when not set, take Title as the unique identifier
    /// </summary>
    [Parameter]
    public string? Key { get; set; }

    /// <summary>
    /// Get/set whether the component is locked, the default is null, when not set, take the configuration of DockView
    /// </summary>
    /// <remarks>Locked, cannot be dragged</remarks>
    [Parameter]
    public bool? IsLock { get; set; }

    /// <summary>
    /// Get/set whether to show the lock button, the default is null, when not set, take the configuration of DockView
    /// </summary>
    [Parameter]
    public bool? ShowLock { get; set; }

    /// <summary>
    /// Get/set whether the component is floating, the default is null, when not set, take the configuration of DockView
    /// </summary>
    [Parameter]
    public bool? IsFloating { get; set; }

    /// <summary>
    /// Get/set whether to show the floating button, the default is null, when not set, take the configuration of DockView
    /// </summary>
    [Parameter]
    public bool? ShowFloat { get; set; }

    /// <summary>
    /// Get/set whether to show the maximize button, the default is null, when not set, take the configuration of DockView
    /// </summary>
    [Parameter]
    public bool? ShowMaximize { get; set; }

    /// <summary>
    /// Get/set whether the component is always displayed, the default is null, when not set, take the configuration of DockView
    /// </summary>
    [Parameter]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Renderer { get; set; }

    /// <summary>
    /// Get/set whether to show the title bar icon, the default is false
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public bool ShowTitleBar { get; set; }

    /// <summary>
    /// Get/set the title bar icon, the default is null, when not set, use the default icon
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public string? TitleBarIcon { get; set; }

    /// <summary>
    /// Get/set the title bar icon URL, the default is null, when not set, use the default icon
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public string? TitleBarIconUrl { get; set; }

    /// <summary>
    /// Get/set the title bar icon click callback method, the default is null
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public Func<Task>? OnClickTitleBarCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Type = DockViewContentType.Component;
    }

    
    private async Task OnClickBar()
    {
        if (OnClickTitleBarCallback != null)
        {
            await OnClickTitleBarCallback();
        }
    }

    /// <summary>
    /// Set the Visible parameter method
    /// </summary>
    /// <param name="visible"></param>
    public void SetVisible(bool visible)
    {
        Visible = visible;
    }
}
