// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using Bdziam.DockView.Infrastructure;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockComponent base class
/// </summary>
public abstract class DockViewComponentBase : IdComponentBase, IDisposable
{
    /// <summary>
    /// Get/set the render type, default is Component
    /// </summary>
    [Parameter]
    public DockViewContentType Type { get; set; }

    /// <summary>
    /// Get/set the component width percentage, default is null (not set)
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// Get/set the component height percentage, default is null (not set)
    /// </summary>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// Get/set child components
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Get/set the DockContent instance
    /// </summary>
    [CascadingParameter]
    private List<DockViewComponentBase>? Parent { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Parent?.Add(this);
    }

    /// <summary>
    /// Dispose method
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Parent?.Remove(this);
        }
    }

    /// <summary>
    /// Dispose method
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
