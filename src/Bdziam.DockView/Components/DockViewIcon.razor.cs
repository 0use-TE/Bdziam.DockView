// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bdziam.DockView.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockViewIcon component
/// </summary>
public partial class DockViewIcon : IdComponentBase
{
    /// <summary>
    /// Get/set the function name (e.g., dock, pin, close, etc.)
    /// </summary>
    [Parameter, NotNull]
    [EditorRequired]
    public string? Function { get; set; }

    /// <summary>
    /// Get/set the icon name (Material Icon name)
    /// </summary>
    [Parameter]
    public string? IconName { get; set; }

    /// <summary>
    /// Get the style string
    /// </summary>
    private string? ClassString => CssBuilder.Default("b-dockview-control-icon")
        .AddClass($"b-dockview-control-icon-{Function}")
        .Build();

    /// <summary>
    /// Get the Href attribute
    /// </summary>
    protected string Href => $"./_content/Bdziam.DockView/icon/dockview.svg#{Function}";

    /// <summary>
    /// Get the actual icon name to display
    /// </summary>
    protected string DisplayIconName => IconName ?? GetDefaultIconForFunction();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Function ??= "close";
    }

    /// <summary>
    /// Get default Material Icon name for a function
    /// </summary>
    private string GetDefaultIconForFunction()
    {
        return Function switch
        {
            "close" => "close",
            "dock" => "tab_unselected",
            "float" => "picture_in_picture_alt",
            "pin" => "push_pin",
            "pushpin" => "keep",
            "lock" => "lock",
            "lock_open" => "lock_open",
            "full" => "fullscreen",
            "restore" => "fullscreen_exit",
            "down" => "expand_more",
            "bar" => "drag_indicator",
            "dropdown" => "arrow_drop_down",
            _ => "help_outline"
        };
    }
}
