// Copyright (c) Bdziam. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bdziam.DockView.Infrastructure;

/// <summary>
/// A utility class for building CSS class strings in a fluent manner
/// </summary>
public class CssBuilder
{
    private readonly StringBuilder _stringBuilder;

    /// <summary>
    /// Initializes a new instance of the CssBuilder class
    /// </summary>
    private CssBuilder()
    {
        _stringBuilder = new StringBuilder();
    }

    /// <summary>
    /// Initializes a new instance of the CssBuilder class with an initial CSS class
    /// </summary>
    /// <param name="cssClass">The initial CSS class</param>
    private CssBuilder(string? cssClass)
    {
        _stringBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(cssClass))
        {
            _stringBuilder.Append(cssClass);
        }
    }

    /// <summary>
    /// Creates a new CssBuilder instance
    /// </summary>
    /// <returns>A new CssBuilder instance</returns>
    public static CssBuilder Default() => new();

    /// <summary>
    /// Creates a new CssBuilder instance with an initial CSS class
    /// </summary>
    /// <param name="cssClass">The initial CSS class</param>
    /// <returns>A new CssBuilder instance</returns>
    public static CssBuilder Default(string? cssClass) => new(cssClass);

    /// <summary>
    /// Adds a CSS class to the builder
    /// </summary>
    /// <param name="cssClass">The CSS class to add</param>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder AddClass(string? cssClass)
    {
        if (!string.IsNullOrWhiteSpace(cssClass))
        {
            if (_stringBuilder.Length > 0)
            {
                _stringBuilder.Append(' ');
            }
            _stringBuilder.Append(cssClass);
        }
        return this;
    }

    /// <summary>
    /// Conditionally adds a CSS class to the builder
    /// </summary>
    /// <param name="cssClass">The CSS class to add</param>
    /// <param name="condition">The condition that determines whether to add the class</param>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder AddClass(string? cssClass, bool condition)
    {
        return condition ? AddClass(cssClass) : this;
    }

    /// <summary>
    /// Conditionally adds a CSS class to the builder using a function
    /// </summary>
    /// <param name="cssClass">The CSS class to add</param>
    /// <param name="conditionFunc">A function that returns whether to add the class</param>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder AddClass(string? cssClass, Func<bool> conditionFunc)
    {
        return conditionFunc != null && conditionFunc() ? AddClass(cssClass) : this;
    }

    /// <summary>
    /// Adds CSS classes from additional attributes dictionary
    /// </summary>
    /// <param name="additionalAttributes">The additional attributes dictionary</param>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder AddClassFromAttributes(IDictionary<string, object>? additionalAttributes)
    {
        if (additionalAttributes != null && additionalAttributes.TryGetValue("class", out var cssClass))
        {
            AddClass(cssClass?.ToString());
        }
        return this;
    }

    /// <summary>
    /// Adds multiple CSS classes
    /// </summary>
    /// <param name="cssClasses">The CSS classes to add</param>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder AddClasses(params string?[] cssClasses)
    {
        if (cssClasses != null)
        {
            foreach (var cssClass in cssClasses)
            {
                AddClass(cssClass);
            }
        }
        return this;
    }

    /// <summary>
    /// Adds multiple CSS classes conditionally
    /// </summary>
    /// <param name="condition">The condition that determines whether to add the classes</param>
    /// <param name="cssClasses">The CSS classes to add</param>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder AddClasses(bool condition, params string?[] cssClasses)
    {
        return condition ? AddClasses(cssClasses) : this;
    }

    /// <summary>
    /// Removes a CSS class from the builder
    /// </summary>
    /// <param name="cssClass">The CSS class to remove</param>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder RemoveClass(string? cssClass)
    {
        if (!string.IsNullOrWhiteSpace(cssClass))
        {
            var currentClasses = _stringBuilder.ToString();
            var classes = currentClasses.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var filteredClasses = classes.Where(c => !string.Equals(c, cssClass, StringComparison.OrdinalIgnoreCase));

            _stringBuilder.Clear();
            _stringBuilder.Append(string.Join(" ", filteredClasses));
        }
        return this;
    }

    /// <summary>
    /// Clears all CSS classes from the builder
    /// </summary>
    /// <returns>The current CssBuilder instance for fluent chaining</returns>
    public CssBuilder Clear()
    {
        _stringBuilder.Clear();
        return this;
    }

    /// <summary>
    /// Builds the final CSS class string
    /// </summary>
    /// <returns>The CSS class string, or null if no classes were added</returns>
    public string? Build()
    {
        var result = _stringBuilder.ToString().Trim();
        return string.IsNullOrEmpty(result) ? null : result;
    }

    /// <summary>
    /// Returns the string representation of the CSS classes
    /// </summary>
    /// <returns>The CSS class string</returns>
    public override string ToString() => Build() ?? string.Empty;

    /// <summary>
    /// Implicitly converts a CssBuilder to a string
    /// </summary>
    /// <param name="builder">The CssBuilder instance</param>
    public static implicit operator string?(CssBuilder builder) => builder?.Build();
}
