// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Bdziam.DockView.Converters;

namespace Bdziam.DockView.Components;

class DockViewConfig
{
    /// <summary>
    /// Get/set whether to enable local layout persistence, default is true
    /// </summary>
    public bool EnableLocalStorage { get; set; } = true;

    /// <summary>
    /// Get/set whether it is locked, default is false
    /// </summary>
    /// <remarks>Cannot be dragged after locking</remarks>
    [JsonPropertyName("lock")]
    public bool IsLock { get; set; }

    /// <summary>
    /// Get/set whether to show the lock button, default is true
    /// </summary>
    public bool ShowLock { get; set; }

    /// <summary>
    /// Get/set whether it is floating, default is false
    /// </summary>
    /// <remarks>Cannot be dragged after locking</remarks>
    public bool IsFloating { get; set; }

    /// <summary>
    /// Get/set whether to show the float button, default is true
    /// </summary>
    public bool ShowFloat { get; set; } = true;

    /// <summary>
    /// Get/set whether to show the close button, default is true
    /// </summary>
    public bool ShowClose { get; set; }

    /// <summary>
    /// Get/set whether to show the pin button, default is true
    /// </summary>
    public bool ShowPin { get; set; } = true;

    /// <summary>
    /// Get/set whether to show the maximize button, default is true
    /// </summary>
    public bool ShowMaximize { get; set; } = true;

    /// <summary>
    /// Get/set client-side rendering mode, default is null, client-side defaults to always onlyWhenVisible
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DockViewRenderMode Renderer { get; set; }

    /// <summary>
    /// Get/set the C# callback method when panel visibility changes
    /// </summary>
    public string? PanelVisibleChangedCallback { get; set; }

    /// <summary>
    /// Get/set the C# callback method when the component is initialized
    /// </summary>
    public string? InitializedCallback { get; set; }

    /// <summary>
    /// Get/set the C# callback method when the lock state changes
    /// </summary>
    public string? LockChangedCallback { get; set; }

    /// <summary>
    /// Get/set the C# callback method when the splitter is adjusted
    /// </summary>
    public string? SplitterCallback { get; set; }

    /// <summary>
    /// Get/set the client-side cache key
    /// </summary>
    public string? LocalStorageKey { get; set; }

    /// <summary>
    /// Get/set the Golden-Layout configuration item collection, default is an empty collection
    /// </summary>
    [JsonPropertyName("content")]
    [JsonConverter(typeof(DockViewComponentConverter))]
    public List<DockViewComponentBase> Contents { get; set; } = [];

    /// <summary>
    /// Get/set the component theme, default is null
    /// </summary>
    public string? Theme { get; set; }

    /// <summary>
    /// Get/set the layout configuration, default is null
    /// </summary>
    public string? LayoutConfig { get; set; }
}
