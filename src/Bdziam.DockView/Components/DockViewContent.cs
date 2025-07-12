// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Bdziam.DockView.Converters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Bdziam.DockView.Components;

/// <summary>
/// The DockContent class corresponds to the content configuration item
/// </summary>
public class DockViewContent : DockViewComponentBase
{
    /// <summary>
    /// Constructor to set default Type
    /// </summary>
    public DockViewContent()
    {
        Type = DockViewContentType.Column;
    }

    /// <summary>
    /// Get/set the content item collection
    /// </summary>
    [JsonConverter(typeof(DockViewComponentConverter))]
    [JsonPropertyName("content")]
    public List<DockViewComponentBase> Items { get; set; } = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<List<DockViewComponentBase>>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<List<DockViewComponentBase>>.Value), Items);
        builder.AddAttribute(2, nameof(CascadingValue<List<DockViewComponentBase>>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<List<DockViewComponentBase>>.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}
