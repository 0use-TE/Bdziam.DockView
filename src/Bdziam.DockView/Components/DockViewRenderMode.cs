// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;
using Bdziam.DockView.Converters;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockViewRenderMode rendering mode enumeration type
/// </summary>
[JsonConverter(typeof(DockViewRenderModeConverter))]
public enum DockViewRenderMode
{
    /// <summary>
    /// Render only when visible
    /// </summary>
    OnlyWhenVisible,

    /// <summary>
    /// Always render
    /// </summary>
    Always
}
