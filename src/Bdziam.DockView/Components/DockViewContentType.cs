// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;
using Bdziam.DockView.Converters;

namespace Bdziam.DockView.Components;

/// <summary>
/// DockContent type
/// </summary>
[JsonConverter(typeof(DockViewContentTypeConverter))]
public enum DockViewContentType
{
    /// <summary>
    /// Row
    /// </summary>
    Row,

    /// <summary>
    /// Column
    /// </summary>
    Column,

    /// <summary>
    /// Component group
    /// </summary>
    Group,

    /// <summary>
    /// Component
    /// </summary>
    Component,
}
