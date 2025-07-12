// Copyright (c) Bdziam. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Bdziam.DockView.Infrastructure;

/// <summary>
/// Base component class that provides common functionality for components that need an Id
/// </summary>
public abstract class IdComponentBase : ComponentBase
{
    /// <summary>
    /// Gets or sets the unique identifier for this component
    /// </summary>
    [Parameter]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that will be applied to the component
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Generates a unique ID for the component
    /// </summary>
    /// <returns>A unique string identifier</returns>
    protected virtual string GenerateId() => $"dv_{Guid.NewGuid():N}";

    protected override void OnInitialized()
    {
        Id = Id ?? GenerateId();
        base.OnInitialized();
    }
    
}

/// <summary>
/// Simple attribute to mark properties as required (minimal replacement for Bootstrap Blazor's NotNull)
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NotNullAttribute : RequiredAttribute
{
    public NotNullAttribute() : base()
    {
        ErrorMessage = "This property is required and cannot be null.";
    }
}
